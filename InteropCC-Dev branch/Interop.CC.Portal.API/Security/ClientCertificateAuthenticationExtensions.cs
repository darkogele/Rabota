
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using Owin;

namespace Interop.CC.Portal.API.Security
{
    public static class ClientCertificateAuthenticationExtensions
    {
        public static IAppBuilder UseClientCertificateAuthentication(this IAppBuilder app,
            X509RevocationMode revocationMode = X509RevocationMode.Online, bool createExtendedClaims = false)
        {
            X509CertificateValidator chainTrustValidator = X509CertificateValidator.CreateChainTrustValidator(true,
                new X509ChainPolicy()
                {
                    RevocationMode = revocationMode
                });
            ClientCertificateAuthenticationOptions options = new ClientCertificateAuthenticationOptions("X.509")
            {
                Validator = chainTrustValidator,
                CreateExtendedClaimSet = createExtendedClaims
            };
            return ClientCertificateAuthenticationExtensions.UseClientCertificateAuthentication(app, options);
        }

        public static IAppBuilder UseClientCertificateAuthentication(this IAppBuilder app,
            ClientCertificateAuthenticationOptions options)
        {
            AppBuilderUseExtensions.Use<IAppBuilder>(app, options); // (app,new object());
            return app;
        }
    }
}