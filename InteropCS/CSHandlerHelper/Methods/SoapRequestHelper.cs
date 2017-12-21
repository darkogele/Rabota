using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using CSHandlerHelper.Contracts;
using CSHandlerHelper.Model;
using CSHandlerHelper.SOAP;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models.Models;

namespace CSHandlerHelper.Methods
{
    public class SoapRequestHelper : ISoapRequestHelper
    {
        public ResponseInteropCommunication Execute(string req, string contentType, string url)
        {
            var btResponse = new ResponseInteropCommunication();

            HttpWebRequest request = CreateWebRequest(contentType, url);
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(req);

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                btResponse.MimeType = response.ContentType;
                btResponse.StatusCode = response.StatusCode;
                using (StreamReader rd = new StreamReader(stream: response.GetResponseStream()))
                {
                    btResponse.Response = rd.ReadToEnd();
                    return btResponse;
                }
            }
        }

        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public HttpWebRequest CreateWebRequest(string contentType, string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.Headers.Add(@"SOAPAction", soapAction);
            webRequest.ContentType = contentType; // "application/soap+xml; charset=\"utf-8\"";
            webRequest.Accept = "application/soap+xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        public SoapMessage UnwrapMimMessage(string mimMessage)
        {
            XDocument xDoc = XDocument.Load(new StringReader(mimMessage));

            //var mimMessage = unwrappedResponse.Element((XNamespace) "http://www.slss.hr/" + "ReveiveMIMMsg").Element((XNamespace) "http://schemas.slss.hr/MIMMsg" + "MimMessage");

            XmlSerializer serializer = new XmlSerializer(typeof(SoapMessage));
            SoapMessage soapMsg;
            using (TextReader reader = new StringReader(xDoc.ToString()))
            {
                soapMsg = (SoapMessage)serializer.Deserialize(reader);
            }
            return soapMsg;
        }

        public SoapFaultMessage UnwrapSoapFaultMessage(string soapFaultMessage)
        {
            XDocument xDoc = XDocument.Load(new StringReader(soapFaultMessage));

            //var mimMessage = unwrappedResponse.Element((XNamespace) "http://www.slss.hr/" + "ReveiveMIMMsg").Element((XNamespace) "http://schemas.slss.hr/MIMMsg" + "MimMessage");

            XmlSerializer serializer = new XmlSerializer(typeof(SoapFaultMessage));
            SoapFaultMessage soapMsg;
            using (TextReader reader = new StringReader(xDoc.ToString()))
            {
                soapMsg = (SoapFaultMessage)serializer.Deserialize(reader);
            }
            return soapMsg;
        }

        public bool ValidateSignature(string mimMessage, string publicKey, ILogger _logger)
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(mimMessage);
            csp = new RSACryptoServiceProvider();
            csp.FromXmlString(publicKey);
            try
            {
                if (doc == null) throw new ArgumentException("Doc");
                if (csp == null) throw new ArgumentException("Key");
            }
            catch (Exception e)
            {
                _logger.Info(e.Message + "==== Null XMLDocument or CryptoServiceProvider!!!", "Request");
            }

            SignedXml signedXml = new SignedXml(doc);
            XmlNodeList nodeList = doc.GetElementsByTagName("Signature");
            try
            {
                if (nodeList.Count <= 0) throw new CryptographicException("Verification failed: No Signature was found in the document.");
                if (nodeList.Count >= 2) throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }
            catch (Exception e)
            {
                _logger.Info(e.Message + "==== none or multiple Signature tags!!!", "Request");
            }
            signedXml.LoadXml((XmlElement)nodeList[0]);
            return signedXml.CheckSignature(csp);
        }
        public SoapFault CreateSoapFaultDB(Guid tId, string code, string subCode, string details, string reason)
        {
            return new SoapFault()
            {
                TransactionId = tId,
                Code = code,
                DateCreated = DateTime.Now,
                DateOccured = DateTime.Now,
                Details = details,
                Reason = reason,
                SubCode = subCode
            };
        }

        public string BTsoapFault(string btError)
        {

            string output = "";
            var errorMessage = btError.Split(';');

            // BizzTalk error for SoapFault message handling 
            Dictionary<string, string> errors = new Dictionary<string, string>();

            errors.Add("AccessMap=False", "Не дозволен пристап до соодветниот учесник");
            errors.Add("Status=0", "Сервисот не е активен");
            errors.Add("IsActive=False", "Учесникот не е активен");

            // SoapFault message error handling
            foreach (var btr in errors)
            {
                var results = Array.FindAll(errorMessage, s => s.Equals(btr.Key));
                foreach (string result in results)
                {
                    // Output message
                    output += errors[result] + Environment.NewLine;
                }
            }

            return output;
        }
    }
}
