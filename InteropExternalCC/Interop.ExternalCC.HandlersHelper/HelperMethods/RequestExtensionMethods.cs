using System;
using System.IO;
using System.Web;
using Interop.ExternalCC.HandlersHelper.Contracts;

namespace Interop.ExternalCC.HandlersHelper.HelperMethods
{
    public class RequestExtensionMethods : IRequestExtensionMethods
    {
        //public static bool IsSoapRequest(this HttpContext context)
        //{
        //    var soapAction = context.Request.Headers["SOAPAction"];

        //    if (soapAction != null)
        //    {
        //        if (!String.IsNullOrEmpty(soapAction))
        //            return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return false;
        //}
        public string GetSoapAction(HttpContext context)
        {
            var soapAction = context.Request.Headers["SOAPAction"];

            if (soapAction != null)
            {
                if (!String.IsNullOrEmpty(soapAction))
                    return soapAction;
            }
            else
            {
                return String.Empty;
            }
            return String.Empty;
        }
        public string GetSoapBody(HttpContext context)
        {
            var inputStream = context.Request.InputStream;
            var tSR = new StreamReader(inputStream);
            return tSR.ReadToEnd();
        }
    }
}