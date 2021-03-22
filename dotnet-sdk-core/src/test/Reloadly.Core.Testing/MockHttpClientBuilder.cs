using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reloadly.Core.Testing
{
    public class MockHttpClientBuilder
    {
        private HttpMethod _expectsMethod = HttpMethod.Get;
        private Uri? _expectsUri;
        private Dictionary<string, string> _expectsHeaders = new Dictionary<string, string>();
        private Dictionary<string, string> _expectsContentHeaders = new Dictionary<string, string>();
        private object? _oAuthResponseBody = null;
        private string? _returnsBody;
        private HttpStatusCode _returnsStatusCode = HttpStatusCode.OK;

        public MockHttpClientBuilder ConfigureOAuth(object responseBody)
        {
            _oAuthResponseBody = responseBody;
            return this;
        }

        public MockHttpClientBuilder ExpectsMethod(HttpMethod method)
        {
            _expectsMethod = method;
            return this;
        }

        public MockHttpClientBuilder ExpectsUri(Uri uri)
        {
            _expectsUri = uri;
            return this;
        }

        public MockHttpClientBuilder ExpectsContentHeader(string name, string value)
        {
            _expectsContentHeaders[name] = value;
            return this;
        }

        public MockHttpClientBuilder ExpectsHeader(string name, string value)
        {
            _expectsHeaders[name] = value;
            return this;
        }

        public MockHttpClientBuilder ReturnsJsonBody(string json)
        {
            _returnsBody = json;
            return this;
        }

        public MockHttpClientBuilder ReturnsStatusCode(HttpStatusCode statusCode)
        {
            _returnsStatusCode = statusCode;
            return this;
        }

        public HttpClient CreateClient()
        {
            var responseMessage = new HttpResponseMessage(_returnsStatusCode);

            if (_returnsBody != null)
            {
                responseMessage.Content = new StringContent(_returnsBody, Encoding.UTF8, MediaTypeNames.Application.Json);
            }

            responseMessage.RequestMessage = new HttpRequestMessage(_expectsMethod, _expectsUri);
            foreach (var header in _expectsHeaders)
                responseMessage.RequestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            var protectedMock = mockMessageHandler.Protected();

            protectedMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m =>
                        m.Method == _expectsMethod &&
                        (_expectsUri == null || m.RequestUri.ToString() == _expectsUri.ToString()) &&
                        _expectsHeaders
                            .All(h => m.Headers.GetValues(h.Key).Any(v => v == h.Value)) &&
                        _expectsContentHeaders
                            .All(h => m.Content.Headers.GetValues(h.Key).Any(v => v == h.Value))),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            SetupOatuh(protectedMock);

            return new HttpClient(mockMessageHandler.Object);
        }

        private void SetupOatuh(IProtectedMock<HttpMessageHandler> handler)
        {
            if (_oAuthResponseBody != null)
            {
                var authResponseMessage = new HttpResponseMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(_oAuthResponseBody))
                };

                handler
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.Is<HttpRequestMessage>(m =>
                            m.Method.Method == "POST" &&
                            m.RequestUri.ToString().EndsWith("oauth/token")),
                        ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(authResponseMessage);
            }
        }

        public IHttpClientFactory CreateFactory()
        {
            var mock = new Mock<IHttpClientFactory>();
            mock.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(CreateClient());
            return mock.Object;
        }
    }
}
