using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Reloadly.Airtime.Dto;
using Reloadly.Core.Constant;
using Reloadly.Core.Enums;
using Reloadly.Core.Exception.Oauth;
using Reloadly.Core.Internal.Net;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Unit.Operation
{
    [TestClass]
    public class RefreshAccessTokenTests
    {
        private const string InvalidToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.5mhBHqs5_DTLdINd9p5m7ZJ6XD0Xc55kIaCRY5r6HRA";
        private const string ValidToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.XczKUIBWB0viqqDiXT-KU75vJsrS7CkxvZiBL6c6oc8";

        [TestMethod]
        public async Task AirtimeApiShouldRefreshAccessToken()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var protectedHandlerMock = handlerMock.Protected();

            protectedHandlerMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(
                        m => m.Headers.GetValues(HeaderNames.Authorization).First() == $"Bearer {ValidToken}"),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(AccountBalanceResponseMessage);

            protectedHandlerMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(
                        m => m.Headers.GetValues(HeaderNames.Authorization).First() == $"Bearer {InvalidToken}"),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(FailedOAuthResponseMessage);

            var httpClient = new HttpClient(handlerMock.Object);
            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var api = new AirtimeApi(factoryMock.Object, InvalidToken, ReloadlyEnvironment.Sandbox);

            try
            {
                var _ = await api.Accounts.GetBalanceAsync();
            }
            catch (ReloadlyOAuthException<AccountBalanceInfo> ex)
            {
                var response = await api.RefreshTokenForRequest(ex.Request, ValidToken);

                var reloadlyHttpClient = new ReloadlyHttpClient(factoryMock.Object, ReloadlyApiVersion.AirtimeV1);
                var result = await reloadlyHttpClient.SendAsync(ex.Request);
                Assert.AreEqual(9m, result?.Amount);
            }
        }

        private HttpResponseMessage FailedOAuthResponseMessage =>
            new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage =
                    new HttpRequestMessage(
                        HttpMethod.Get,
                        new Uri(new Uri(ServiceUrls.AirtimeAuth), "oauth/token")),
                Content = new StringContent("{}")
            };

        private HttpResponseMessage AccountBalanceResponseMessage =>
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new AccountBalanceInfo { Amount = 9m }))
            };
    }
}
