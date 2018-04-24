using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using AutoRestRef.Templates;
using AutoRestRef.DataAccess;

namespace AutoRestRef
{
    class ARRAgent
    { 

        private static string GetDes(string serviceUrl)
        {
            var content = RemoteFileAccess.GetOnlineContent(serviceUrl);
            const string defaultDesContent = "";
            if (content != null)
            {
                //parse the html
                Console.WriteLine($"Parse description from {serviceUrl}");
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var desNodes = doc.DocumentNode.SelectNodes("//main/p");
                var value = desNodes == null ? defaultDesContent : desNodes.First().InnerText;           
                return value;
            }
            else
            {
                //if 404 or parse error then return ""
                return defaultDesContent;
            }
        }
        private static List<ServiceTemplate> GetServices(string tocUrl)
        {
            var content = RemoteFileAccess.GetOnlineContent(tocUrl);
            if (content != null)
            {
                var urlAbsPath = tocUrl.Replace("toc.json", "");
                var tocJsonObj = JObject.Parse(content);
                var objWithChildren = tocJsonObj["items"].Where(obj => obj["children"] != null && obj["toc_title"] != null && obj["href"] != null);
                var rsl = objWithChildren.Select(obj =>
                {
                    var name = (string)obj["toc_title"];
                    var url = urlAbsPath + obj["href"];
                    var des = GetDes(url);
                    return new ServiceTemplate(name, url, des);
                }).ToList();
                return rsl;
            }
            else
            {
                //if 404 or no content then return empty list
                return new List<ServiceTemplate>();
            }
        }
        private static List<OutputTemplate> GetRestAPIOutput(string inputFilePath)
        {
            var content = LocalFileAccess.ReadFile(inputFilePath);
            if (content != null)
            {
                var restApiJArray = JArray.Parse(content);
                var rsl = restApiJArray.Select(obj =>
                {
                    var name = (string)obj["name"];
                    var scope = (string)obj["scope"];
                    var services = GetServices((string)obj["toc_url"]);
                    return new OutputTemplate(name, scope, services);
                }).ToList();
                return rsl;
            }
            else
            { 
                return null;
            }
        }
        public static void Run(string inputFilePath, string outputFilePath)
        {
            var rsl = GetRestAPIOutput(inputFilePath);
            var jsonString = JsonConvert.SerializeObject(rsl, Formatting.Indented);
            var writeSuccess = LocalFileAccess.WriteFile(outputFilePath, jsonString);
            if (writeSuccess)
            {
                Console.WriteLine("Save file successfully!");
            }
            else {
                Console.WriteLine("Save file failed with errors.");
            }
        }
    }
}
