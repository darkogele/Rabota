using System;
using System.Web.Http;
using Interop.CC.Portal.API;
using Interop.CC.Portal.API.App_Start;
using Interop.CC.Portal.API.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Interop.CC.CrossCutting.Logging;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace Interop.CC.Portal.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

        }

        // Конфигурација на OAuth модулот за автентикација
        public void ConfigureAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
#if DEBUG
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(15),
#else 
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
#endif
                Provider = new SimpleAuthorizationServerProvider(new NLogger()),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
