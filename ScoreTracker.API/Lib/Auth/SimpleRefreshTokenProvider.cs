using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Infrastructure;
using ScoreTracker.API.Model;
using ScoreTracker.API.Model.Repo;

namespace ScoreTracker.API.Lib.Auth
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientId = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientId))
                return;

            var refreshTokenId = Guid.NewGuid().ToString("n");

            using (var repo = new AuthRepository())
            {
                var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                var token = new RefreshToken()
                {
                    Id = refreshTokenId.GetHash(),
                    ClientId = clientId,
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket();

                var result = await repo.AddRefreshToken(token);

                if (result)
                {
                    context.SetToken(refreshTokenId);
                }
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            string hashedTokenId = context.Token.GetHash();

            using (var repo = new AuthRepository())
            {
                var refreshToken = await repo.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    var result = await repo.RemoveRefreshToken(hashedTokenId);
                }
            }
        }
    }
}