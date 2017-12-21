using System;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/callinghandler")]
    public class CallingHandlerController : ApiController
    {
        /// <summary>
        /// Regular, this method has appropriate name, depend on service that's being called. 
        /// Method usually has it's own parametars
        /// In order to keep method name more generally, I decided to give this method name 'CallHandler'
        /// I didn't use parametars because this structure is convinient for calling any institution service
        /// Parametars are set in service client ex: client.GetPropertyList("mio", "katastarservis", "1", "1", "1");
        /// </summary>
        [HttpGet]
        [Route("callhandler")]
        public string CallHandler()
        {
            string responseFromServer;
            try
            {
                //var request = (HttpWebRequest)WebRequest.Create("http://localhost/ExternalHandler/external/AKN/PropertyList");
                //request.Method = "GET";
                //request.ContentType = "application/x-www-form-urlencoded";
                //WebResponse response = request.GetResponse();
                //Stream dataStream = response.GetResponseStream();
                //if (dataStream != null)
                //{
                //    var reader = new StreamReader(dataStream);
                //    responseFromServer = reader.ReadToEnd();
                //    reader.Close();
                //    dataStream.Close();
                //    response.Close();
                //}

                var client = new AKNPropertyListTest.PropertyListClient();
                var result = client.GetPropertyList("mio", "katastarservis", "1", "1", "1");
                responseFromServer = result.message;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                Dispose();
            }
            //catch (WebException webException)
            //{
            //    WebResponse errResponse = webException.Response;
            //    string fullErrorFromHandler = string.Empty;
            //    using (Stream faultFromAnotherSide = errResponse.GetResponseStream())
            //    {
            //        if (faultFromAnotherSide != null)
            //        {
            //            var reader = new StreamReader(faultFromAnotherSide);
            //            fullErrorFromHandler = reader.ReadToEnd();
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(fullErrorFromHandler))
            //    {
            //        throw new Exception();
            //    }

            //}
            return responseFromServer;
        }
    }
}
