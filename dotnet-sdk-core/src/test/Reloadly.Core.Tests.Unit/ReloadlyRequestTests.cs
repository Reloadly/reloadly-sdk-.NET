using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Core.Constant;
using Reloadly.Core.Internal;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reloadly.Core.Tests.Unit
{
    [TestClass]
    public class ReloadlyRequestTests
    {
        [TestMethod]
        public async Task ShouldCreate()
        {
            // arrange
            var url = ServiceUrls.ByEnvironment(Enums.ReloadlyEnvironment.Live);
            var reloadlyRequest = new ReloadlyRequest(HttpMethod.Get, new Uri(url));
            reloadlyRequest.SetHeader("Accept", "application/json");
            reloadlyRequest.SetBody(new { name = "bodyValue" });
            reloadlyRequest.SetQueryParameter("param", "queryValue");

            // act
            var request = reloadlyRequest.CreateHttpRequestMessage();

            // assert
            Assert.AreEqual(new Uri(url).Host, request.RequestUri.Host);

            var requestBody = await request.Content.ReadAsStringAsync();
            Assert.IsTrue(requestBody.Contains("\"name\":\"bodyValue\""));

            Assert.AreEqual("?param=queryValue", request.RequestUri.Query);
        }
    }
}
