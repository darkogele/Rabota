using System.Diagnostics;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Portal.UI.Helpers;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Interop.CC.Portal.UI.Models
{
    public class ClientCertActionFilter : ActionFilterAttribute
    {
        NLogger logger = new NLogger();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

#if DEBUG


            // LOCAL WITH CERTIFICATE

            //X509Certificate cert = new X509Certificate("C:\\Projects\\KORVCCA.p12", "5Aqy9GwE");
            //if (cert != null)
            //{
            //    //TODO Check for cert from PKI
            //    //var name = testCert.GetNameInfo(X509NameType.SimpleName, false);
            //    //var tokenEndpoint = AppSettings.Get<string>("BaseApiUrl") + "token";
            //    //var totalMilliseconds = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            //    //var data = "grant_type=password&username=" + cert.Subject + "&password=" + "&client_id=ngAuthApp";
            //    logger.Info(Environment.NewLine + Environment.NewLine
            //                + "Subject: " + cert.Subject + Environment.NewLine
            //                + "Issuer: " + cert.Issuer + Environment.NewLine
            //                + "Handle: " + cert.Handle + Environment.NewLine + Environment.NewLine);
            //    //var token = WebRequestHelper.GetAccessToken(tokenEndpoint, data);
            //    //filterContext.HttpContext.Items["token"] = token;
            //    filterContext.HttpContext.Items["certSubject"] = cert.Subject;
            //    filterContext.HttpContext.Items["publicKey"] = "testkey";
            //}

            //else
            //{
            //    filterContext.Result =
            //        new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "ErrorCert" } });
            //}

            // LOCAL WITHOUT CERTIFICATE

            //var tokenEndpoint = AppSettings.Get<string>("BaseApiUrl") + "token";
            //var totalMilliseconds = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            //var data = "grant_type=password&username=TestUser" + "&password=" + "&client_id=ngAuthApp-" + totalMilliseconds;
            logger.Info("LOCAL WITHOUT CERTIFICATE - testUser");
            //var token = WebRequestHelper.GetAccessToken(tokenEndpoint, data);
            //filterContext.HttpContext.Items["token"] = token;
            filterContext.HttpContext.Items["certSubject"] = "Certificate";
            filterContext.HttpContext.Items["publicKey"] = "testKey";


#else
            
            // RELEASE

            var certificate = filterContext.HttpContext.Request.ClientCertificate;
            if (certificate.IsPresent)
            {
                //TODO Check for cert from PKI
                //var name = testCert.GetNameInfo(X509NameType.SimpleName, false);
                //var tokenEndpoint = AppSettings.Get<string>("BaseApiUrl") + "token";
                //var totalMilliseconds = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                //var data = "grant_type=password&username=" + certificate.Subject + "&password=" + "&client_id=ngAuthApp-" + totalMilliseconds;
                logger.Info(Environment.NewLine + Environment.NewLine
                            + "Subject: " + certificate.Subject + Environment.NewLine
                            + "Issuer: " + certificate.Issuer + Environment.NewLine
                            + "AllKeys: " + certificate.AllKeys + Environment.NewLine
                            + "BinaryIssuer: " + Convert.ToBase64String(certificate.BinaryIssuer) + Environment.NewLine
                            + "CertEncoding: " + certificate.CertEncoding + Environment.NewLine
                            + "Certificate: " + Convert.ToBase64String(certificate.Certificate) + Environment.NewLine
                            + "Cookie: " + certificate.Cookie + Environment.NewLine
                            + "Flags: " + certificate.Flags + Environment.NewLine
                            + "KeySize: " + certificate.KeySize + Environment.NewLine
                            + "PublicKey: " + Convert.ToBase64String(certificate.PublicKey) + Environment.NewLine
                            + "SecretKeySize: " + certificate.SecretKeySize + Environment.NewLine
                            + "SerialNumber: " + certificate.SerialNumber + Environment.NewLine
                            + "ServerIssuer: " + certificate.ServerIssuer + Environment.NewLine
                            + "ServerSubject: " + certificate.ServerSubject + Environment.NewLine
                            + "ValidFrom: " + certificate.ValidFrom + Environment.NewLine
                            + "ValidUntil: " + certificate.ValidUntil + Environment.NewLine + Environment.NewLine);
                //var token = WebRequestHelper.GetAccessToken(tokenEndpoint, data);
                //filterContext.HttpContext.Items["token"] = token;
                filterContext.HttpContext.Items["certSubject"] = certificate.Subject;
                var pk = Convert.ToBase64String(certificate.PublicKey);
                var cpk=pk.Replace("+", "i2n0t1e5rop");
                logger.Info(cpk);
                filterContext.HttpContext.Items["publicKey"] = cpk;
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "ErrorCert" } });
            }


#endif
        }
    }
}