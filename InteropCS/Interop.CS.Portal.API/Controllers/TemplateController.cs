using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;


namespace Interop.CS.Portal.API.Controllers
{
    public class TemplateController : Controller
    {

        // за Експортирање на податоците од соодветното View
        public ActionResult PrintMessageLogs()
        {
            return View();
        }
    }
}