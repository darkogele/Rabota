using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CSHandlerHelper.Contracts;
using CSHandlerHelper.Model;
using CSHandlerHelper.SOAP;
using Interop.CS.CrossCutting;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Encryption;
using Interop.CS.Models;
using Interop.CS.Models.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.UoW;
using Newtonsoft.Json;

namespace CSHandlerHelper.Methods
{
    public class MimMsgHelper : IMimMsgHelper
    {
        public SoapMessage CreateMimRequestMsg(UrlSegment urlSegments, string transactionID, string soapAction, string soapBody, string sessionKey, string iVector, string soapMethodName, OwnCertificate ownCert)
        {
            return new SoapMessage()
            {
                Header = new Header()
                {
                    MimHeader = new MimHeader()
                    {
                        id = "Header",
                        Consumer = urlSegments.Consumer,
                        Provider = String.Empty,
                        RoutingToken = urlSegments.RoutingToken,
                        Service = urlSegments.Service,
                        ServiceMethod = soapMethodName,
                        //TransactionId = Guid.NewGuid().ToString(),
                        TransactionId = transactionID,
                        Dir = "Request",
                        PublicKey = ownCert.PublicKey,
                        MimeType = String.Empty,
                        TimeStamp = DateTime.Now,
                        CorrelationID = String.Empty,
                        CallType = urlSegments.Async ? MimHeaderCallType.asynchronous : MimHeaderCallType.synchronous,
                        Signature = new MimSignature()
                    },
                    MimAdditionalHeader = new MimAdditionalHeader()
                    {
                        Status = String.Empty,
                        StatusMessage = String.Empty,
                        ProviderEndpointUrl = String.Empty,
                        ExternalEndpointUrl = String.Empty,
                        WebServiceUrl = String.Empty

                    },
                    CryptoHeader = new CryptoHeader()
                    {
                        Key = sessionKey,
                        InitializationVector = iVector,
                        FormatValue = "AES"
                    }
                },
                Body = new Body()
                {
                    MimBody = new MimBody()
                    {
                        id = "Body",
                        Message = soapBody
                    }
                }

            };
        }
        public SoapMessage CreateMimResponseMsg(SoapMessage mimMsg, string soapBody, string mimeType, string sessionKey, string iVector, OwnCertificate ownCert)
        {

            return new SoapMessage()
            {
                Header = new Header()
                {
                    MimHeader = new MimHeader()
                    {
                        id = "Header",
                        Consumer = mimMsg.Header.MimHeader.Consumer,
                        Provider = AppSettings.Get<string>("ParticipantCode"),
                        RoutingToken = mimMsg.Header.MimHeader.RoutingToken,
                        Service = mimMsg.Header.MimHeader.Service,
                        ServiceMethod = mimMsg.Header.MimHeader.ServiceMethod,
                        TransactionId = mimMsg.Header.MimHeader.TransactionId,
                        Dir = "Response",
                        PublicKey = ownCert.PublicKey,
                        MimeType = mimeType,
                        TimeStamp = DateTime.Now,
                        CorrelationID = String.Empty,
                        CallType = mimMsg.Header.MimHeader.CallType,
                        Signature = new MimSignature()
                    },
                    MimAdditionalHeader = new MimAdditionalHeader()
                    {
                        Status = "200",
                        StatusMessage = "OK",
                        ProviderEndpointUrl = String.Empty,
                        ExternalEndpointUrl = String.Empty,
                        WebServiceUrl = String.Empty

                    },
                    CryptoHeader = new CryptoHeader()
                    {
                        Key = sessionKey,
                        InitializationVector = iVector,
                        FormatValue = "AES"
                    }
                },
                Body = new Body()
                {
                    MimBody = new MimBody()
                    {
                        id = "Body",
                        Message = soapBody
                    }
                }

            };
        }
        public string CreateMimSignedXmlMsg(SoapMessage mimMsg, OwnCertificate ownCert, ILogger _logger)
        {
            var doc = CreateMimXmlMsg(mimMsg);
            return SignXml(doc, ownCert.Certificate, _logger);
        }
        public EncryptedPacket EncryptSoapBody(string original, RsaWithRsaParameterKey keys)
        {
            var hybrid = new HybridEncryption();
            var encryptedBlock = hybrid.EncryptData(Encoding.UTF8.GetBytes(original), keys);
            return encryptedBlock;
        }
        public string DecryptSoapBody(byte[] encryptedData, byte[] sessionKey, byte[] iVector, RsaWithRsaParameterKey rsaParams)
        {
            var encryptedBlock = new EncryptedPacket();
            encryptedBlock.EncryptedData = encryptedData;
            encryptedBlock.EncryptedSessionKey = sessionKey;
            encryptedBlock.Iv = iVector;
            encryptedBlock.RsaParams = rsaParams;

            var hybrid = new HybridEncryption();
            var decrpyted = hybrid.DecryptData(encryptedBlock, encryptedBlock.RsaParams);
            var decryptedString = Encoding.UTF8.GetString(decrpyted);
            return decryptedString;
        }
        public OwnCertificate LoadOwnCertificate(ILogger logger)
        {
            OwnCertificate output = new OwnCertificate();
            string certPath = AppSettings.Get<string>("MyCertificate");
            string certPass = AppSettings.Get<string>("MyCertificatePassword");
            try
            {
                output.Certificate = new X509Certificate2(certPath, certPass, X509KeyStorageFlags.Exportable);
            }
            catch (Exception e)
            {
                //LogHelper.WriteInNLoc("B", "WE", e.Message + "=====" + pass, "Request_" + DateTime.Now, "Info");
                logger.Info(e.Message + "=====" + certPath + "====" + certPass, "Request");
            }
            output.PublicKey = output.Certificate.PublicKey.Key.ToXmlString(false);
            var rsa = (RSACryptoServiceProvider)output.Certificate.PrivateKey;
            output.PrivateKey = rsa.ExportParameters(true);
            return output;
        }
        public RsaWithRsaParameterKey GetKeys(string routingToken, OwnCertificate ownCert)
        {
            //var participantRepo = new ParticipantRepository(new UnitOfWork(new InteropContext()));
            //var publicKeyString = participantRepo.GetPublicKey(routingToken);
            var publicKeyString =
                "<RSAKeyValue><Modulus>ks+L8kWHiBwiPw4zJcZwIkeGrhNP0fI6LohybpGjNoZSf4bZ1hXrgLiWoklA2QY7CD7hPbW2d1cLVK7VOAYqAtyIdrchG6AVSWg2ul90QT/BgvNFcBqf9xuS3l25t1OimUcj47/hPx2Nu9NMMMpGhqp6PR2pEwjvMAxHgW7BzOM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var fromModulus = publicKeyString.IndexOf("<Modulus>") + "<Modulus>".Length;
            var toModulus = publicKeyString.LastIndexOf("</Modulus>");
            var modulus = publicKeyString.Substring(fromModulus, toModulus - fromModulus);
            var fromExponent = publicKeyString.IndexOf("<Exponent>") + "<Exponent>".Length;
            var toExponent = publicKeyString.LastIndexOf("</Exponent>");
            var exponent = publicKeyString.Substring(fromExponent, toExponent - fromExponent);

            var rsaParam = new RSAParameters();
            rsaParam.Modulus = Convert.FromBase64String(modulus);
            rsaParam.Exponent = Convert.FromBase64String(exponent);

            var rsaParamKey = new RsaWithRsaParameterKey();
            rsaParamKey.PublicKey = rsaParam;
            rsaParamKey.PrivateKey = ownCert.PrivateKey;

            return rsaParamKey;
        }
        public XmlDocument CreateMimXmlMsg(SoapMessage mimMsg)
        {
            //string mimMsgXmlEncoded;
            //using (var stream = new MemoryStream())
            //{
            //    using (var writer = XmlWriter.Create(stream))
            //    {
            //        new XmlSerializer(mimMsg.GetType()).Serialize(writer, mimMsg);
            //        mimMsgXmlEncoded = Encoding.UTF8.GetString(stream.ToArray());
            //    }
            //}
            XmlSerializerNamespaces soapNS = new XmlSerializerNamespaces();
            soapNS.Add("", "http://www.slss.hr/");
            soapNS.Add("mioa", "http://mioa.gov.mk/interop/mim/v1");
            soapNS.Add("xs", "http://www.w3.org/2001/XMLSchema");
            soapNS.Add("soap", "http://www.w3.org/2003/05/soap-envelope");

            // Represents an XML document
            var xmlDoc = new XmlDocument();
            // Initializes a new instance of the XmlDocument class.          
            var xmlSerializer = new XmlSerializer(typeof(SoapMessage));
            // Creates a stream whose backing store is memory. 
            using (var xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, mimMsg, soapNS);
                xmlStream.Position = 0;
                // Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);

                foreach (XmlNode node in xmlDoc)
                    if (node.NodeType == XmlNodeType.XmlDeclaration)
                        xmlDoc.RemoveChild(node);

                // var mimNode = xmlDoc.DocumentElement;
                //mimNode.RemoveAllAttributes();
                // mimNode.SetAttribute("xmlns","http://schemas.slss.hr/MIMMsg");

                //                xmlDoc.LoadXml(@"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                //                                                       <soap:Body> <ReveiveMIMMsg xmlns=""http://www.slss.hr/"">" + xmlDoc.InnerXml + "</ReveiveMIMMsg></soap:Body>" + "" +
                //                               "</soap:Envelope>");//Soap msg for BizTalk
                return xmlDoc;
            }
        }

