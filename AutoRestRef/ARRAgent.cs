using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using AutoRestRef.Templates;
using AutoRestRef.DataAccess;
using System.Net;

namespace AutoRestRef
{
    class ARRAgent
    {
        public static bool Run(string inputFilePath, string outputFilePath)
        {
            var inputJsonArray = CheckAndGetInputContent(inputFilePath);
            if (inputJsonArray == null) return false;

            var outputObj = GetRestAPIOutput(inputJsonArray);
            var outputJsonStr = JsonConvert.SerializeObject(outputObj, Formatting.Indented);
            var writeSuccess = LocalFileAccess.WriteFile(outputFilePath, outputJsonStr);
            if (writeSuccess)
            {
                Console.WriteLine("Save file successfully!");
                return true;
            }
            else
            {
                Console.WriteLine("Save file failed with errors.");
                return false;
            }
        }
        private static List<InputTemplate> CheckAndGetInputContent(string inputFilePath)
        {
            var content = LocalFileAccess.ReadFile(inputFilePath);
            //if local file not exist
            if (content == null) return null;
            try
            {
                //judge the input format match to input template
                return JsonConvert.DeserializeObject<List<InputTemplate>>(content, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });
            }
            catch (Exception e)
            {
                //if input json format not match to InputTemplate structure then failed.
                Console.WriteLine(e.Message);
                return null;
            }
        }
        private static List<OutputTemplate> GetRestAPIOutput(List<InputTemplate> inputJsonArray)
        {
            var restApiOutput = inputJsonArray.Select(obj => new OutputTemplate(obj.name, obj.scope, GetServices(obj.tocUrl))).ToList();
            return restApiOutput;
        }
        private static List<ServiceTemplate> GetServices(string tocUrl)
        {
            var content = RemoteFileAccess.GetOnlineContent(tocUrl);
            //if 404 or no content then return empty list
            if (content == null) return new List<ServiceTemplate>();
            //parse toc.json
            var urlAbsPath = tocUrl.Replace("toc.json", "");
            var tocJsonObj = JObject.Parse(content);
            var objWithChildren = tocJsonObj["items"].Where(obj => obj["children"] != null && obj["toc_title"] != null && obj["href"] != null);
            var services = objWithChildren.Select(obj =>
            {
                var name = (string)obj["toc_title"];
                var url = new Uri(urlAbsPath + obj["href"]).ToString();
                var des = GetDes(url);
                return new ServiceTemplate(name, url, des);
            }).ToList();
            return services;
        }
        private static string GetDes(string serviceUrl)
        {
            var content = WebUtility.HtmlDecode(RemoteFileAccess.GetOnlineContent(serviceUrl));
            const string defaultDesContent = "";
            //if 404 or parse error then return ""
            if (content == null) return defaultDesContent;
            //parse the html
            Console.WriteLine($"Parse description from {serviceUrl}");
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var desNodes = doc.DocumentNode.SelectSingleNode("//main/p");
            var des = desNodes?.InnerText ?? defaultDesContent;
            return des;
        }
    }
}
