using System;
using System.IdentityModel.Tokens.Jwt;

namespace Reloadly.Core.Internal.Net
{
    public abstract class ServiceApi
    {
        protected string? ClientId { get; set; }
        protected string? ClientSecret { get; set; }
        protected string? AccessToken { get; set; }

        public ServiceApi(
            string accessToken)
        {
            ValidateAccessToken(accessToken);
            AccessToken = accessToken;
        }

        public ServiceApi(
            string clientId, string clientSecret)
        {
            ValidateCredentials(clientId, clientSecret);

            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        protected static void ValidateCredentials(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId) &&
                string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new ArgumentException(
                    "Client ID and Client Secret are required.");
            }
        }

        private void ValidateAccessToken(string? accessToken)
        {
            JwtSecurityToken token;

            token = new JwtSecurityToken(accessToken);

            var validToUtc = token.ValidTo.ToUniversalTime();
            if (DateTime.UtcNow.Subtract(validToUtc) < TimeSpan.FromMinutes(5))
            {
                throw new ArgumentException("The access token is expired or about to expire.");
            }
        }
    }
}