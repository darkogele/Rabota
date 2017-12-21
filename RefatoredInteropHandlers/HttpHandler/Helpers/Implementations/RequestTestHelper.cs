using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using CrossCutting.Logging;
using Helpers.Contracts;
using Helpers.Models;

namespace Helpers.Implementations
{
    public class RequestTestHelper : IRequestTestHelper
    {
        private readonly ILogger _logger;
        public RequestTestHelper(ILogger logger)
        {
            _logger = logger;
        }
        public UrlSegment GetUrlSegments(string url)
        {
            //Method throw its own custom exception called UrlSegmentException
            //Also method can throw default Exception instead of UrlSegmentException

            //DELEGATE WAY
            //IDEA IS NOT TO CALL TRY...CATCH IN EVERY METHOD
            //TRY...CATCH IS INSIDE ExecuteLogicAndLogException METHOD, WHITCH NOW TAKES CARE OF TYPE OF THE EXCEPTION. EXAMPLE: UrlSegmentException, SoapBodyException

            var result = Helper.ExecuteLogicAndLogException(() => 
            {
                var urlSegments = new UrlSegment();
                //TODO: Set in .config
                const string participantCode = "CRRM";

                const bool isInteropTestCommunicationCall = false;
                var splitUrl = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var splitedUrl = new List<string>();
                foreach (var segment in splitUrl)
                {
                    splitedUrl.Add(segment);
                }

                if (splitedUrl.Count == 3)
                {
                    urlSegments.Consumer = participantCode;
                    urlSegments.RoutingToken = splitedUrl[1];
                    urlSegments.Service = splitedUrl[2];
                    urlSegments.IsUrlCorrrect = true;
                    urlSegments.IsInteropTestCommunicationCall = isInteropTestCommunicationCall;
                }
                if (splitedUrl.Count == 4)
                {
                    urlSegments.Consumer = participantCode;
                    urlSegments.RoutingToken = splitedUrl[2];
                    urlSegments.Service = splitedUrl[3];
                    urlSegments.IsUrlCorrrect = true;
                    urlSegments.IsInteropTestCommunicationCall = isInteropTestCommunicationCall;
                }
                if (splitedUrl.Count == 5)
                {
                    //var async = !String.IsNullOrEmpty(splitedUrl[5]) && splitedUrl[5] == "Async";
                    urlSegments.Consumer = participantCode;
                    urlSegments.RoutingToken = splitedUrl[3];
                    urlSegments.Service = splitedUrl[4];
                        //Async = async,
                    urlSegments.IsUrlCorrrect = true;
                    urlSegments.IsInteropTestCommunicationCall = isInteropTestCommunicationCall;
                }
                return urlSegments;
            }, TypeException.UrlSegmentException, _logger);

            return result;
            //STANDARD WAY
            //try
            //{
            //}
            //catch (Exception exception)
            //{
            //    _logger.Error("Error happened in GetUrlSegments", exception.Message, "RequestTestHelper_GetUrlSegments");
            //    throw new Exception("Error in GetUrlSegments", new UrlSegmentException(exception.Message, exception.InnerException));
            //}
        }
        public string GetActionFromContentType(string contentType)
        {
            var actionName = string.Empty;
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                if (!string.IsNullOrEmpty(contentType))
                {
                    var action = contentType.Split(';').Last();

                    if (!string.IsNullOrEmpty(action))
                    {
                        action = action.Substring(action.IndexOf('"') + 1);
                        actionName = action.Substring(0, action.Length - 1);
                    }
                }
                return actionName;
            }, TypeException.DefaultException, _logger);

