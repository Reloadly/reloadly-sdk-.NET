using System;
using System.Net.Http;

namespace Reloadly.Airtime.Tests.Integration
{
    internal sealed class DefaultHttpClientFactory : IHttpClientFactory
    {
        private static readonly Lazy<HttpClient> _httpClientLazy =
            new Lazy<HttpClient>(() => new HttpClient());

        public HttpClient CreateClient(string name) => _httpClientLazy.Value;
    }
}
