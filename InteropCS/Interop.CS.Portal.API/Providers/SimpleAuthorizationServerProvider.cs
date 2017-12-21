using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Interop.CS.Portal.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private string publicKey;
        private string roleName;
        // checking if client has valid auth parameters
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            publicKey = context.Parameters["publicKey"];
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            using (var _repo = new AuthRepository(new UnitOfWork(new InteropContext())))
            {
                client = _repo.FindClient(context.ClientId);
                //var splitedClientId = context.ClientId.Split('-');
                //client = _repo.FindClient(splitedClientId[0]);
            }

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        // validating username and password sent to the back-end server.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            var pk = publicKey;
            var cpk = pk.Replace("i2n0t1e5rop", "+");
            using (AuthRepository _repo = new AuthRepository(new UnitOfWork(new InteropContext())))
            {
                ApplicationUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null || cpk != user.PublicKey)
                {
                    if (user != null)
                    {
                    }
                    context.SetError("invalid_grant", "Incorect credentials");
                    return;
                }

                roleName = await _repo.FindRoleAsync(user.Id);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType); // bearer token
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim(ClaimTypes.Role, roleName));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    { 
                        "userName", context.UserName
                    },
                    { 
                        "permision", (roleName == "Admin" || roleName == "SuperAdmin") ? "true" : "false"
                    },
                    { 
                        "superPermision", roleName == "SuperAdmin" ? "true" : "false"
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);

            // creating ACCESS TOKEN
            context.Validated(ticket);

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}