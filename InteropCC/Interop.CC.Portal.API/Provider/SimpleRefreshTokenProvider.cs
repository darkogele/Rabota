using Interop.CC.Models;
using Interop.CC.Models.Models;
using Interop.CC.Models.Repository;
using Interop.CC.Models.UoW;
using Interop.CC.Portal.API.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Interop.CC.Portal.API.Provider
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        // Опис: Методот врши креирање на Рефреш Токен
        // Влезни параметри: AuthenticationTokenCreateContext context
        // Излезни параметри: Task од извршеното креирање на Рефреш Токен
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            using (var _repo = new AuthRepository(new UnitOfWork(new InteropContext())))
            {
                var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                var token = new RefreshToken()
                {
                    Id = Helper.GetHash(refreshTokenId),
                    ClientId = clientid,
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket();

                var result = await _repo.AddRefreshToken(token);

                if (result)
                {
                    context.SetToken(refreshTokenId);
                }

            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        // Опис: Методот врши вчитување на Рефреш Токен од база
        // Влезни параметри: AuthenticationTokenReceiveContext context
        // Излезни параметри: Task од извршеното вчитување на Рефреш Токен
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            //var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Helper.GetHash(context.Token);

            using (var _repo = new AuthRepository(new UnitOfWork(new InteropContext())))
            {
                var refreshToken = await _repo.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    var result = await _repo.RemoveRefreshToken(hashedTokenId);
                }
            }
        }
    }
}