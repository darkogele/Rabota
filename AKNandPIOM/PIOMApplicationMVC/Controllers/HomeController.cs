using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PIOMApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastralParcel()
        {
            return View();
        }

        public ActionResult DataForPropertyList()
        {
            return View();
        }
    }
}