﻿using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.External.NinjectConfig;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;

namespace Interop.CC.Handler.External
{
    public class Global : NinjectHttpApplication
    {
        private ILogger _logger;
        public static IKernel NewKernel;
        protected override IKernel CreateKernel()
        {
            NewKernel = new StandardKernel(new WCFNinject());
            return NewKernel;
        }

        public Global()
        {
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
            using (IKernel kernel = new StandardKernel(new WCFNinject()))
            {
                _logger = kernel.Get<ILogger>();
            }
            var exception = Server.GetLastError();
            _logger.Error(exception.Message, exception);
            Server.ClearError();
            //LogHelper.WriteInNLoc("GLOBAL", "GLOBAL", exception.Message, "Request_" + DateTime.Now, "Info");

            //Response.Redirect("/Home/Error");
            //HttpContext.Current.Response.ContentType = "text/xml";
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