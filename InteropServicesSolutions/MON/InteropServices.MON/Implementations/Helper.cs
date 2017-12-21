using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.MON.Implementations
{
    public static class Helper
    {
        public static string GetData(string EMBG)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://ednevnik.edu.mk/api/student");
            request.Method = "POST";
            string postData = "embg=" + EMBG + "&token=LMV6WBP0820PRJ60JECE59KSLLN9BB";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            var status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var responseToken = responseFromServer;
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseToken;
        }
    }
}
