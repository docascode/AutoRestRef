using System;
using System.Text;
using System.Net;

namespace AutoRestRef.DataAccess
{
    class RemoteFileAccess
    {
        public static string GetOnlineContent(string url)
        {
            try
            {
                var client = new WebClient();
                var uri = new Uri(url);
                return Encoding.UTF8.GetString(client.DownloadData(uri));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
