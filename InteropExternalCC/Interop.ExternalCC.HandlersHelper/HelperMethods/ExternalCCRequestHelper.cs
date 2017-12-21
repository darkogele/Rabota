using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Interop.ExternalCC.CrossCutting;
using Interop.ExternalCC.CrossCutting.Logging;
using Interop.ExternalCC.HandlersHelper.Exceptions;
using Interop.ExternalCC.HandlersHelper.SOAP;
using Newtonsoft.Json;
using NLog.Layouts;

namespace Interop.ExternalCC.HandlersHelper.HelperMethods
{
    public class ExternalCCRequestHelper : IExternalCCRequestHelper
    {
        public string GetParticipantCode(string soapBody)
        {
            var xDoc = XDocument.Parse(soapBody);
            var participantCode = xDoc.Descendants().SingleOrDefault(x => x.Name.LocalName == "RoutingToken");
            if (participantCode == null)
            {
                throw new NotFoundParticipantCodeException();
            }
            return participantCode.Value;
        }

        public Dictionary<string, string> GetAllParticipantsUri()
        {
            WebRequest req = WebRequest.Create("http://localhost/Interop.CS.Portal.API/api/External/GetAllParticipantsUri");
            req.Proxy = new WebProxy("localhost", true);
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            var responseString = sr.ReadToEnd();
            var responseJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
            return responseJson;
        }

        public string GetParticipantUri(string participantCode)
        {
            var url = "http://localhost/Interop.CS.Portal.API/api/External/GetParticipantUri?participantCode=" + participantCode;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = AppSettings.Get<string>("WebRequestMethod");
            Stream dataStream = request.GetRequestStream();
            WebResponse response = request.GetResponse();
            var status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            if (responseFromServer == null)
            {
                throw new NotFoundParticipantUriException(participantCode);
            }

            var responseToken = responseFromServer;
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseToken.ToString();
        }

        public string ResentExternalCCRequest(string soapAction, string soapBody, string url)
        {
            var urlToCentralServer = url;
            HttpWebRequest request = CreateWebRequest(soapAction, urlToCentralServer);
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(soapBody);

            var nameLoger = "soapEnvelopeXml_" + DateTime.Now;
            using (var logger = LoggingFactory.GetNLogger(nameLoger))
            {
                logger.Info(soapEnvelopeXml.InnerText);
                logger.Info(soapEnvelopeXml.InnerXml);
                logger.Info(soapEnvelopeXml.OuterXml);
            }

            //MNOGU BITNO, NE TRGAJ ZA DA POMINE NA BIZTALK, PROBLEM KAKO NA http://stackoverflow.com/questions/19258810/xmldocument-save-adds-return-carriages-to-xml-when-elements-are-blank
            var settings = new XmlWriterSettings { Indent = true };
            XmlWriter writer = XmlWriter.Create(request.GetRequestStream(), settings);

            //using (Stream stream = request.GetRequestStream())
            //{
            soapEnvelopeXml.Save(writer);
            //}
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(stream: response.GetResponseStream()))
                {
                    return rd.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest(string soapAction, string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);
            //webRequest.Headers.Add(@"SOAPAction", soapAction);
            //webRequest.ContentType = "text/xml; charset=\"utf-8\"";
            if (!string.IsNullOrEmpty(soapAction))
                webRequest.ContentType = "application/soap+xml; charset=utf-8; action=\"" + soapAction + "\"";
            else
                webRequest.ContentType = "application/soap+xml; charset=utf-8;";
            //webRequest.Accept = "text/xml";
            webRequest.Accept = AppSettings.Get<string>("WebRequestAccept");
            webRequest.Method = AppSettings.Get<string>("WebRequestMethod");
            return webRequest;
        }

        public SoapMessage UnwrapMimMessage(string mimMessage, string externalCode)
        {
            XDocument xDoc = XDocument.Load(new StringReader(mimMessage));

            XmlSerializer serializer = new XmlSerializer(typeof(SoapMessage));
            SoapMessage soapMsg;
            using (TextReader reader = new StringReader(xDoc.ToString()))
            {
                soapMsg = (SoapMessage)serializer.Deserialize(reader);
            }

            return soapMsg;
        }
    }
}
