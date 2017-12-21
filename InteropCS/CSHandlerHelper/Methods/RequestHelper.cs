using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSHandlerHelper.Contracts;
using CSHandlerHelper.Model;
using Interop.CS.CrossCutting;
using Interop.CS.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.UoW;

namespace CSHandlerHelper.Methods
{
    public class RequestHelper : IRequestHelper
    {
        public UrlSegment GetUrlSegments(string url)
        {
            var participantCode = AppSettings.Get<string>("ParticipantCode");
            var splitedUrl = url.Split('/');

            if (splitedUrl.Length == 4)
            {
                return new UrlSegment
                {
                    Consumer = participantCode,
                    RoutingToken = splitedUrl[2],
                    Service = splitedUrl[3],
                    IsUrlCorrrect = true
                };
            }
            else if (splitedUrl.Length == 5)
            {
                var async = !String.IsNullOrEmpty(splitedUrl[5]) && splitedUrl[5] == "Async";
                return new UrlSegment
                {
                    Consumer = participantCode,
                    RoutingToken = splitedUrl[2],
                    Service = splitedUrl[3],
                    Async = async,
                    IsUrlCorrrect = true
                };
            }
            return new UrlSegment();
        }

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

        public string GetSoapHeader(NameValueCollection headers)
        {

            if (headers["SOAPAction"] != null && !String.IsNullOrEmpty(headers["SOAPAction"]))
            {
                return headers["SOAPAction"];
            }
            return String.Empty;
        }

        public string GetSoapBody(Stream inputStream)
        {
            var tSR = new StreamReader(inputStream);
            return tSR.ReadToEnd();
        }

        public string GetSoapMethodName(string soapBody)
        {
            var xdoc = XDocument.Parse(soapBody);
            XNamespace ns = "http://www.w3.org/2003/05/soap-envelope";
            XElement element = xdoc.Descendants(ns + "Body").FirstOrDefault();
            var fn = (XElement)element.FirstNode;
            return fn.Name.LocalName;
        }


        public string GetServiceUrl(MimHeader mimMessageHeaderMimMessage)
        {
            //var serviceRepository = new ServiceRepository(new UnitOfWork(new InteropContext()));
            //var service = serviceRepository.GetServiceByCode(mimMessageHeaderMimMessage.Service);
            //if (service != null)
            //{
            //    return service.Endpoint;
            //}

            //throw new Exception("There is no service for given service code");

            return "Panche_Trifunov";
        }
    }
}
