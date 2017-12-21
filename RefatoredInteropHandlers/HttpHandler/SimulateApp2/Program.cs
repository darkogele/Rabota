using System;
using System.IO;
using System.Net;

namespace SimulateApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Imoten list AKN test okolina
                //var client = new AKNPropertyListTest.PropertyListClient();
                //var result = client.GetPropertyList("mio", "katastarservis", "1", "1", "1");
                //var resultFromCallingService = result;


                //Imoten list dokument AKN test okolina
                //var client = new AKNPListDocClient();
                //var result = client.GetPListDoc("25", "997", "1024", "", false);
                //var resultFromCallingService = result;

                var responseFromServer = string.Empty;
                var request = (HttpWebRequest)WebRequest.Create("http://localhost/ApiCallingHandler/api/callinghandler/callhandler");
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
                var resultFromCallingService = responseFromServer;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