        public string SignXml(XmlDocument xmlDoc, X509Certificate2 cert, ILogger _logger)
        {

            var Key = (RSACryptoServiceProvider)cert.PrivateKey;
            // Check arguments. 
            try
            {
                if (xmlDoc == null)
                    throw new ArgumentException("xmlDoc");
                if (cert == null)
                    throw new ArgumentException("Certificate");
            }
            catch (Exception e)
            {
                _logger.Info(e.Message, "Request");
            }

            var signedXml = new SignedXml(xmlDoc);

            var XMLSignature = signedXml.Signature;
            signedXml.SigningKey = Key;


            var reference = new Reference("#Header");
            var env = new XmlDsigEnvelopedSignatureTransform();
            var c14n = new XmlDsigC14NWithCommentsTransform();
            reference.AddTransform(env);
            reference.AddTransform(c14n);
            signedXml.AddReference(reference);
            var reference1 = new Reference("#Body");
            var env1 = new XmlDsigEnvelopedSignatureTransform();
            var c14n1 = new XmlDsigC14NWithCommentsTransform();
            reference1.AddTransform(env1);
            reference1.AddTransform(c14n1);
            signedXml.AddReference(reference1);

            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));

            XMLSignature.KeyInfo = keyInfo;
            try
            {
                signedXml.ComputeSignature();
            }
            catch (Exception e)
            {
                _logger.Info(e.Message + "==== Non existing Body or Header atributes!!!", "Request");
            }
            var xmlDigitalSignature = signedXml.GetXml();

            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("mioa", "http://mioa.gov.mk/interop/mim/v1");

            try
            {
                var sig = xmlDoc.SelectSingleNode("//mioa:Signature", ns);
                sig.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            }
            catch (Exception e)
            {
                _logger.Info(e.Message + "==== Non existing mioa:Signature tag!!!", "Request");
            }
            return xmlDoc.InnerXml;
        }

        public XmlDocument CreateFaultMessage(SoapFaultMessage soapFault)
        {
            XmlSerializerNamespaces soapNS = new XmlSerializerNamespaces();
            soapNS.Add("m", "http://www.example.org/timeouts");
            soapNS.Add("xml", "http://www.w3.org/XML/1998/namespace");
            soapNS.Add("env", "http://www.w3.org/2003/05/soap-envelope");

            // Represents an XML document
            var xmlDoc = new XmlDocument();
            // Initializes a new instance of the XmlDocument class.          
            var xmlSerializer = new XmlSerializer(typeof(SoapFaultMessage));
            // Creates a stream whose backing store is memory. 
            using (var xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, soapFault, soapNS);
                xmlStream.Position = 0;
                // Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);

                foreach (XmlNode node in xmlDoc)
                    if (node.NodeType == XmlNodeType.XmlDeclaration)
                        xmlDoc.RemoveChild(node);

                return xmlDoc;
            }
        }
        public SoapFaultMessage CreateSoapFault(string code, string subCode, string mTime, string text)
        {
            return new SoapFaultMessage
            {
                Body = new FaultBody
                {
                    Fault = new Fault
                    {
                        Code = new Code { Subcode = new Subcode { value = subCode }, value = code },
                        Detail = new Detail { maxTime = mTime },
                        Reason = new Reason { Text = new Text { value = text } }
                    }
                }
            };
        }

        public void LogMimMessage(SoapMessage mimMsg, ILogger _logger)
        {
            var mimMessageLog = new MessageLog
            {
                CallType = mimMsg.Header.MimHeader.CallType.ToString(),
                PublicKey = mimMsg.Header.MimHeader.PublicKey,
                Consumer = mimMsg.Header.MimHeader.Consumer,
                Dir = mimMsg.Header.MimHeader.Dir,
                MimeType = mimMsg.Header.MimHeader.MimeType,
                TransactionId = new Guid(mimMsg.Header.MimHeader.TransactionId),
                Provider = mimMsg.Header.MimHeader.Provider,
                RoutingToken = mimMsg.Header.MimHeader.RoutingToken,
                Service = mimMsg.Header.MimHeader.Service,
                ServiceMethod = mimMsg.Header.MimHeader.ServiceMethod,
                Status = mimMsg.Header.MimHeader.Service,
                Timestamp = mimMsg.Header.MimHeader.TimeStamp,
                CreateDate = DateTime.Now,
                CorrelationId = mimMsg.Header.MimHeader.CorrelationID,
                Signature = ""
            };

            var context = new ValidationContext(mimMessageLog, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(mimMessageLog, context, results, true);

            var nameLogerError = mimMsg.Header.MimHeader.Dir;
            if (isValid)
            {
                try
                {
                    var messageLogsRepository = new MessageLogRepository(new UnitOfWork(new InteropContext()));
                    messageLogsRepository.InsertMessageLog(mimMessageLog);
                }
                catch (Exception e)
                {
                    //LogHelper.WriteInNLoc("", "", JsonConvert.SerializeObject(mimMessageLog) + "------------" + e.Message, nameLogerError);
                    _logger.Error(JsonConvert.SerializeObject(mimMessageLog) + "------------" + e.Message, e, "LogMimMessage" + nameLogerError);
                }
            }
            else
            {
                //LogHelper.WriteInNLoc("", "", JsonConvert.SerializeObject(mimMessageLog), nameLogerError);
                _logger.Error(JsonConvert.SerializeObject(mimMessageLog), "Validator = false", "LogMimMessage" + nameLogerError);
            }
        }
    }
}
