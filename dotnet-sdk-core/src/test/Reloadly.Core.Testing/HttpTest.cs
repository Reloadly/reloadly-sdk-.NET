using Newtonsoft.Json;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Net;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Reloadly.Core.Testing
{
    public class HttpTest
    {
        private readonly Uri _baseUri;

        public HttpTest(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        private string? _responseBody = null;
        private readonly MockHttpClientBuilder _httpClientBuilder = new MockHttpClientBuilder();

        public HttpTest ResponseBodyFromFile(string resourceFilename)
        {
            _responseBody = File.ReadAllText($"Resources/{resourceFilename}");
            Debug.Assert(!string.IsNullOrWhiteSpace(_responseBody), $"Cannot read file contents: '{resourceFilename}'.");
            _httpClientBuilder.ReturnsJsonBody(_responseBody);
            return this;
        }

        public IHttpClientFactory Factory
        {
            get => _httpClientBuilder.CreateFactory();
        }

        public IReloadlyHttpClient HttpClient => new ReloadlyHttpClient(Factory, ReloadlyApiVersion.AirtimeV1, false);

        public T ResponseBody<T>()
        {
            Debug.Assert(_responseBody != null, nameof(_responseBody));
            var deserialized = JsonConvert.DeserializeObject<T>(_responseBody);

            if (deserialized == null)
            {
                throw new System.Exception($"Cannot deserialize '{nameof(_responseBody)}'.");
            }

            return deserialized;
        }

        public HttpTest ConfigureOAuth(object responseBody)
        {
            _httpClientBuilder.ConfigureOAuth(responseBody);
            return this;
        }

        public HttpTest ExpectRequestBodyField(string name, object value)
        {
            // todo
            return this;
        }

        public HttpTest ExpectHeader(string name, string value)
        {
            _httpClientBuilder.ExpectsHeader(name, value);
            return this;
        }

        public HttpTest ExpectMethod(HttpMethod method)
        {
            _httpClientBuilder.ExpectsMethod(method);
            return this;
        }

        public HttpTest ExpectUriPath(string path)
        {
            var uri = new Uri(_baseUri, path);
            _httpClientBuilder.ExpectsUri(uri);
            return this;
        }

        public HttpTest StatusCode(HttpStatusCode statusCode)
        {
            _httpClientBuilder.ReturnsStatusCode(statusCode);
            return this;
        }

    }
}
