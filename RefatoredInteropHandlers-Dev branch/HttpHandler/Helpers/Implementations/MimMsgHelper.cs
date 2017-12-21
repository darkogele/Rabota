using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CrossCutting.Logging;
using Helpers.Contracts;
using Helpers.Models;

namespace Helpers.Implementations
{
    public class MimMsgHelper :IMimMsgHelper
    {
        private readonly ILogger _logger;
        public MimMsgHelper(ILogger logger)
        {
            _logger = logger;
        }
        public SoapMessage CreateMimRequestMsg(UrlSegment urlSegments, string transactionId)
        {
            return new SoapMessage
            {
                Header = new Header
                {
                    MimHeader = new MimHeader
                    {
                        id = "MIMHeader",
                        Consumer = urlSegments.Consumer,
                        RoutingToken = urlSegments.RoutingToken,
                        Service = urlSegments.Service,
                        TransactionId = transactionId,
                        Dir = "Request",
                        CallType = urlSegments.Async ? MimHeaderCallType.asynchronous : MimHeaderCallType.synchronous,
                    },
                    MimAdditionalHeader = new MimAdditionalHeader
                    {
                        IsInteropTestCommunicationCall = urlSegments.IsInteropTestCommunicationCall
                    },
                    CryptoHeader = new CryptoHeader
                    {
                        FormatValue = "AES"
                    }
                },
                Body = new Body
                {
                    MimBody = new MimBody
                    {
                        id = "MIMBody"
                    }
                }
            };
        }

        public OwnCertificate LoadOwnCertificate()
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                var output = new OwnCertificate();
                string certPath = ConfigurationManager.AppSettings["MyCertificatePath"];
                string certPass = ConfigurationManager.AppSettings["MyCertificatePassword"];
                var certUser = ConfigurationManager.AppSettings["MyCertificateName"];
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);


                bool getFromStore = Boolean.Parse(ConfigurationManager.AppSettings["CertificateFromStore"]);

                if (getFromStore)
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
                var builder = new StringBuilder();
                builder.AppendLine(Convert.ToBase64String(output.Certificate.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
                output.CertString = builder.ToString();
                output.PublicKey = output.Certificate.PublicKey.Key.ToXmlString(false);
                var rsa = (RSACryptoServiceProvider)output.Certificate.PrivateKey;
                output.PrivateKey = rsa.ExportParameters(true);
                return output;
            },TypeException.DefaultException,_logger);

