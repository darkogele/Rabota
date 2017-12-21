using System;
using System.IO;
using System.Net;
using System.Web.Configuration;
using System.Web.Http;
using System.Xml;

namespace Interop.CC.Portal.API.Controllers
{
    [Authorize]
    public class InteropTestCommunicationController : ApiController
    {
        // Опис: Методот овозможува тестирање на комуникација
        // Влезни параметри: рутирачки токен за провајдер
        // Излезни параметри: податочен тип bool
        [HttpGet]
        public bool TestCommunication(string providerRoutingToken)
        {
            try
            {
                var result = ExecuteRequest(GetRequestUriString(providerRoutingToken));
                const string expectedResult = @"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope""><s:Body><InteropTestCommunicationService xmlns=""http://tempuri.org/"">InteropTestCommunicationCallResponse</InteropTestCommunicationService></s:Body></s:Envelope>";
                if (result == expectedResult)
                {
                    return true;
                }
                return false;;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // Опис: Методот овозможува вчитување на Uri за соодветниот повик
        // Влезни параметри: рутирачки токен за провајдер
        // Излезни параметри: податочен тип string
        private string GetRequestUriString(string providerRoutingToken)
        {
            return WebConfigurationManager.AppSettings["Interop.CC.Handler.ExternalPath"] + providerRoutingToken +
                   "/InteropTestCommunicationService/InteropTestCommunicationCall";
        }

        // Опис: Методот овозможува вчитување на Uri за соодветниот повик
        // Влезни параметри: Uri за соодветниот повик
        // Излезни параметри: податочен тип string
        private string ExecuteRequest(string requestUriString)
        {
            HttpWebRequest request = CreateWebRequest(requestUriString);
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope""><s:Body><InteropTestCommunicationService xmlns=""http://tempuri.org/""></InteropTestCommunicationService></s:Body></s:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            string soapResult;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }

            return soapResult;
        }

        // Опис: Методот овозможува креирање на Web повик
        // Влезни параметри: Uri за соодветниот повик
        // Излезни параметри: HttpWebRequest модел
        private HttpWebRequest CreateWebRequest(string requestUriString)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "application/soap+xml; charset=\"utf-8\"";
            webRequest.Accept = "application/soap+xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}
