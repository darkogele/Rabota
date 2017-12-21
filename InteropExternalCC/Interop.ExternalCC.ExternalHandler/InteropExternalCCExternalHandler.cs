using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Interop.ExternalCC.HandlersHelper.Contracts;
using Interop.ExternalCC.HandlersHelper.HelperMethods;
using Ninject;
using Interop.ExternalCC.CrossCutting;
using Interop.ExternalCC.CrossCutting.Logging;
using Interop.ExternalCC.ExternalHandler.Ninject;
using System.IO;

namespace Interop.ExternalCC.ExternalHandler
{
    public class InteropExternalCCExternalHandler : IHttpHandler
    {
        private readonly IExternalCCRequestHelper _externalCcRequestHelper;
        private readonly IRequestExtensionMethods _requestExtensionMethods;

        public InteropExternalCCExternalHandler()
        {
            using (IKernel kernel = new StandardKernel(new RegisterNinjectModule()))
            {
                _externalCcRequestHelper = kernel.Get<IExternalCCRequestHelper>();
                _requestExtensionMethods = kernel.Get<IRequestExtensionMethods>();

            }
        }

        public InteropExternalCCExternalHandler(IExternalCCRequestHelper externalCcRequestHelper, IRequestExtensionMethods requestExtensionMethods)
        {
            _externalCcRequestHelper = externalCcRequestHelper;
            _requestExtensionMethods = requestExtensionMethods;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var logSoap = AppSettings.Get<bool>("LogSoap");
            if (logSoap)
            {
                var nameLoger = "Request to External Handler_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info("---- Request to External Handler ----");
                }
            }


            var soapBody = string.Empty;
            bool fromFile = bool.Parse(AppSettings.Get<string>("FromFileSystem"));
            if (fromFile)
            {
               soapBody = File.ReadAllText(@"C:\NSRequest.txt");
                var nameLoger = "soapBody_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info(soapBody);
                }
            }
            else
            {
                soapBody = _requestExtensionMethods.GetSoapBody(context);
            }

            if (string.IsNullOrEmpty(soapBody))
            {
                context.Response.StatusCode = 400;

                context.Response.End();
            }

            //var action1 = _requestExtensionMethods.GetSoapAction(context);
            
           
            var xDoc = XDocument.Parse(soapBody);
            var soapAction2 = xDoc.Descendants().SingleOrDefault(x => x.Name.LocalName == "ActionName");
            

            if (logSoap)
            {
                var nameLoger = "Soap action from SoapBody_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info(soapBody + "------------- soap action from SoapBody:" + soapAction2.Value);
                }
            }




            //var kingExternalBusCode = AppSettings.Get<string>("KingExternalBusCode");
            //var readyMim = _externalCcRequestHelper.UnwrapMimMessage(soapBody, kingExternalBusCode);

            #region Log SOAP Request Message

            if (logSoap)
            {
                var nameLoger = "Request_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info(soapBody);
                }
            }

            #endregion
            List<XNode> childNodes = null;
            //change AdditionalHeader in soapBody
            if (AppSettings.Get<bool>("SwitchAdditionalHeader"))
            {
                XNamespace defaultNs = "http://mioa.gov.mk/interop/mim/v1";
                childNodes = xDoc.Descendants(defaultNs + "MIMadditionalHeader").Nodes().ToList();
                xDoc.Descendants(defaultNs + "MIMadditionalHeader").Descendants().Remove();
                xDoc.Descendants().SingleOrDefault(x => x.Name.LocalName == "MIMadditionalHeader")
                    .Add(new XElement(defaultNs + "Status"), 
                    new XElement(defaultNs + "StatusMessage"), 
                    new XElement(defaultNs + "ProviderEndpointUrl"), 
                    new XElement(defaultNs + "ExternalEndpointUrl"), 
                    new XElement(defaultNs + "WebServiceUrl"), 
                    new XElement(defaultNs + "ConsumerBusCode"), 
                    new XElement(defaultNs + "TimeStampToken"), 
                    new XElement(defaultNs + "IsInteropTestCommunicationCall", false));
                soapBody = xDoc.ToString();
            }
            else
            {
                XNamespace defaultNs = "http://mioa.gov.mk/interop/mim/v1";
                xDoc.Descendants().SingleOrDefault(x => x.Name.LocalName == "MIMadditionalHeader")
                    .Add(new XElement(defaultNs + "TimeStampToken"), 
                    new XElement(defaultNs + "IsInteropTestCommunicationCall", false));
                soapBody = xDoc.ToString();
            }
            var nameLoger1 = "Body to be sent to BizTalk_" + DateTime.Now;
            using (var logger = LoggingFactory.GetNLogger(nameLoger1))
            {
                logger.Info("Body to be sent to BizTalk:_____________________" + soapBody);
            }
            var url = AppSettings.Get<string>("UrlToCentralServer");


            string responseFromExternalCs;
            try
            {
                var nameLoger = "soapAction_url_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info("soapAction_url_: " + soapAction2.Value + " " + url);
                }

                responseFromExternalCs = _externalCcRequestHelper.ResentExternalCCRequest(soapAction2.Value, soapBody, url);
            }
            catch (Exception ex)
            {
                var nameLoger = "Error when sent to BizTalk_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info("Error when sent to BizTalk: " + ex);
                }
                
                throw ex;
            }

            #region Log SOAP Request Message

            if (logSoap)
            {
                var nameLoger = "Response_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info(responseFromExternalCs);
                }
            }

            #endregion

            var xDocAfterResponse = XDocument.Parse(responseFromExternalCs);
            //change back AdditionalHeader in soapBody
            if (AppSettings.Get<bool>("SwitchAdditionalHeader"))
            {
                XNamespace defaultNs = "http://mioa.gov.mk/interop/mim/v1";
                xDocAfterResponse.Descendants(defaultNs + "MIMadditionalHeader").Descendants().Remove();
                foreach (XNode nsNodes in childNodes)
                {
                    xDocAfterResponse.Descendants().SingleOrDefault(x => x.Name.LocalName == "MIMadditionalHeader").Add(nsNodes);
                }

                responseFromExternalCs = xDocAfterResponse.ToString();
                }

            if (logSoap)
            {
                var nameLoger = "Response_to_NS_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info(responseFromExternalCs);
                }
            }

            HttpContext.Current.Response.ContentType = "application/soap+xml";
            context.Response.Write(responseFromExternalCs);
        }
    }
}