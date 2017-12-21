using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interop.CC.Portal.UI.Controllers
{
    public class AngularController : Controller
    {
        // GET: Angular
        public ActionResult Index(string viewName)
        {
            return PartialView(string.Format("~/App/{0}/{0}.cshtml", viewName));
        }

        public ActionResult Template(string module, string template)
        {
            return PartialView(string.Format("~/App/{0}/Templates/{1}.cshtml", module, template));
        }
    }
}