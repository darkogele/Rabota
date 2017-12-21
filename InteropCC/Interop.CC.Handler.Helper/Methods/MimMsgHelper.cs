using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Encryption;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Models.Models;
using Interop.CC.Models.Repository;
using Interop.CC.Models;
using Interop.CC.Models.UoW;
using Newtonsoft.Json;
using System.Linq;
using Interop.CC.CrossCutting;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Handler.Helper.SOAP;

namespace Interop.CC.Handler.Helper.Methods
{
    public class MimMsgHelper :IMimMsgHelper
    {
        // Опис: Метод за креирање на MIM порака за повик 
        // Влезни параметри: UrlSegment urlSegments, податочни вредности transactionID
        // Излезни параметри: SoapMessage модел
        public SoapMessage CreateMimRequestMsg(UrlSegment urlSegments, string transactionID)
        {
            
            return new SoapMessage
            {
                Header = new Header
                {
                    MimHeader = new MimHeader
                    {
                        id = "MIMHeader",
                        Consumer = urlSegments.Consumer,
                        Provider = String.Empty,
                        RoutingToken = urlSegments.RoutingToken,
                        Service = urlSegments.Service,
                        ServiceMethod = string.Empty,
                        //TransactionId = Guid.NewGuid().ToString(),
                        TransactionId = transactionID,
                        Dir = "Request",
                        PublicKey = string.Empty,
                        MimeType = String.Empty,
                        //TimeStamp = datetimeTS,
                        CorrelationID = String.Empty,
                        CallType = urlSegments.Async ? MimHeaderCallType.asynchronous : MimHeaderCallType.synchronous,
                        //Signature = new MimSignature()
                    },
                    MimAdditionalHeader = new MimAdditionalHeader()
                    {
                        Status = String.Empty,
                        StatusMessage = String.Empty,
                        ProviderEndpointUrl = String.Empty,
                        ExternalEndpointUrl = String.Empty,
                        WebServiceUrl = String.Empty,
                        IsInteropTestCommunicationCall = urlSegments.IsInteropTestCommunicationCall,
                        ConsumerBusCode =""// AppSettings.Get<string>("KingExternalBusCode"),
                        //TimeStampToken = resultGenerateTS
                    },
                    CryptoHeader = new CryptoHeader()
                    {
                        Key = string.Empty,
                        InitializationVector = string.Empty,
                        FormatValue = "AES"
                    }
                },
                Body = new Body()
                {
                    MimBody = new MimBody()
                    {
                        id = "MIMBody",
                        Message = string.Empty
                    }
                }

            };
        }

        // Опис: Метод за креирање на MIM порака за одговор 
        // Влезни параметри: SoapMessage mimMsg, mimeType
        // Излезни параметри: SoapMessage модел
        public SoapMessage CreateMimResponseMsg(SoapMessage mimMsg, string mimeType)
        {

            return new SoapMessage()
            {
                Header = new Header()
                {
                    MimHeader = new MimHeader()
                    {
                        id = "MIMHeader",
                        Consumer = mimMsg.Header.MimHeader.Consumer,
                        Provider = AppSettings.Get<string>("ParticipantCode"),
                        RoutingToken = mimMsg.Header.MimHeader.RoutingToken,
                        Service = mimMsg.Header.MimHeader.Service,
                        ServiceMethod = mimMsg.Header.MimHeader.ServiceMethod,
                        TransactionId = mimMsg.Header.MimHeader.TransactionId,
                        Dir = "Response",
                        PublicKey = mimMsg.Header.MimHeader.PublicKey,
                        MimeType = mimeType,
                      //  TimeStamp = datetimeTS,
                        CorrelationID = String.Empty,
                        CallType = mimMsg.Header.MimHeader.CallType,
                        //Signature = new MimSignature()
                    },
                    MimAdditionalHeader = new MimAdditionalHeader()
                    {
                        Status = "200",
                        StatusMessage = "OK",
                        ProviderEndpointUrl = String.Empty,
                        ExternalEndpointUrl = String.Empty,
                        WebServiceUrl = String.Empty,
                        ConsumerBusCode = mimMsg.Header.MimAdditionalHeader.ConsumerBusCode, 
                        IsInteropTestCommunicationCall = mimMsg.Header.MimAdditionalHeader.IsInteropTestCommunicationCall
                      //  TimeStampToken = resultGenerateTS

                    },
                    CryptoHeader = new CryptoHeader()
                    {
                        //Key = sessionKey,
                        //InitializationVector = iVector,
                        FormatValue = "AES"
                    }
                },
                Body = new Body()
                {
                    MimBody = new MimBody()
                    {
                        id = "MIMBody",
                        //Message = soapBody
                    }
                }

            };
        }

