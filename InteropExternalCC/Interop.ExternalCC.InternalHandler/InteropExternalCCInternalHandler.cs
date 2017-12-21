using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Interop.ExternalCC.CrossCutting.Logging;
using Interop.ExternalCC.CrossCutting;
using Interop.ExternalCC.HandlersHelper.Contracts;
using Interop.ExternalCC.InternalHandler.MIM2ExternalCCReference;
using Interop.ExternalCC.InternalHandler.Ninject;
using Ninject;
using Interop.ExternalCC.HandlersHelper.HelperMethods;

namespace Interop.ExternalCC.InternalHandler
{
    public class InteropExternalCCInternalHandler : IHttpHandler
    {
        private readonly IExternalCCRequestHelper _externalCcRequestHelper;
        private readonly ICacheHelper _cacheHelper;
        private readonly IRequestExtensionMethods _requestExtensionMethods;

        public InteropExternalCCInternalHandler()
        {
            using (IKernel kernel = new StandardKernel(new RegisterNinjectModule()))
            {
                _externalCcRequestHelper = kernel.Get<IExternalCCRequestHelper>();
                _cacheHelper = kernel.Get<ICacheHelper>();
                _requestExtensionMethods = kernel.Get<IRequestExtensionMethods>();
            }
        }

        public bool IsReusable
       { 
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {

            var soapAction = context.Request.ContentType; //_requestExtensionMethods.GetSoapAction(context);
            var soapBody = _requestExtensionMethods.GetSoapBody(context);
            var logSoap = AppSettings.Get<bool>("LogSoap");
            if (logSoap)
            {
                var nameLoger = "Request_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Info(soapBody + "-------soap action ContentType:" + soapAction);
                }
            }
            if (string.IsNullOrEmpty(soapBody))
            {
                context.Response.StatusCode = 400;
                context.Response.End();
            }

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

            //var url = "";
            //var participantCode = _externalCcRequestHelper.GetParticipantCode(soapBody);
            //var exists = _cacheHelper.Exists(participantCode);
            //if (exists)
            //{
            //    url = _cacheHelper.Get<string>(participantCode);
            //}
            //else
            //{
            //    url = _externalCcRequestHelper.GetParticipantUri(participantCode);
            //    _cacheHelper.Add(url, participantCode);
            //}

            MIMBody mimBody = new MIMBody();

            mimBody.Message = soapBody;

            //var test = new ServiceClient();
            //test.InvokeWebMethod(ref mimBody);

            var responseFromExternalCs = "";
            var urlToExternalBus = AppSettings.Get<string>("UrlToExternalBus");
            //var url = "https://externalcc.interop.st/ExternalCC/Service.svc";
            //var dictionary = new Dictionary<string, string>();
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                 ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira 
                responseFromExternalCs = _externalCcRequestHelper.ResentExternalCCRequest(null, soapBody, urlToExternalBus);
            }
            catch (Exception ex)
            {
                var nameLoger = "Error_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLoger))
                {
                    logger.Error(ex.Message);
                }
                //_cacheHelper.ClearAll();
                //dictionary = _externalCcRequestHelper.GetAllParticipantsUri();
                //foreach (var participant in dictionary)
                //{
                //    _cacheHelper.Add(participant.Value, participant.Key);
                //}
                //exists = _cacheHelper.Exists(participantCode);
                //if (exists)
                //{
                //    url = _cacheHelper.Get<string>(participantCode);
                //}
                //else
                //{
                //    url = _externalCcRequestHelper.GetParticipantUri(participantCode);
                //    _cacheHelper.Add(url, participantCode);
                //}
                //responseFromExternalCs = _externalCcRequestHelper.ResentExternalCCRequest(soapAction, soapBody, url);
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

            var xDoc = XDocument.Parse(responseFromExternalCs);
            //change AdditionalHeader in soapBody
            if (AppSettings.Get<bool>("SwitchAdditionalHeader"))
            {
                XNamespace defaultNs = "http://mioa.gov.mk/interop/mim/v1";
                xDoc.Descendants(defaultNs + "MIMadditionalHeader").Descendants().Remove();
                xDoc.Descendants().SingleOrDefault(x => x.Name.LocalName == "MIMadditionalHeader").Add(new XElement(defaultNs + "Status", ""), new XElement(defaultNs + "StatusMessage", ""), new XElement(defaultNs + "ProviderEndpointUrl", ""), new XElement(defaultNs + "ExternalEndpointUrl", ""), new XElement(defaultNs + "WebServiceUrl", ""), new XElement(defaultNs + "ConsumerBusCode", ""), new XElement(defaultNs + "TimeStampToken", ""), new XElement(defaultNs + "IsInteropTestCommunicationCall", false));
                responseFromExternalCs = xDoc.ToString();

                var nameLoger = "Switch items in additional header" + DateTime.Now;
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
