using System.Collections.Generic;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Interop.CC.Portal.API.Security
{
    public class ClientCertificateAuthenticationHandler :
        AuthenticationHandler<ClientCertificateAuthenticationOptions>
    {
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var cert = Context.Get<X509Certificate2>("ssl.ClientCertificate");
            if (cert == null)
            {
                return Task.FromResult<AuthenticationTicket>(null);
            }
            try
            {
                Options.Validator.Validate(cert);
            }
            catch
            {
                return Task.FromResult<AuthenticationTicket>(null);
            }
            //var claims = GetClaimsFromCertificate(
            //  cert, cert.Issuer, Options.CreateExtendedClaimSet);
            var claims = new List<Claim>();
            var identity = new ClaimsIdentity(Options.AuthenticationType);
            identity.AddClaims(claims);
            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            return Task.FromResult<AuthenticationTicket>(ticket);
        }
    }
}