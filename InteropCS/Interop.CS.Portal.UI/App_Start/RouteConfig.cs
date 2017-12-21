using System.Web.Mvc;
using System.Web.Routing;

namespace Interop.CS.Portal.UI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ModuleMainViews",
                url: "Module/{viewName}",
                defaults: new { controller = "Angular", action = "Index" }
            );

            routes.MapRoute(
               name: "AngularTemplates", 
               url: "App/{Module}/Templates/{Template}",
               defaults: new { controller = "Angular", action = "Template" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { Controller ="Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
