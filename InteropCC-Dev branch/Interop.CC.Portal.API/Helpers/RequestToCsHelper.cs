using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Interop.CC.Models.FaultModels;
using Interop.CC.Models.Models;
using Newtonsoft.Json;

namespace Interop.CC.Portal.API.Helpers
{
    public static class RequestToCsHelper
    {
        /// <summary>
        /// For GET, PUT request that may contain or not request parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>

        public static T MakeRequestToCs<T>(string url, string requestMethod)
        {
            string responseFromServer = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = requestMethod;
            request.ContentType = "application/x-www-form-urlencoded";
            if (requestMethod == "PUT")
            {
                request.ContentLength = 0;
            }
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
            return JsonConvert.DeserializeObject<T>(responseFromServer);
        }

        /// <summary>
        /// For POST request that has object as a parameter
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessMapping"></param>
        /// <returns></returns>
        public static CreateAccessMappingResult MakeRequestToCsCreateAccessMapping(string url, AccessMapping accessMapping)
        {
            try
            {
                string responseFromServer;

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json;charset=utf-8";
                request.Accept = "application/json; charset=utf-8";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string serializeObject = new JavaScriptSerializer().Serialize(accessMapping);

                    streamWriter.Write(serializeObject);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        responseFromServer = streamReader.ReadToEnd();
                    }
                }
                if (!string.IsNullOrEmpty(responseFromServer))
                {
                    return JsonConvert.DeserializeObject<CreateAccessMappingResult>(responseFromServer);
                }
            }
            catch (WebException webException)
            {
                string exceptionFromAccMapp = string.Empty;
                WebResponse errResponse = webException.Response;
                if (errResponse.ContentLength > 0)
                {
                    using (Stream faultFromAnotherSide = errResponse.GetResponseStream())
                    {
                        if (faultFromAnotherSide != null)
                        {
                            var reader = new StreamReader(faultFromAnotherSide);
                            exceptionFromAccMapp = reader.ReadToEnd();
                        }
                    }
                    if (!string.IsNullOrEmpty(exceptionFromAccMapp))
                    {
                        var accessMappingError = new JavaScriptSerializer().Deserialize<AccessMappingFaultModel>(exceptionFromAccMapp);
                        throw new Exception(accessMappingError.exceptionMessage);
                    }
                }
            }
            return new CreateAccessMappingResult();
        }

    }
}