using Reloadly.Authentication.Operation;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Net;
using System;
using System.Net.Http;

namespace Reloadly.Authentication
{
    /// <summary>
    /// Class that provides an implementation of some of the Authentication and Authorization API methods.
    /// </summary>
    public class AuthenticationApi : IAuthenticationApi
    {
        public static readonly Uri BaseUri = new Uri("https://auth.reloadly.com");
        private readonly IReloadlyHttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly ReloadlyService _service;

        public AuthenticationApi(
            IHttpClientFactory httpClientFactory,
            string clientId, string clientSecret,
            ReloadlyService service)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret));

            _httpClient = new ReloadlyHttpClient(httpClientFactory, ReloadlyApiVersion.AirtimeV1);

            _clientId = clientId;
            _clientSecret = clientSecret;
            _service = service;
        }

        public OAuth2Operation ClientCredentials =>
            new OAuth2Operation(_httpClient, BaseUri, _clientId, _clientSecret, _service);
    }
}

