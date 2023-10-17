using System;
using System.Text;
using System.Net;
using System.Net.Http;

namespace AutoRestRef.DataAccess
{
    class RemoteFileAccess
    {
        public static string GetOnlineContent(string url)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                var client = new HttpClient(handler);
                var uri = new Uri(url);
                var content = client.GetStringAsync(uri).Result;

                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
