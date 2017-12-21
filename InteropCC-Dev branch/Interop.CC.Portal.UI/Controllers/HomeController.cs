using System;
using Interop.CC.Portal.UI.Helpers;
using Interop.CC.Portal.UI.Models;
using System.Configuration;
using System.Web.Mvc;

namespace Interop.CC.Portal.UI.Controllers
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
                ViewBag.ApiCsUrl = ConfigurationManager.AppSettings["ApiCSUrl"];
                ViewBag.BaseApiUri = AppSettings.Get<string>("BaseApiUrl");
                ViewBag.ClientId = AppSettings.Get<string>("ClientId");
                //ViewBag.TokenEndpoint = AppSettings.Get<string>("BaseApiUrl") + "token";
                ViewBag.InstitutionName = ConfigurationManager.AppSettings["InstitutionName"];
                ViewBag.InstitutionCode = ConfigurationManager.AppSettings["InstitutionCode"];
                var isProvider = ConfigurationManager.AppSettings["IsProvider"];;
                if (string.IsNullOrEmpty(isProvider))
                {
                    throw new Exception("Set key prop IsProvider in config!");
                }
                ViewBag.IsProvider = AppSettings.Get<bool>("IsProvider");

                SetEnviromentLookup();
            }
            //var token = HttpContext.Items["token"];
            //ViewBag.TokenUsername = JObject.Parse(token.ToString()).GetValue("userName");
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