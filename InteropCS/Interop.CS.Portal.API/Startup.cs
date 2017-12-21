using System;
using System.Web.Http;
using Interop.CS.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.UoW;
using Interop.CS.Portal.API;
using Interop.CS.Portal.API.App_Start;
using Interop.CS.Portal.API.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace Interop.CS.Portal.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        // OAuth configuration
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
#if DEBUG
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(2),
#else 
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
#endif
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
