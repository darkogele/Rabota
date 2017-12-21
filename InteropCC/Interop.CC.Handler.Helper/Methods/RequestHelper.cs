using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml.Linq;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Models.Repository;
using Interop.CC.Models;
using Interop.CC.CrossCutting;
using Interop.CC.Models.UoW;
using System.Xml;

namespace Interop.CC.Handler.Helper.Methods
{
    public class RequestHelper : IRequestHelper
    {
        // Опис: Метод кој овозможува проверка за тоа дали се работи за тест комуникација
        // Влезни параметри: податочна вредност segment
        // Излезни параметри: податочен тип bool
        private bool IsTestCommunicationCall(string segment)
        {
            return segment == "InteropTestCommunicationCall";
        }

        // Опис: Метод за екстракција и анализа на Url од Web повикот кој ги враќа параметрите потребни при комуникација
        // Влезни параметри: податочна вредност url
        // Излезни параметри: UrlSegment модел
        public UrlSegment GetUrlSegments(string url)
        {
            var participantCode = AppSettings.Get<string>("ParticipantCode");
            var isInteropTestCommunicationCall = false;
            var splitUrl = url.Split('/');
            List<string> splitedUrl = new List<string>();
            foreach (var segment in splitUrl)
            {
                if (IsTestCommunicationCall(segment))
                {
                    isInteropTestCommunicationCall = true;
                }
                else
                {
                    splitedUrl.Add(segment);
                }
            }

            if (splitedUrl.Count == 3)
            {
                return new UrlSegment
                {
                    Consumer = participantCode,
                    RoutingToken = splitedUrl[1],
                    Service = splitedUrl[2],
                    IsUrlCorrrect = true,
                    IsInteropTestCommunicationCall = isInteropTestCommunicationCall
                };
            }
            if (splitedUrl.Count == 4)
            {
                return new UrlSegment
                {
                    Consumer = participantCode,
                    RoutingToken = splitedUrl[2],
                    Service = splitedUrl[3],
                    IsUrlCorrrect = true,
                    IsInteropTestCommunicationCall = isInteropTestCommunicationCall
                };
            }
            if (splitedUrl.Count == 5)
            {
                var async = !String.IsNullOrEmpty(splitedUrl[5]) && splitedUrl[5] == "Async";
                return new UrlSegment
                {
                    Consumer = participantCode,
                    RoutingToken = splitedUrl[2],
                    Service = splitedUrl[3],
                    Async = async,
                    IsUrlCorrrect = true,
                    IsInteropTestCommunicationCall = isInteropTestCommunicationCall
                };
            }
            return new UrlSegment();
        }

        // Опис: Проверува дали Web повикот е од тип Soap (доколку е го враќа soapAction параметарот)
        // Влезни параметри: NameValueCollection headers, ref bool isSoapRequest
        // Излезни параметри: податочен тип string
        public string IsSoapRequest(NameValueCollection headers, ref bool isSoapRequest)
        {
            var soapAction = headers["SOAPAction"];

            if (soapAction != null)
            {
                if (!String.IsNullOrEmpty(soapAction))
                {
                    isSoapRequest = true;
                    return soapAction;
                }
            }
            return String.Empty;
        }

        // Опис: Проверува дали Web повикот е од тип Soap и дали има елементот headers содржи податоци или е null (доколку е го враќа soapAction параметарот)
        // Влезни параметри: NameValueCollection headers
        // Излезни параметри: податочен тип string 
        public string GetSoapHeader(NameValueCollection headers)
        {

            if (headers["SOAPAction"] != null && !String.IsNullOrEmpty(headers["SOAPAction"]))
            {
                return headers["SOAPAction"];
            }
            return String.Empty;
        }

        // Опис: Метод кој го екстрахира и вчитува телото на Soap пораката
        // Влезни параметри: Stream inputStream
        // Излезни параметри: податочен тип string 
        public string GetOnlySoapBody(Stream inputStream)
        {
            var tSR = new StreamReader(inputStream);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(tSR.ReadToEnd());
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
            var bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//s:Body", ns);
            if (bodyNode == null)
            {
                ns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//soap:Body", ns);
            }
            return bodyNode.InnerXml;
        }
        public string GetOnlySoapBodyFromString(string inputStream)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(inputStream);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
            var bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//s:Body", ns);
            if (bodyNode == null)
            {
                ns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//soap:Body", ns);
            }
            return bodyNode.InnerXml;
        }
        public string GetSoapBody(Stream inputStream)
        {
            var tSR = new StreamReader(inputStream);
            return tSR.ReadToEnd();
        }

        // Опис: Метод кој го екстрахира и вчитува името на соодветниот метод на Soap пораката
        // Влезни параметри: податочна вредност url
        // Излезни параметри: податочен тип string 
        public string GetSoapMethodName(string soapBody)
        {
            var xdoc = XDocument.Parse(soapBody);
            //XNamespace ns = "http://www.w3.org/2003/05/soap-envelope";
            //XElement element = xdoc.Descendants(ns + "Body").FirstOrDefault();
            var fn = (XElement)xdoc.FirstNode;
            return fn.Name.LocalName;
        }

        // Опис: Метод кој го вчитува endpoint-от односно Url-то на соодветен сервис
        // Влезни параметри: MimHeader mimMessageHeaderMimMessage
        // Излезни параметри: податочен тип string 

        public string GetServiceUrl(MimHeader mimMessageHeaderMimMessage)
        {
            var serviceName = mimMessageHeaderMimMessage.Service;
            if (serviceName == "InteropTestCommunicationService")
            {
                return "InteropTestCommunicationServiceEndpoint";
            }
            var serviceRepository = new ServiceRepository(new UnitOfWork(new InteropContext()));
            var service = serviceRepository.GetServiceByCode(serviceName);
            if (service != null)
            {
                return service.Endpoint;
            }

            throw new Exception("There is no service for given service code");
        }
    }
}