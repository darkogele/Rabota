using System.Web.Http;
using Interop.CC.Portal.API.App_Start;
using Newtonsoft.Json.Serialization;

namespace Interop.CC.Portal.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            UnityConfig.RegisterComponents();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