            return result;
            //try
            //{
            //}
            //catch (Exception exception)
            //{
            //    _logger.Error("Error happened in GetPublicKeyForProvider", exception.Message, "MimMsgHelper_LoadOwnCertificate");
            //    throw new Exception("Error in method LoadOwnCertificate: " + exception.Message);
            //}
        }

        public RsaWithRsaParameterKey GetPrivateKey(RSAParameters privateKeyRsaParams)
        {
            var rsaParamKey = new RsaWithRsaParameterKey {PrivateKey = privateKeyRsaParams};
            return rsaParamKey;
        }

        public PublicKeyClass GetPublicKeyForProvider(string routingToken)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                const string cerDb = "MIIFbzCCBFegAwIBAgIQcfFVjMoHX3YMSiPgvP2wJzANBgkqhkiG9w0BAQsFADBhMQswCQYDVQQGEwJNSzEXMBUGA1UEChMOS0lCUyBBRCBTa29wamUxHzAdBgNVBAsTFkZPUiBURVNUIFBVUlBPU0VTIE9OTFkxGDAWBgNVBAMTD0tJQlMgVEVTVCBDQSBHMzAeFw0xNTEyMjkwMDAwMDBaFw0xNjEyMjgyMzU5NTlaMIGfMQ8wDQYDVQQLFAZPZGRlbDExFTATBgNVBAoUDEluc3RpdHVjaWphMTEXMBUGA1UECxQORU1CUyAtIDU1Mjk1ODExHDAaBgNVBAsUE0VEQiAtIDQwMzAwMDE0MTYzMDkxCzAJBgNVBAYTAk1LMR8wHQYJKoZIhvcNAQkBFhBjYS1wb21vc0BraWJzLm1rMRAwDgYDVQQDDAdTZXJ2aXMxMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmJTEsZPJMF7LOQy4sncUa1W0MBhywvZh93E2OoQMqZ5vXbO84W2/Voe8MW+tx6tmtTFCZqtZMU2QSY84j9DqL4T5o+o4NOoodAo212F7g5AhV4GQhPZqLh3/p221QyN9vE32wFanuJv19Y33jshKhnCGD/7kPRQJHfnee55/2ZmiwUovg1FvQ0Jw+3SkRRTagV6xRizxnSD05j1qHLu8CrPDZ9pDkeYIIVaW0o0Dbmh2O1Od+otOMZfEGOUnGG9IGd35X76GBtQlq4WLCGyDnOEOvpU8m9WNQsxoayFkahPb6QMjD4xVzs6u60a3s6+gmNoSRnGHvimsjDyP62cLBQIDAQABo4IB4jCCAd4wCQYDVR0TBAIwADCBvwYDVR0gBIG3MIG0MEMGC2CGSAGG+EUBBxcCMDQwMgYIKwYBBQUHAgEWJmh0dHA6Ly93d3cua2lic3RydXN0Lm1rL3JlcG9zaXRvcnkvY3BzMG0GBgQAizABAjBjMGEGCCsGAQUFBwICMFUaU092YSBlIGt2YWxpZmlrdXZhbiBzZXJ0aWZpa2F0IHphIGVsZWt0cm9uc2tpIHBlY2hhdCBzb2dsYXNubyBFdnJvcHNrYXRhIFJlZ3VsYXRpdmEuME4GA1UdHwRHMEUwQ6BBoD+GPWh0dHA6Ly9jcmwtdGVzdC5hZGFjb20uY29tL0tJQlNBRFNrb3BqZVRlc3RDQUczL0xhdGVzdENSTC5jcmwwCwYDVR0PBAQDAgTwMB0GA1UdDgQWBBTQuhzgctAC0dqZgeBUdbdnD/Gn0jAfBgNVHSMEGDAWgBQ00QpTasVWIdkKx8aUuJG7utGnYjA7BgNVHSUENDAyBggrBgEFBQcDAgYIKwYBBQUHAwQGCCsGAQUFBwMFBggrBgEFBQcDBgYIKwYBBQUHAwcwGwYDVR0RBBQwEoEQY2EtcG9tb3NAa2licy5tazAYBggrBgEFBQcBAwQMMAowCAYGBACORgEBMA0GCSqGSIb3DQEBCwUAA4IBAQCnyDC4b4FaG6RqL/uuJdo5aofepj+nPTE0Tn3MsdYp8iLJAQp7jXoE0/O3oxi6I976AjFnVpZL3bZRzFV1EBziNIw4+cLrconaM+f7eWIE+/JiwWXZ4DANiOTBE2cunroTHG58RfGsalh0volpF55JlukCHKHRC0xbZ8ZkxVMHOiF8T4dfA018dYu7ooOBmAmNGWLhqDV7P1lldrGz0WiHaToNF8pjRg4ehgaaQ5ouX87mzcNfoPO9nxKvVK+wfoQoHcUwn9vRcCZbdt82/DVNWThsgpcAe6Dq5juIFf/ZBdsq47gIyg+d1IyVxynTIyburRPx0KWuE6xfqrfOA9Ud";

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
            }, TypeException.DefaultException, _logger);
            //try
            //{
            //}
            //catch (Exception exception)
            //{
            //    _logger.Error("Error happened in GetPublicKeyForProvider", exception.Message, "MimMsgHelper_GetPublicKeyForProvider");
            //    throw new Exception("Error in method GetPublicKeyForProvider: " + exception.Message);
            //}
            return result;
        }

        public EncryptedPacket EncryptSoapBody(string original, RsaWithRsaParameterKey keys)
        {
            var hybrid = new HybridEncryption();
            var encryptedBlock = hybrid.EncryptData(Encoding.UTF8.GetBytes(original), keys);
            return encryptedBlock;
        }

        public XmlDocument CreateMimXmlMsg(SoapMessage mimMsg)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                var soapNS = new XmlSerializerNamespaces();
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

                    return xmlDoc;
                }
            }, TypeException.DefaultException, _logger);

            return result;
        }

        public string CreateMimSignedXmlMsg(XmlDocument soapmsg, OwnCertificate ownCert)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                return SignXml(soapmsg, ownCert.Certificate);
            },TypeException.DefaultException, _logger);

            return result;

        }

        public string DecryptSoapBody(byte[] encryptedData, byte[] sessionKey, byte[] iVector, RsaWithRsaParameterKey rsaParams)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
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
                
            }, TypeException.DefaultException, _logger);

            return result;

        }

        public SoapMessage CreateMimResponseMsg(SoapMessage mimMsg, string mimeType)
        {
            const string participantCode = "CRRM";
            return new SoapMessage
            {
                Header = new Header
                {
                    MimHeader = new MimHeader
                    {
                        id = "MIMHeader",
                        Consumer = mimMsg.Header.MimHeader.Consumer,
                        Provider = participantCode,//ConfigurationManager.AppSettings["ParticipantCode"],
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
                    MimAdditionalHeader = new MimAdditionalHeader
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

        public RsaWithRsaParameterKey GetPublicKeyFromString(string certString)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
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

            }, TypeException.DefaultException, _logger);

            return result;
        }

        public void LogInitialMimMessage(SoapMessage mimMsg)
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
           //TODO: Add logic for validation and insert in DB
        }

        public string SignXml(XmlDocument xmlDoc, X509Certificate2 cert)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                var key = (RSACryptoServiceProvider)cert.PrivateKey;
                var signedXml = new SignedXml(xmlDoc);
                var xmlSignature = signedXml.Signature;
                signedXml.SigningKey = key;

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

                xmlSignature.KeyInfo = keyInfo;

                signedXml.ComputeSignature();

                var xmlDigitalSignature = signedXml.GetXml();

                var ns = new XmlNamespaceManager(xmlDoc.NameTable);
                ns.AddNamespace("mioa", "http://mioa.gov.mk/interop/mim/v1");

                var mimHeader = xmlDoc.SelectSingleNode("//mioa:MIMHeader", ns);
                mimHeader.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

                return xmlDoc.InnerXml;
            }, TypeException.DefaultException, _logger);

            return result;
        }
    }
}
