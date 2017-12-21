using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Interop.CS.Portal.UI.Helper
{
    public class WebRequestHelper
    {
        public static string GetAccessToken(string url, string data)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            // Create a request using a URL that can receive a post. 
            var request = (HttpWebRequest)WebRequest.Create(url);
            //Add certification to request
            //var cert = new X509Certificate2(certFile, certPass, X509KeyStorageFlags.MachineKeySet);
            //request.ClientCertificates.Add(cert);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            var status = ((HttpWebResponse)response).StatusDescription;
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            var responseToken = responseFromServer;
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseToken;
        }
    }
}