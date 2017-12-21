using System;
using System.IO;
using System.Net;
using System.ServiceModel;

namespace SimulateApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string responseFromServer = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://localhost/ApiCallingHandler/api/callinghandler/callhandler");
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
                var resultFromCallingService = responseFromServer;
            }
            catch (WebException exception)
            {
                WebResponse errResponse = exception.Response;
                string fullInteropFaultFromAdapter = string.Empty;
                using (Stream faultFromAnotherSide = errResponse.GetResponseStream())
                {
                    if (faultFromAnotherSide != null)
                    {
                        var reader = new StreamReader(faultFromAnotherSide);
                        fullInteropFaultFromAdapter = reader.ReadToEnd();
                    }
                }
                if (!string.IsNullOrEmpty(fullInteropFaultFromAdapter))
                {
                    throw new FaultException("");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception.InnerException);
            }
           
        }

    }
}
