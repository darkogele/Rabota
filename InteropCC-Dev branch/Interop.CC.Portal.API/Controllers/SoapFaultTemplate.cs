using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interop.CC.Portal.API.Controllers
{
    public class SoapFaultTemplate : Controller
    {
        public ActionResult PrintSoapFaults()
        {
            return View();
        }
    }
}