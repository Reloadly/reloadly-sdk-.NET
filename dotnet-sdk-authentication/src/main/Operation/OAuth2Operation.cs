using Microsoft.Net.Http.Headers;
using Reloadly.Authentication.Dto.Request;
using Reloadly.Authentication.Dto.Response;
using Reloadly.Core.Constant;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal;
using Reloadly.Core.Internal.Net;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Reloadly.Authentication.Operation
{
    public class OAuth2Operation
    {
        private const string KEY_CLIENT_ID = "client_id";
        private const string KEY_CLIENT_SECRET = "client_secret";
        private const string KEY_GRANT_TYPE = "grant_type";
        private const string KEY_AUDIENCE = "audience";

        private const string PATH_OAUTH = "oauth";
        private const string PATH_TOKEN = "token";

        private readonly IReloadlyHttpClient _httpClient;
        private readonly Uri _baseUri;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly ReloadlyService _service;

        public OAuth2Operation(
            IReloadlyHttpClient httpClient,
            Uri baseUri, string clientId, string clientSecret,
            ReloadlyService service)
        {
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));

            _httpClient = httpClient;
            _baseUri = baseUri;
            _clientId = clientId;
            _service = service;
            _clientSecret = clientSecret;
        }

        /// <summary>
        /// Creates a request to get a Token for the given audience using the 'Client Credentials' grant.
        /// </summary>
        /// <returns>A Request to configure and execute.</returns>
        public Task<OAuthTokenResponse> GetAccessTokenAsync()
        {
            var audience = _service.Url;

            if (!audience.StartsWith("https://"))
            {
                if (audience.StartsWith("http://")) audience = audience["http://".Length..];
                audience = "https://" + audience;
            }

            var builder = new UriBuilder(_baseUri);
            builder.Path = $"{builder.Path.TrimEnd('/')}/{PATH_OAUTH}/{PATH_TOKEN}";

            var requestBody = new TokenRequestBody
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Audience = audience,
                GrantType = GrantType.ClientCredentials
            };

            var request = new ReloadlyRequest<OAuthTokenResponse>(HttpMethod.Post, builder.Uri) { Body = requestBody };

            request
                .SetBody(requestBody)
                .AddHeader(HeaderNames.Accept, MediaTypeNames.Application.Json);

            return _httpClient.SendAsync(request);
        }
    }
}
