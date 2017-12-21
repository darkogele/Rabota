using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PIOMApplicationMVC.Startup))]
namespace PIOMApplicationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