        // Опис: Метод за креирање на MIM потпишана порака 
        // Влезни параметри: SoapMessage soapmsg, OwnCertificate ownCert, ILogger _logger
        // Излезни параметри: податочен тип string
        public string CreateMimSignedXmlMsg(SoapMessage soapmsg, OwnCertificate ownCert, ILogger _logger)
        {
            var doc = CreateMimXmlMsg(soapmsg);
            return SignXml(doc, ownCert.Certificate, _logger);
        }

        // Опис: Метод за енкрипција на телото на Soap пораката
        // Влезни параметри: податочна вредност original, RsaWithRsaParameterKey keys
        // Излезни параметри: EncryptedPacket модел
        public EncryptedPacket EncryptSoapBody(string original, RsaWithRsaParameterKey keys)
        {
            var hybrid = new HybridEncryption();
            var encryptedBlock = hybrid.EncryptData(Encoding.UTF8.GetBytes(original), keys);
            return encryptedBlock;
        }

        // Опис: Метод за декрипција на телото на Soap пораката
        // Влезни параметри: поворка бајти encryptedData, sessionKey, iVector, RsaWithRsaParameterKey rsaParams
        // Излезни параметри: податочен тип string 
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

        // Опис: Метод за вчитување на сертификат
        // Влезни параметри: ILogger logger
        // Излезни параметри: OwnCertificate модел
        public OwnCertificate LoadOwnCertificate(ILogger logger)
        {
            OwnCertificate output = new OwnCertificate();
            string certPath = AppSettings.Get<string>("MyCertificatePath");
            string certPass = AppSettings.Get<string>("MyCertificatePassword");
            var certUser = AppSettings.Get<string>("MyCertificateName");
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            
            bool getFromStore = Boolean.Parse(AppSettings.Get<string>("CertificateFromStore"));
            try
            {
                if(getFromStore)
                {
                    output.Certificate = store.Certificates
                    .Find(X509FindType.FindBySubjectName, certUser, false)
                    .OfType<X509Certificate2>()
                    .First();
                }
                else
                {
                    output.Certificate = new X509Certificate2(certPath, certPass, X509KeyStorageFlags.Exportable);
                }
               
            }
            catch (Exception e)
            {
                //LogHelper.WriteInNLoc("B", "WE", e.Message + "=====" + pass, "Request_" + DateTime.Now, "Info");
                logger.Error("LoadOwnCertificate", e);
                logger.Info(e.Message + "=====" + certUser + "====" + StoreLocation.LocalMachine, "Request");
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Convert.ToBase64String(output.Certificate.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
            var stringBuilder = builder.ToString();
            output.CertString = stringBuilder;
            output.PublicKey = output.Certificate.PublicKey.Key.ToXmlString(false);
            var rsa = (RSACryptoServiceProvider)output.Certificate.PrivateKey;
            output.PrivateKey = rsa.ExportParameters(true);
            return output;
        }

        // Опис: Метод за екстракција и вчитување на јавен клуч од сертификат за соодветен провајдер
        // Влезни параметри: податочна вредност routingToken
        // Излезни параметри: PublicKeyClass модел
        public PublicKeyClass GetPublicKeyForProvider(string routingToken)
        {
            var providersRepository = new ProvidersRepository(new UnitOfWork(new InteropContext()));
            var cerDb = providersRepository.GetPublicKey(routingToken);

            var cert = new X509Certificate2(Convert.FromBase64String(cerDb));
            var publicKeyCert = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var publicKeyString = publicKeyCert.ToXmlString(false);

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

            var publicKey = new PublicKeyClass();
            publicKey.PublicKeyRsa = rsaParamKey;
            publicKey.PublicKeyString = publicKeyString;
            publicKey.CertString = cerDb;

            return publicKey;
        }

        // Опис: Метод за екстракција и вчитување на јавен клуч од сертификат кој е во форма на податочен тип string
        // Влезни параметри: податочна вредност certString
        // Излезни параметри: RsaWithRsaParameterKey модел 
        public RsaWithRsaParameterKey GetPublicKeyFromString(string certString)
        {
            var cert = new X509Certificate2(Convert.FromBase64String(certString));
            var publicKeyCert = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var publicKeyString = publicKeyCert.ToXmlString(false);

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

            return rsaParamKey;
        }

        // Опис: Метод за екстракција и вчитување на приватен клуч од сертификат кој е во форма на податочен тип string
        // Влезни параметри: податочна вредност certString
        // Излезни параметри: RsaWithRsaParameterKey модел 
        public RsaWithRsaParameterKey GetPrivateKey(RSAParameters privateKeyRsaParams)
        {
            var rsaParamKey = new RsaWithRsaParameterKey();
            rsaParamKey.PrivateKey = privateKeyRsaParams;
            return rsaParamKey;
        }

        // Опис: Метод за креирање на MIM XML порака
        // Влезни параметри: SoapMessage mimMsg
        // Излезни параметри: XmlDocument модел 
        public  XmlDocument CreateMimXmlMsg(SoapMessage mimMsg)
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

        // Опис: Метод за креирање на SoapFault порака
        // Влезни параметри: SoapFaultMessage soapFault
        // Излезни параметри: XmlDocument модел 
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

        // Опис: Метод за креирање на SoapFault порака
        // Влезни параметри: podato;na vrednost code, subCode, mTime, text
        // Излезни параметри: SoapFaultMessage модел 
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

        // Опис: Метод за потпис на XML потпишана порака 
        // Влезни параметри: XmlDocument xmlDoc, X509Certificate2 cert, ILogger _logger
        // Излезни параметри: податочен тип string
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

            
            var reference = new Reference("#MIMHeader");
            var env = new XmlDsigEnvelopedSignatureTransform();
            var c14n = new XmlDsigC14NWithCommentsTransform();
            reference.AddTransform(env);
            reference.AddTransform(c14n);
            signedXml.AddReference(reference);
            var reference1 = new Reference("#MIMBody");
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
                _logger.Info(e.Message + "==== Non existing MIMBody or MIMHeader atributes!!!", "Request");
            }
            var xmlDigitalSignature = signedXml.GetXml();

            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("mioa", "http://mioa.gov.mk/interop/mim/v1");

            try
            {
                //var sig = xmlDoc.SelectSingleNode("//mioa:Signature", ns);
                var mimHeader = xmlDoc.SelectSingleNode("//mioa:MIMHeader", ns);
                mimHeader.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
               // mimHeader.RemoveChild(sig);
               // sig.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            }
            catch (Exception e)
            {
                _logger.Info(e.Message + "==== Non existing mioa:Signature tag!!!", "Request");
            }
            return xmlDoc.InnerXml;
        }

        // Опис: Метод за запишување на MIM пораката во база
        // Влезни параметри: SoapMessage mimMsg, ILogger _logger
        // Излезни параметри: / 
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
                Status = mimMsg.Header.MimAdditionalHeader.Status,
                Timestamp = DateTime.Now/*mimMsg.Header.MimHeader.TimeStamp*/,
                CreateDate = DateTime.Now,
                CorrelationId = mimMsg.Header.MimHeader.CorrelationID,
                Signature = "",
                TokenTimestamp = mimMsg.Header.MimAdditionalHeader.TimeStampToken,
                IsInteropTestCommunicationCall = mimMsg.Header.MimAdditionalHeader.IsInteropTestCommunicationCall
            };

