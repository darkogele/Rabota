using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace Implementations.Helpers
{
    public static class Helper
    {
        public static string GetData(string embg)
        {
            string responseFromService = string.Empty;
            var serviceUrl = ConfigurationSettings.AppSettings.Get("MonServiceUrl");
            if (!string.IsNullOrEmpty(serviceUrl))
            {
                var request = (HttpWebRequest)WebRequest.Create(serviceUrl);
                request.Method = "POST";
                string postData = "embg=" + embg + "&token=LMV6WBP0820PRJ60JECE59KSLLN9BB";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                var status = ((HttpWebResponse)response).StatusDescription;
                dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                responseFromService = responseFromServer;
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            return responseFromService;
        }
    }
}
