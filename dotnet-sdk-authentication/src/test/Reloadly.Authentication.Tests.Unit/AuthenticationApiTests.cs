using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reloadly.Authentication.Dto.Response;
using Reloadly.Authentication.Operation;
using Reloadly.Core.Constant;
using Reloadly.Core.Enums;
using Reloadly.Core.Exception.Oauth;
using Reloadly.Core.Internal.Net;
using Reloadly.Core.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Reloadly.Authentication.Tests.Unit
{
    [TestClass]
    public class OAuth2OperationTests : BaseOperationsTests
    {
        private const string CLIENT_ID = "some-client-id";
        private const string CLIENT_SECRET = "some-client-secret";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionWhenClientIdIsEmpty()
        {
            var httpClientMock = new Mock<IReloadlyHttpClient>();
            new OAuth2Operation(
                httpClientMock.Object, AuthenticationApi.BaseUri, " ", CLIENT_SECRET, ReloadlyService.AirtimeSandbox);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionWhenClientSecretIsEmpty()
        {
            var httpClientMock = new Mock<IReloadlyHttpClient>();
            new OAuth2Operation(
                httpClientMock.Object, AuthenticationApi.BaseUri, CLIENT_ID, " ", ReloadlyService.AirtimeSandbox);
        }

        [TestMethod]
        public async Task ShouldCreateOAuth2ClientCredentialsTokenRequest()
        {
            var httpTest = new HttpTest(new Uri(ServiceUrls.AirtimeAuth))
                .ExpectMethod(HttpMethod.Post)
                .ExpectHeader(HeaderNames.Accept, MediaTypeNames.Application.Json)
                .ExpectRequestBodyField("client_id", CLIENT_ID)
                .ExpectRequestBodyField("client_secret", CLIENT_SECRET)
                .ExpectUriPath("oauth/token")
                .ExpectRequestBodyField("grant_type", GrantType.ClientCredentials)
                .ResponseBodyFromFile("auth/success_token_response.json");

            var expected = httpTest.ResponseBody<OAuthTokenResponse>();

            var oAuth2Operation = new OAuth2Operation(
                httpTest.HttpClient, AuthenticationApi.BaseUri, CLIENT_ID, CLIENT_SECRET, ReloadlyService.AirtimeSandbox);

            var actual = await oAuth2Operation.GetAccessTokenAsync();

            Verify(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ReloadlyOAuthException<OAuthTokenResponse>))]
        public async Task ShouldThrowReloadlyOAuthException()
        {
            var httpTest = new HttpTest(new Uri(ServiceUrls.AirtimeAuth))
                .ExpectMethod(HttpMethod.Post)
                .ExpectHeader(HeaderNames.Accept, MediaTypeNames.Application.Json)
                .ExpectUriPath("oauth/token")
                .ExpectRequestBodyField("grant_type", GrantType.ClientCredentials)
                .ExpectRequestBodyField("client_id", CLIENT_ID)
                .ExpectRequestBodyField("client_secret", CLIENT_SECRET)
                .ResponseBodyFromFile("auth/error_access_denied.json")
                .StatusCode(HttpStatusCode.Unauthorized);

            var oAuth2Operation = new OAuth2Operation(
                httpTest.HttpClient, AuthenticationApi.BaseUri, CLIENT_ID, CLIENT_SECRET, ReloadlyService.AirtimeSandbox);

            try
            {
                var tokenRequest = await oAuth2Operation.GetAccessTokenAsync();
            }
            catch (ReloadlyOAuthException ex)
            {
                Assert.AreEqual(401, ex.HttpStatusCode);
                throw;
            }
            catch
            {
                Assert.Fail("A generic exception was thrown.");
            }

            Assert.Fail("An exception was not thrown.");
        }
    }
}