            var context = new ValidationContext(mimMessageLog, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(mimMessageLog, context, results, true);

            var nameLogerError = mimMsg.Header.MimHeader.Dir;
            if (isValid)
            {
                try
                {
                    var messageLogsRepository = new MessageLogsRepository(new UnitOfWork(new InteropContext()));
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
                _logger.Error(JsonConvert.SerializeObject(mimMessageLog), "Validator = false" , "LogMimMessage" + nameLogerError);
            }
        }

        public void LogInitialMimMessage(SoapMessage mimMsg, ILogger _logger)
        {
            var mimMessageLog = new MessageLog
            {
                CallType = mimMsg.Header.MimHeader.CallType.ToString(),
                PublicKey = string.Empty,
                Consumer = mimMsg.Header.MimHeader.Consumer,
                Dir = mimMsg.Header.MimHeader.Dir,
                MimeType = string.Empty,
                TransactionId = new Guid(mimMsg.Header.MimHeader.TransactionId),
                Provider = mimMsg.Header.MimHeader.Provider,
                RoutingToken = mimMsg.Header.MimHeader.RoutingToken,
                Service = mimMsg.Header.MimHeader.Service,
                ServiceMethod = string.Empty,
                Status = string.Empty,
                Timestamp = DateTime.Now,
                CreateDate = DateTime.Now,
                CorrelationId = string.Empty,
                Signature = "",
                TokenTimestamp = string.Empty,
                IsInteropTestCommunicationCall = mimMsg.Header.MimAdditionalHeader.IsInteropTestCommunicationCall
            };

            var context = new ValidationContext(mimMessageLog, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(mimMessageLog, context, results, true);

            var nameLogerError = mimMsg.Header.MimHeader.Dir;
            if (isValid)
            {
                try
                {
                    var messageLogsRepository = new MessageLogsRepository(new UnitOfWork(new InteropContext()));
                    messageLogsRepository.InsertMessageLog(mimMessageLog);
                }
                catch (Exception e)
                {
                    //LogHelper.WriteInNLoc("", "", JsonConvert.SerializeObject(mimMessageLog) + "------------" + e.Message, nameLogerError);
                    _logger.Error(JsonConvert.SerializeObject(mimMessageLog) + "------------" + e.Message, e,
                        "LogMimMessage" + nameLogerError);
                }
            }
            else
            {
                //LogHelper.WriteInNLoc("", "", JsonConvert.SerializeObject(mimMessageLog), nameLogerError);
                _logger.Error(JsonConvert.SerializeObject(mimMessageLog), "Validator = false", "LogMimMessage" + nameLogerError);
            }
        }

        //public SoapMessage CreateMimRequestMsg(UrlSegment urlSegments, string transactionID)
        //{
        //    return new SoapMessage
        //    {
        //        Header = new Header
        //        {
        //            MimHeader = new MimHeader
        //            {
        //                Consumer = urlSegments.Consumer,
        //                TransactionId = transactionID,
        //                Provider = String.Empty,
        //                RoutingToken = urlSegments.RoutingToken,
        //                Service = urlSegments.Service,
        //                CallType = urlSegments.Async ? MimHeaderCallType.asynchronous : MimHeaderCallType.synchronous,
        //                Dir = "Request",
        //            },
        //            MimAdditionalHeader = new MimAdditionalHeader
        //            {
        //                IsInteropTestCommunicationCall = urlSegments.IsInteropTestCommunicationCall,
        //            },
        //            CryptoHeader = new CryptoHeader()
        //        },
        //        Body = new Body
        //        {
        //            MimBody = new MimBody
        //            {
        //                id = "MIMBody"
        //            }
        //        }
        //    };
        //}
    }
}