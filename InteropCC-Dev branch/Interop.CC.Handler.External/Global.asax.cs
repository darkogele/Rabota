using System;
using System.Web;
using System.Web.Routing;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Internal.NinjectConfig;
using Interop.CC.Models.RepositoryContracts;
using Ninject;

namespace Interop.CC.Handler.Internal
{
    public class Global : HttpApplication
    {
        private readonly ILogger _logger;
        public Global()
        {
            using (IKernel kernel = new StandardKernel(new WCFNinject()))
            {
                _logger = kernel.Get<ILogger>();
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }
        
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Server.ClearError();
            //LogHelper.WriteInNLoc("GLOBAL", "GLOBAL", exception.Message, "Request_" + DateTime.Now, "Info");
            _logger.Error(exception.Message, exception, "Request", "GLOBAL");


            // SoapFaultMessage CreateSoapFault(string code, string subCode, string mTime, string text)
            //var soapFault = _mimMsgHelper.CreateSoapFault("Code value", "Code - SubCode value", "Details - MaxTime value", "External Reason: GLOBAL");

            //// CreateSoapFaultDB(Guid tId, string code, string subCode, string details, Exception e)
            //var soapFaultDB = _soapRequestHelper.CreateSoapFaultDB(Guid.NewGuid(), soapFault.Body.Fault.Code.value, soapFault.Body.Fault.Code.Subcode.value, soapFault.Body.Fault.Detail.maxTime, soapFault.Body.Fault.Reason.Text.value);
            //_soapFaultRepo.InsertSoapFault(soapFaultDB);

            //var soapFaultXml = _mimMsgHelper.CreateFaultMessage(soapFault);

            //HttpContext.Current.Response.ContentType = "application/soap+xml";
            //HttpContext.Current.Response.Write(soapFaultXml.InnerXml);


            //Response.Redirect("/Home/Error");
            //HttpContext.Current.Response.ContentType = "application/soap+xml";
            //HttpContext.Current.Response.Write(exception);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}