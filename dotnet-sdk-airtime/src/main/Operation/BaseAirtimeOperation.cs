using Microsoft.Net.Http.Headers;
using Reloadly.Authentication;
using Reloadly.Authentication.Operation;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal;
using Reloadly.Core.Internal.Client;
using Reloadly.Core.Internal.Filter;
using Reloadly.Core.Internal.Net;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public abstract class BaseAirtimeOperation : BaseOperation
    {
        protected string? ClientId;
        protected string? ClientSecret;
        protected string? AccessToken;
        private readonly bool _cacheAccessToken = false;

        protected BaseAirtimeOperation(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId,
            string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, environment)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(clientId));
            Debug.Assert(!string.IsNullOrWhiteSpace(clientSecret));

            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        protected BaseAirtimeOperation(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, environment)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(accessToken));
            AccessToken = accessToken;
            _cacheAccessToken = true;
        }

        protected Uri BuildUri(QueryFilter? filter, string endpoint, params string[] segments)
        {
            var uri = BuildUri(endpoint, segments);
            var uriBuilder = new UriBuilder(uri);

            if (filter != null)
            {
                var query = string.Join('&', filter.Parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
                uriBuilder.Query = query;
            }

            return uriBuilder.Uri;
        }

        protected Uri BuildUri(string endpoint, params string[] segments)
        {
            return new Uri(BaseUri, $"{endpoint}/{string.Join("/", segments)}".TrimEnd('/'));
        }

        protected async Task<ReloadlyRequest<TResponse>> CreateGetRequestAsync<TResponse>(Uri uri)
            where TResponse : class
        {
            var accessToken = await RetrieveAccessTokenAsync();

            return new ReloadlyRequest<TResponse>(HttpMethod.Get, uri)
                .AddHeader(HeaderNames.Accept, ReloadlyApiVersion.AirtimeV1)
                .AddHeader(HeaderNames.Authorization, $"Bearer {accessToken}");
        }

        protected async Task<ReloadlyRequest<TResponse>> CreatePostRequestAsync<TResponse>(Uri uri, object body)
            where TResponse : class
        {
            var accessToken = await RetrieveAccessTokenAsync();

            return new ReloadlyRequest<TResponse>(HttpMethod.Post, uri)
                .AddHeader(HeaderNames.Accept, ReloadlyApiVersion.AirtimeV1)
                .AddHeader(HeaderNames.ContentType, MediaTypeNames.Application.Json)
                .AddHeader(HeaderNames.Authorization, $"Bearer {accessToken}")
                .SetBody(body);
        }

        protected async Task<string> RetrieveAccessTokenAsync()
        {
            if (!string.IsNullOrWhiteSpace(AccessToken)) return AccessToken;
            
            var service = Environment == ReloadlyEnvironment.Live ? ReloadlyService.Airtime : ReloadlyService.AirtimeSandbox;

            var accessToken = await DoGetAccessToken(service);

            if (_cacheAccessToken)
            {
                AccessToken = accessToken;
            }

            return accessToken;
        }

        private async Task<string> DoGetAccessToken(ReloadlyService service)
        {
            var operation =
                new OAuth2Operation(
                    ((ReloadlyHttpClient)HttpClient).Clone(true), AuthenticationApi.BaseUri, ClientId!, ClientSecret!,
                    service);

            var tokenHolder = await operation.GetAccessTokenAsync();
            return tokenHolder.AccessToken!;
        }
    }
}
