using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Interop.CS.Portal.UI.Helper;
using Interop.CS.Portal.UI.Model;
using Newtonsoft.Json.Linq;
using NLog;

namespace Interop.CS.Portal.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [ClientCertActionFilter]
        public ActionResult Index()
        {
            if (Request.Url != null && Request.ApplicationPath != null)
            {
                ViewBag.BaseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                ViewBag.ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
                //ViewBag.TokenEndpoint = AppSettings.Get<string>("BaseApiUrl") + "token";
                ViewBag.BaseApiUrl = AppSettings.Get<string>("BaseApiUrl");
                ViewBag.ClientId = AppSettings.Get<string>("ClientId");
                ViewBag.ItemsPerPage = ConfigurationManager.AppSettings["ItemsPerPage"];
                ViewBag.InstitutionName = ConfigurationManager.AppSettings["InstitutionName"];
                SetEnviromentLookup();
            }
            //var token = HttpContext.Items["token"];
            //ViewBag.CertName = JObject.Parse(token.ToString()).GetValue("userName");
            //return View(token);
            ViewBag.CertSubject = HttpContext.Items["certSubject"];
            ViewBag.PublicKey = HttpContext.Items["publicKey"];
            return View();
        }

        public ActionResult ErrorCert()
        {
            return View();
        }
        public string SetEnviromentLookup()
        {
            switch (ConfigurationManager.AppSettings["Enviroment"].ToLower())
            {
                case "production":
                    ViewBag.CssClassBackColor = "navbar-production";
                    ViewBag.EnviromentName = "Продукција";
                    break;
                case "development":
                    ViewBag.CssClassBackColor = "navbar-razvoj";
                    ViewBag.EnviromentName = "Развој";
                    break;
                case "test":
                    ViewBag.CssClassBackColor = "navbar-test";
                    ViewBag.EnviromentName = "Тест";
                    break;
            }
            return ViewBag.CssClassBackColor;
        }
    
    }
}