using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Reloadly.Core.Constant;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal;
using Reloadly.Core.Internal.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Reloadly.Core.Internal.Utility;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using Reloadly.Core.Exception;

namespace Reloadly.Core.Tests.Unit
{
    [TestClass]
    public class ReloadlyHttpClientTests
    {
        [TestMethod]
        public async Task ShouldSendTelemetry()
        {
            var mhMock = new Mock<HttpMessageHandler>();
            mhMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { Content = new StringContent(string.Empty) });

            var httpClient = new HttpClient(mhMock.Object);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var reloadlyHttpClient = new ReloadlyHttpClient(
                httpClientFactoryMock.Object, ReloadlyApiVersion.AirtimeV1, disableTelemetry: false);

            var response = await reloadlyHttpClient
                .SendAsync(new ReloadlyRequest<object>(HttpMethod.Get, new System.Uri(ServiceUrls.AirtimeSandbox)));

            var reqMessage = (HttpRequestMessage)mhMock.Invocations[0].Arguments[0];
            var header = reqMessage.Headers.First(h => h.Key == "Reloadly-Client").Value.First();
            Base64.TryDecode(header, out var json);
            var jObject = JObject.Parse(json);

            Assert.AreEqual("reloadly-sdk-dotnet", jObject["name"]);
            Assert.AreEqual(ReloadlyApiVersion.AirtimeV1, jObject["api-version"]);

            var sdkVersion = typeof(ReloadlyHttpClient).Assembly.GetName().Version.ToString();
            Assert.AreEqual(sdkVersion, jObject["env"]["reloadly-sdk-dotnet"]);
        }

        [TestMethod]
        public async Task ShouldNotSendTelemetryWhenDisabled()
        {
            IEnumerable<string> discard;

            var mhMock = new Mock<HttpMessageHandler>();
            mhMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Headers.TryGetValues("Reloadly-Client", out discard) == false),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { Content = new StringContent("{}") });

            var httpClient = new HttpClient(mhMock.Object);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var reloadlyHttpClient = new ReloadlyHttpClient(
                httpClientFactoryMock.Object, ReloadlyApiVersion.AirtimeV1, disableTelemetry: true);

            var response = await reloadlyHttpClient
                .SendAsync(new ReloadlyRequest<object>(HttpMethod.Get, new System.Uri(ServiceUrls.AirtimeSandbox)));

            Assert.IsNotNull(response);
        }

        [TestMethod]
        [ExpectedException(typeof(RateLimitException))]
        public async Task ShouldThrowRateLimitException()
        {
            var mhMock = new Mock<HttpMessageHandler>();

            var mockResponse = new HttpResponseMessage
            {
                RequestMessage = new HttpRequestMessage() { RequestUri = new System.Uri("https://topups.reloadly.com") },
                StatusCode = HttpStatusCode.TooManyRequests
            };

            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-Limit", "10");
            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-Remaining", "0");
            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-Reset", "0");

            mhMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            var httpClient = new HttpClient(mhMock.Object);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var reloadlyHttpClient = new ReloadlyHttpClient(
                httpClientFactoryMock.Object, ReloadlyApiVersion.AirtimeV1, disableTelemetry: false);

            var response = await reloadlyHttpClient
                .SendAsync(new ReloadlyRequest<object>(HttpMethod.Get, new System.Uri(ServiceUrls.AirtimeSandbox)));
        }
    }
}
