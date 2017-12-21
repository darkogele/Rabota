using Interop.CC.Models;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Repository;
using Interop.CC.Models.UoW;
using Interop.CC.Portal.API.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Interop.CC.Models.Models;
using Microsoft.Owin.Security;
using System.Web.Http.Cors;
using Interop.CC.CrossCutting.Logging;

namespace Interop.CC.Portal.API.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private string publicKey;
        private List<string> roleNames;
        private ILogger _logger;
        // checking if client has valid auth parameters
        public SimpleAuthorizationServerProvider(ILogger logger)
        {
            _logger = logger;
        }

        // Опис: Методот врши валидација на клиентската автентикација 
        // Влезни параметри: OAuthValidateClientAuthenticationContext context
        // Излезни параметри: Task од извршеното валидирање на внесените кориснички податоци
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            publicKey = context.Parameters["publicKey"];
            _logger.Info("Public key:" + publicKey);
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

        // Опис: Методот врши испраќање на валидираните кориснички податоци до сервер 
        // Влезни параметри: OAuthGrantResourceOwnerCredentialsContext context
        // Излезни параметри: Task од извршеното валидирање на испратените кориснички податоци
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            
            var pk = publicKey;
            var cpk = pk.Replace("i2n0t1e5rop", "+");
            _logger.Info("publicKey changed:" + cpk);
            using (AuthRepository _repo = new AuthRepository(new UnitOfWork(new InteropContext())))
            {
                ApplicationUser user = await _repo.FindUser(context.UserName, context.Password);
                _logger.Info("context.UserName:" + context.UserName);
                _logger.Info("context.Password:" + context.Password);
                _logger.Info("publicKey compare:" + publicKey);
                if(user == null)
                    _logger.Info("Userot e null");
                else
                    _logger.Info("user.PublicKey compare:" + user.PublicKey);
                if (user == null || cpk!=user.PublicKey)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                roleNames = await _repo.FindRoleAsync(user.Id);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType); // bearer token
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            foreach (var role in roleNames)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    { 
                        "userName", context.UserName
                    },
                    {
                        "roles", Newtonsoft.Json.JsonConvert.SerializeObject(roleNames)
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);

            // creating ACCESS TOKEN
            context.Validated(ticket);

        }

        // Опис: Методот врши додавање на соодветни endpoint-и за соодветниот Токен
        // Влезни параметри: OAuthTokenEndpointContext context
        // Излезни параметри: Task од извршеното додавање на endpoint-и за соодветниот Токен
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        // Опис: Методот врши издавање на Рефреш Токен
        // Влезни параметри: OAuthGrantRefreshTokenContext context
        // Излезни параметри: Task од извршеното издавање на Рефреш Токен
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