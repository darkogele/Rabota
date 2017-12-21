using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Handler.Helper.SOAP;
using Interop.CC.Models.Models;
using Interop.CC.CrossCutting.Logging;

namespace Interop.CC.Handler.Helper.Methods
{
    public class SoapRequestHelper : ISoapRequestHelper
    {

        // Опис: Метод кој овозможува процесирање и проследување на Web повици помеѓу системите во рамки на MIM архитектурата
        // Влезни параметри: Податочни вредности req, contentType, url
        // Излезни параметри: ResponseInteropCommunication модел како одговор на соодветен Web повик
        public ResponseInteropCommunication Execute(string req, string contentType, string url, ILogger logger)
        {
            logger.Info("Execute kon BizTalk");
            var btResponse = new ResponseInteropCommunication();

            HttpWebRequest request = CreateWebRequest(contentType, url);

            //request.Timeout = Timeout.Infinite;
            //request.KeepAlive = true;

            //IMPORTANT!!! DO NOT MOVE!!! IT'S SET TO AVOID TIMEOUT THAT APPEARS WHILE SENDING BIG MESSAGES
            request.Timeout = 120000;//600000(10 min)
            ////////////////////

            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(req);

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            try
            {
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    btResponse.MimeType = response.ContentType;
                    btResponse.StatusCode = response.StatusCode;
                    var responseFromBizTalk = response.GetResponseStream();
                    if (responseFromBizTalk != null)
                    {
                        using (var rd = new StreamReader(responseFromBizTalk))
                        {
                            btResponse.Response = rd.ReadToEnd();
                            rd.Close();
                            rd.Dispose();
                        }
                        response.Close();
                    }
                }

            }
            catch (WebException exception)
            {
                //Овој catch дел е за да ги прикажеме custom InteropFault грешки, што ги праќаме од адаптерите како и грешки ако адаптерот е недостапен

                logger.Info("errResponse status e: " + exception.Status);
                logger.Error("WebException vo povikot kon BizTalk", exception);
                WebResponse errResponse = exception.Response;
                if (errResponse.ContentLength == 0)
                {
                    if (exception.Status == WebExceptionStatus.ProtocolError ||
                        exception.Status == WebExceptionStatus.Timeout)
                    {
                        logger.Error("Adapterot na institucijata e nedostapen i vrati greska: " + exception.Message,
                            exception, "ProtocolOrTimeoutErrorOnAdapterRequest");
                        throw new FaultException("Адаптерот на сервисот е недостапен!");
                    }
                }
                else
                {
                    string fullInteropFaultFromAdapter = string.Empty;
                    if (errResponse.ContentLength > 0)
                    {
                        //Se vrakja realen Fault exception od adapterot
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
                }
                
            }
            catch (Exception exception)
            {
                logger.Error("Exception vo samiot BizTalk/Servisot na institucijata", exception, "ExceptionBizTalkORInstitution");
                throw new Exception(exception.Message, exception.InnerException);
            }

            return btResponse;
        }

        // Опис: Метод за креирање на SOAP Web повик
        // Влезни параметри: Податочни вредности contentType, url
        // Излезни параметри: HttpWebRequest модел
        public HttpWebRequest CreateWebRequest(string contentType, string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.Headers.Add(@"SOAPAction", soapAction);
            webRequest.ContentType = contentType; // "application/soap+xml; charset=\"utf-8\"";
            webRequest.Accept = "application/soap+xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        // Опис: Метод за конвертирање на MIM пораката во порака от тип SoapMessage
        // Влезни параметри: Податочни вредности mimMessage
        // Излезни параметри: SoapMessage модел
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

        // Опис: Метод за конвертирање на MIM пораката во порака от тип SoapFaultMessage
        // Влезни параметри: Податочни вредности soapFaultMessage
        // Излезни параметри: SoapFaultMessage модел 
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

        // Опис: Метод за валидирање на сертификатот со користење на крипто провајдер
        // Влезни параметри: Податочни вредности mimMessage, publicKey, ILogger _logger
        // Излезни параметри: податочен тип bool
        public bool ValidateSignature(string mimMessage, string publicKey, ILogger _logger)
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(mimMessage);
            var cert = new X509Certificate2(Convert.FromBase64String(publicKey));
            var publicKeyCert = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var publicKeyString = publicKeyCert.ToXmlString(false);
            csp = new RSACryptoServiceProvider();
            csp.FromXmlString(publicKeyString);
            try
            {
                if (doc == null) throw new ArgumentException("Doc");
                if (csp == null) throw new ArgumentException("Key");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message + "==== Null XMLDocument or CryptoServiceProvider!!!", e);
            }

            SignedXml signedXml = new SignedXml(doc);
            XmlNodeList nodeList = doc.GetElementsByTagName("Signature");
            try
            {
                if (nodeList.Count <= 0) throw new CryptographicException("Verification failed: No Signature was found in the document.");
                //if (nodeList.Count > 2) throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message + "==== none or multiple Signature tags!!!", e);
            }
            if(nodeList.Count > 1)
            {
                for (int i = 0; i < nodeList.Count; i++ )
                    if (nodeList[i].ParentNode.Name == "MIMHeader")
                    {
                        signedXml.LoadXml((XmlElement)nodeList[i]);
                    }
              
            }
            else
            signedXml.LoadXml((XmlElement)nodeList[0]);
            return signedXml.CheckSignature(csp);
        }

        // Опис: Метод за креирање на SoapFault за запис на истата во база
        // Влезни параметри: Guid tId, податочни вредности code, subCode, details, reason
        // Излезни параметри: 
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

        // Опис: Метод за хендлање и соодветен приказ на грешки при комуникација кои ги враќа BizzTalk
        // Влезни параметри: Податочни вредности btError
        // Излезни параметри: податочен тип string
        public string BTsoapFault(string btError)
        {

            string output = "";
            var errorMessage = btError.Split(';');

            // BizzTalk error for SoapFault message handling 
            Dictionary<string,string> errors = new Dictionary<string, string>();

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