using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using CrossCutting.Logging;
using Helpers.Contracts;
using Helpers.Models;

namespace Helpers.Implementations
{
    public class SoapRequestHelper: ISoapRequestHelper
    {
        private readonly ILogger _logger;
        public SoapRequestHelper(ILogger logger)
        {
            _logger = logger;
        }
        public SoapMessage UnwrapMimMessage(string mimMessage)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                XDocument xDoc = XDocument.Load(new StringReader(mimMessage));
                var serializer = new XmlSerializer(typeof(SoapMessage));
                SoapMessage soapMsg;
                using (TextReader reader = new StringReader(xDoc.ToString()))
                {
                    soapMsg = (SoapMessage)serializer.Deserialize(reader);
                }
                return soapMsg;
            }, TypeException.DefaultException, _logger);

            return result;
        }

        public bool ValidateSignature(string mimMessage, string publicKey)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                var csp = new RSACryptoServiceProvider();
                var doc = new XmlDocument();
                doc.LoadXml(mimMessage);
                var cert = new X509Certificate2(Convert.FromBase64String(publicKey));
                var publicKeyCert = (RSACryptoServiceProvider)cert.PublicKey.Key;
                var publicKeyString = publicKeyCert.ToXmlString(false);
                csp = new RSACryptoServiceProvider();
                csp.FromXmlString(publicKeyString);
               
                var signedXml = new SignedXml(doc);
                XmlNodeList nodeList = doc.GetElementsByTagName("Signature");
                if (nodeList.Count <= 0) throw new CryptographicException("Verification failed: No Signature was found in the document.");
                
                if (nodeList.Count > 1)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        var parentNode = nodeList[i].ParentNode;
                        if (parentNode != null && parentNode.Name == "MIMHeader")
                        {
                            signedXml.LoadXml((XmlElement)nodeList[i]);
                        }
                    }
                }
                else
                    signedXml.LoadXml((XmlElement)nodeList[0]);
                return signedXml.CheckSignature(csp);
            },TypeException.DefaultException,_logger);

            return result;
        }
    }
}