            return result;
        }

        public string GetServiceUrl(MimHeader mimMessageHeaderMimMessage)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                var serviceName = mimMessageHeaderMimMessage.Service;
                return serviceName == "InteropTestCommunicationService" ? "InteropTestCommunicationServiceEndpoint" : string.Empty;
                //var serviceRepository = new ServiceRepository(new UnitOfWork(new InteropContext()));
                //var service = serviceRepository.GetServiceByCode(serviceName);
                //if (service != null)
                //{
                //    return service.Endpoint;
                //}
            },TypeException.DefaultException, _logger);

            return result;
        }

        public string GetOnlySoapBodyFromString(string inputStream)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                var doc = new XmlDocument();
                doc.LoadXml(inputStream);
                var ns = new XmlNamespaceManager(doc.NameTable);
                ns.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
                var bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//s:Body", ns);
                if (bodyNode == null)
                {
                    ns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//soap:Body", ns);
                }
                return bodyNode.InnerXml;
            }, TypeException.DefaultException, _logger);

            return result;
        }


        public string GetOnlySoapBody(Stream inputStream)
        {
            //const string inputStream = "<?xml version='1.0' encoding='utf-8'></soap:Envelope><soap:Envelope xmlns:mioa='http://mioa.gov.mk/interop/mim/v1' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns='http://www.slss.hr/' xmlns:soap='http://www.w3.org/2003/05/soap-envelope'><soap:Header><mioa:MIMHeader id='MIMHeader'><mioa:Consumer>CRRM</mioa:Consumer><mioa:Provider>AKN</mioa:Provider><mioa:RoutingToken>MIM1$$AKN</mioa:RoutingToken><mioa:Service>ImotenListParcela</mioa:Service><mioa:ServiceMethod>http://interop.org/IAKNService/GetPropertyList</mioa:ServiceMethod><mioa:TransactionId>a520132a-ff3d-40be-bb66-461a7421bb99</mioa:TransactionId><mioa:Dir>Response</mioa:Dir><mioa:PublicKey>MIIFbzCCBFegAwIBAgIQZhXzWJpQqIltlgMRd/SY3jANBgkqhkiG9w0BAQsFADBhMQswCQYDVQQGEwJNSzEXMBUGA1UEChMOS0lCUyBBRCBTa29wamUxHzAdBgNVBAsTFkZPUiBURVNUIFBVUlBPU0VTIE9OTFkxGDAWBgNVBAMTD0tJQlMgVEVTVCBDQSBHMzAeFw0xNTEyMjkwMDAwMDBaFw0xNjEyMjgyMzU5NTlaMIGfMQ8wDQYDVQQLFAZPZGRlbDIxFTATBgNVBAoUDEluc3RpdHVjaWphMjEXMBUGA1UECxQORU1CUyAtIDU1Mjk1ODExHDAaBgNVBAsUE0VEQiAtIDQwMzAwMDE0MTYzMDkxCzAJBgNVBAYTAk1LMR8wHQYJKoZIhvcNAQkBFhBjYS1wb21vc0BraWJzLm1rMRAwDgYDVQQDDAdTZXJ2aXMyMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAw1K3qXeEQTIgCOGTFACx0aXfksfcVJ2NuQ072C7g9tiqGtGNHyJCnv9sXVvSHSpITP/igATGZcDr39Y6r3Hh9v1x2Cn5Ve4RDEXmcyiBs8Vr8QVE8iPfWD/hCCtcKsK4lK4AaGSOnDiHsnnrUDqxJJWChoivfSkUlkR02I4j/zVHUbiCaSpNrwCHPdYNaE3WeaVjn3SXqLRSg/a47TRTf8wUFoqP4pbXlQtK7Fq6ZhC/0OL3IqbLf5ApIe25Fvo8UwqAdGj04PFzQuYZYZkDceNg0UfMC7GbAlFYwfZxdlWIgWs7h6d0g68GqJkL3Mao8f6iQ/jrzQnK5ykHboZi6QIDAQABo4IB4jCCAd4wCQYDVR0TBAIwADCBvwYDVR0gBIG3MIG0MEMGC2CGSAGG+EUBBxcCMDQwMgYIKwYBBQUHAgEWJmh0dHA6Ly93d3cua2lic3RydXN0Lm1rL3JlcG9zaXRvcnkvY3BzMG0GBgQAizABAjBjMGEGCCsGAQUFBwICMFUaU092YSBlIGt2YWxpZmlrdXZhbiBzZXJ0aWZpa2F0IHphIGVsZWt0cm9uc2tpIHBlY2hhdCBzb2dsYXNubyBFdnJvcHNrYXRhIFJlZ3VsYXRpdmEuME4GA1UdHwRHMEUwQ6BBoD+GPWh0dHA6Ly9jcmwtdGVzdC5hZGFjb20uY29tL0tJQlNBRFNrb3BqZVRlc3RDQUczL0xhdGVzdENSTC5jcmwwCwYDVR0PBAQDAgTwMB0GA1UdDgQWBBSsusmmdC7Kx4yrV3cUB2ILbcRuCjAfBgNVHSMEGDAWgBQ00QpTasVWIdkKx8aUuJG7utGnYjA7BgNVHSUENDAyBggrBgEFBQcDAgYIKwYBBQUHAwQGCCsGAQUFBwMFBggrBgEFBQcDBgYIKwYBBQUHAwcwGwYDVR0RBBQwEoEQY2EtcG9tb3NAa2licy5tazAYBggrBgEFBQcBAwQMMAowCAYGBACORgEBMA0GCSqGSIb3DQEBCwUAA4IBAQAktJuurhytKYFPB9n4WfgGc5hw7FXl7IHjX0MdNIpURQ22j5emh1DrPWnjswREIU3bloCUksCnSSAOCM1U5BM8VqvFtjyn4AXef/+5pIFyNivqMVOWD3F+Qqe/OzIQYHnw9Dcy7BM2o30aEjeLjKnI1OlZgDWiHF4EnqEuoi5DbOtQx0YdrnTcimD4FlHow1AjMbnvcV8FFRRbm9IhB9Bp5wC35AX4EiahUBqsy39585pp6v7aySqqbN1UfCtLah9f9rTWhJh5qrrxBG0ahP6BRx0D015pG3oCyraopIcfodEdtVXvbQJLrNmCHJaKMVo1P2O0GfiArnDO4YmAFwdQ</mioa:PublicKey><mioa:MimeType>application/soap+xml; charset=utf-8</mioa:MimeType><mioa:TimeStamp>2016-06-21T06:44:13</mioa:TimeStamp><mioa:CorrelationID /><mioa:CallType>synchronous</mioa:CallType><Signature xmlns='http://www.w3.org/2000/09/xmldsig#'><SignedInfo><CanonicalizationMethod Algorithm='http://www.w3.org/TR/2001/REC-xml-c14n-20010315' /><SignatureMethod Algorithm='http://www.w3.org/2000/09/xmldsig#rsa-sha1' /><Reference URI='#MIMHeader'><Transforms><Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature' /><Transform Algorithm='http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments' /></Transforms><DigestMethod Algorithm='http://www.w3.org/2000/09/xmldsig#sha1' /><DigestValue>zvYxpBGrW6KpmTt0fAkakoDh9/I=</DigestValue></Reference><Reference URI='#MIMBody'><Transforms><Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature' /><Transform Algorithm='http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments' /></Transforms><DigestMethod Algorithm='http://www.w3.org/2000/09/xmldsig#sha1' /><DigestValue>gSytOCV4rzg++q2oGZHQXRhVBh4=</DigestValue></Reference></SignedInfo><SignatureValue>h+L6Ck7MIH0lHif08jZ1ZUszjKdwQFv1E3BALlHO8s4KJDPPrjMD1WKOE7UkHBPjN66K/ywhwZ38jM2bQJ/36VXIeq6XF+/fG1wuwsOf2rYwH5e9uOH4qzAprezJJ1vgdOQf25iSIdvl/zZHZ7R+5VI7NAt4xoXASYwNtDkHarVeywnH8SHVWL5/OM2Oe9tF9ehGH46duxvCCknZ+PimssJt+Vpwxa0i9kO/a+LD7q+S3FMvIOWfnN/G16Syvfr0kGERZge8DD0Wx0HOUKxWy1xteakiqCAJPtrBqsFQIYtZt4lKJ0bUHG3S9WeNlUJUgAUCFtJMi8oQNIiYcX2PXQ==</SignatureValue><KeyInfo><X509Data><X509Certificate>MIIFTTCCBDWgAwIBAgIQdmiILXAJhrJyVtFIEhZ5zTANBgkqhkiG9w0BAQUFADCBpTELMAkGA1UEBhMCTUsxFzAVBgNVBAoTDktJQlMgQUQgU2tvcGplMR8wHQYDVQQLExZTeW1hbnRlYyBUcnVzdCBOZXR3b3JrMTUwMwYDVQQLEyxDbGFzcyAyIE1hbmFnZWQgUEtJIEluZGl2aWR1YWwgU3Vic2NyaWJlciBDQTElMCMGA1UEAxMcS0lCUyBDZXJ0aWZpY2F0ZSBTZXJ2aWNlcyBDQTAeFw0xNjAxMjkwMDAwMDBaFw0xOTAxMjgyMzU5NTlaMIHBMQswCQYDVQQGEwJNSzEXMBUGA1UECxQORU1CUyAtIDQwNjU5MzAxHDAaBgNVBAsUE0VEQiAtIDQwMzA5OTAyNTQ1MzMxNTAzBgNVBAoULEFnZW5jaWphIHphIGthdGFzdGFyIG5hIG5lZHZpem5vc3RpIC0gU2tvcGplMSgwJgYJKoZIhvcNAQkBFhlnLm5pa29sb3ZAa2F0YXN0YXIuZ292Lm1rMRowGAYDVQQDDBFJbnRlcm9wZXJhYmlsbm9zdDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMfRLODhOvjb4eTA/for3/yrgjLm7bfsKBGl4/1WE4A3TeT9N8gk8DofP0RKbvIHz5PY8sV7RGeIuEzJtRNpl/0dxT5HSpIoz6IAeG+GHYWwrKblbQAlPl+mYz40xdgYEzqYabqVvv7zW+JCex3DKtn4fosK2+8TRM2cZokNiJbyyLrjkjTBVKL4RrAPoAmEleOE+6xSc8Pl0Aqyj6Y/LlA7Sjz526AwcmxgXM5JlJEKjCrfd8QfQyMmku/FqdTIF0lxYwBkMX6mFJ5BZwSHDEandhx7T0r3qKsCWBRy/zhC7sa034TSZP+oI6YSM8XkVayzibnKJJM72inovLXzlhUCAwEAAaOCAVkwggFVMAkGA1UdEwQCMAAwTQYDVR0gBEYwRDBCBgtghkgBhvhFAQcXAjAzMDEGCCsGAQUFBwIBFiVodHRwczovL2NhLmtpYnMuY29tLm1rL3JlcG9zaXRvcnkvY3BzMDYGA1UdHwQvMC0wK6ApoCeGJWh0dHA6Ly9jYS5raWJzLmNvbS5tay9jcmwvVmVyYmFOUS5jcmwwCwYDVR0PBAQDAgTwMB0GA1UdDgQWBBSyOD7M2FIpV/MwHzz0DHpk9pD+qjAfBgNVHSMEGDAWgBTcpNDjYmqaTPtZJZUxbo/S0W4Q2zA7BgNVHSUENDAyBggrBgEFBQcDAgYIKwYBBQUHAwQGCCsGAQUFBwMFBggrBgEFBQcDBgYIKwYBBQUHAwcwJAYDVR0RBB0wG4EZZy5uaWtvbG92QGthdGFzdGFyLmdvdi5tazARBglghkgBhvhCAQEEBAMCB4AwDQYJKoZIhvcNAQEFBQADggEBACk02ThregQBU8qvssaQKPZHkMy2lnO8CwnlXBsR/dJ4EASfrJPJ8FofqqNIh9mrmWvAFOZR5juYK4a+MgJZB+j7z9pBo5BwPCUzEK1+SXJJRbsBP2saP9pi0E6Gpz3Aoy2AfNFfEmRVm2QqrW1TYN6Nw8SVDaM1I0ffpuhP/Li7kdRdzFscqikPXA8hd29eiYh4yXk4XZ7efbtVRcCuDF/R0iIfNIT2kLbqh7TpYXLgkLBJNVMwM+D/tXitO+g1o9+ZKosKEw3RYFlMm2Hd96GoKoaTIS5xaQcdX1qbhNPi0WRHetgC2ZijHkTkLyCBd15LsYar0CpWK2jz0as5gwQ=</X509Certificate></X509Data></KeyInfo></Signature></mioa:MIMHeader><mioa:MIMadditionalHeader><mioa:Status>200</mioa:Status><mioa:StatusMessage>OK</mioa:StatusMessage><mioa:ProviderEndpointUrl /><mioa:ExternalEndpointUrl /><mioa:WebServiceUrl /><mioa:ConsumerBusCode /><mioa:TimeStampToken>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</mioa:TimeStampToken><mioa:IsInteropTestCommunicationCall>false</mioa:IsInteropTestCommunicationCall></mioa:MIMadditionalHeader><mioa:CryptoHeader><mioa:Key>g9yhhITzfP/70LIZvIdpoZxHqldzKg/+KH3yVSbIfrf/AJtF0CQMNpEzUmDrbWCVD0DwnkHnw1Nq2IjSNCzUZPi9oUVZbgzwfLdBJm5L9XpReH9oi5QDzZh6B/EGRGfL/IkJGUt5zMxkUU+DYqvsVH/Y3Gep1Jeb3cF9CJclTYiTtU+c6o8PUVb6BdviRDueeyP+Jpdq4BrsxRbs4kbpCyPvk42dYhIcoXyyuyi9uA4JstypcdOgRXyyN3Qcva+JLwaEGrAiYchtBxWaFkKd+VBXyqrq75bgVHd+dh+YJBzCPw/EfFjWNZHGPMt1oXVmq/0lHNQFYhJ4aRX89LZTwA==</mioa:Key><mioa:InitializationVector>f28g12aq1rTqrJpqGLLkwg==</mioa:InitializationVector><mioa:FormatValue>AES</mioa:FormatValue></mioa:CryptoHeader></soap:Header><soap:Body><mioa:MIMBody id='MIMBody'><mioa:Message>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</mioa:Message></mioa:MIMBody></soap:Body></soap:Envelope>";
            //var stringToStream = GenerateStreamFromString(inputStream);

            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                var tSR = new StreamReader(inputStream);
                var doc = new XmlDocument();
                doc.LoadXml(tSR.ReadToEnd());
                var ns = new XmlNamespaceManager(doc.NameTable);
                ns.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
                var bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//s:Body", ns);
                if (bodyNode == null)
                {
                    ns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//soap:Body", ns);
                }
                return bodyNode.InnerXml;
            },TypeException.SoapBodyException,_logger);

            return result;
        }

        public string MakeWebRequest(string url)
        {
            string responseFromServer = string.Empty;

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Accept = "application/soap+xml";
            try
            {
                using (var response = (HttpWebResponse)webRequest.GetResponse())
                {
                    var responseFromBizTalk = response.GetResponseStream();
                    if (responseFromBizTalk != null)
                    {
                        using (var rd = new StreamReader(responseFromBizTalk))
                        {
                            responseFromServer = rd.ReadToEnd();
                            rd.Close();
                            rd.Dispose();
                        }
                        response.Close();
                    }
                }
                return responseFromServer;
            }
            //ova e koga Internal handler kje bide nedostapen
            catch (WebException webException)
            {
                throw new Exception(url + " ," + webException.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public SoapFaultMessage UnwrapSoapFaultMessage(string soapFaultMessage)
        {
            var xDoc = XDocument.Load(new StringReader(soapFaultMessage));
            var serializer = new XmlSerializer(typeof(SoapFaultMessage));
            SoapFaultMessage soapMsg;
            using (TextReader reader = new StringReader(xDoc.ToString()))
            {
                soapMsg = (SoapFaultMessage)serializer.Deserialize(reader);
            }
            return soapMsg;
        }

        public ResponseInteropCommunication Execute(string decryptedRequestBodyWrappedInSoapEnvelope, string contentType, string url)
        {
            var interopResponse = new ResponseInteropCommunication();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            request.Accept = "application/soap+xml";
            request.Method = "POST";

            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(decryptedRequestBodyWrappedInSoapEnvelope);
            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    interopResponse.MimeType = response.ContentType;
                    interopResponse.StatusCode = response.StatusCode;
                    var responseFromBizTalk = response.GetResponseStream();
                    if (responseFromBizTalk != null)
                    {
                        using (var rd = new StreamReader(responseFromBizTalk))
                        {
                            interopResponse.Response = rd.ReadToEnd();
                            rd.Close();
                            rd.Dispose();
                        }
                        response.Close();
                    }
                }
            }
            catch (WebException exception)
            {
                //Овој catch дел е за да ги прикажеме custom InteropFault грешки, што ги праќаме од адаптерите
                WebResponse errResponse = exception.Response;
                string fullInteropFaultFromAdapter = string.Empty;
                using (Stream faultFromAnotherSide = errResponse.GetResponseStream())
                {
                    if (faultFromAnotherSide != null)
                    {
                        var reader = new StreamReader(faultFromAnotherSide);
                        fullInteropFaultFromAdapter = reader.ReadToEnd();
                    }
                }
                if (!string.IsNullOrEmpty(fullInteropFaultFromAdapter))
                {
                    var fullInteropFaultFromAdapterReason = UnwrapSoapFaultMessage(fullInteropFaultFromAdapter);
                    throw new FaultException(fullInteropFaultFromAdapterReason.Body.Fault.Reason.Text.value);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception.InnerException);
            }
            return interopResponse;
        }

        public XmlDocument CreateFaultMessage(SoapFaultMessage soapFault)
        {
            var soapNS = new XmlSerializerNamespaces();
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

        public SoapMessage UnwrapMimMessage(string mimMessage)
        {
            XDocument xDoc = XDocument.Load(new StringReader(mimMessage));

            var serializer = new XmlSerializer(typeof(SoapMessage));
            SoapMessage soapMsg;
            using (TextReader reader = new StringReader(xDoc.ToString()))
            {
                soapMsg = (SoapMessage)serializer.Deserialize(reader);
            }
            return soapMsg;
        }

        public SoapMessage CreateMimResponseMsg(SoapMessage mimMsg, string mimeType)
        {
            return new SoapMessage
            {
                Header = new Header
                {
                    MimHeader = new MimHeader
                    {
                        id = "MIMHeader",
                        Consumer = mimMsg.Header.MimHeader.Consumer,
                        Provider = "AKN",
                        RoutingToken = mimMsg.Header.MimHeader.RoutingToken,
                        Service = mimMsg.Header.MimHeader.Service,
                        ServiceMethod = mimMsg.Header.MimHeader.ServiceMethod,
                        TransactionId = mimMsg.Header.MimHeader.TransactionId,
                        Dir = "Response",
                        PublicKey = mimMsg.Header.MimHeader.PublicKey,
                        MimeType = mimeType,
                        CorrelationID = String.Empty,
                        CallType = mimMsg.Header.MimHeader.CallType
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

        public string GetSoapBody(Stream inputStream)
        {
            var result = Helper.ExecuteLogicAndLogException(() =>
            {
                var tSR = new StreamReader(inputStream);
                return tSR.ReadToEnd();
            }, TypeException.DefaultException, _logger);

            return result;

        }

        public string GetSoapHeader(NameValueCollection headers)
        {
            if (headers["SOAPAction"] != null && !String.IsNullOrEmpty(headers["SOAPAction"]))
            {
                return headers["SOAPAction"];
            }
            return String.Empty;
        }

       public Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
