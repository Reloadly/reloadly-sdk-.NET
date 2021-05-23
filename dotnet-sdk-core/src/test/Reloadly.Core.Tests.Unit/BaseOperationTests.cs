using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reloadly.Core.Constant;
using Reloadly.Core.Internal.Net;
using System;

namespace Reloadly.Core.Tests.Unit
{
    [TestClass]
    public class BaseOperationTests
    {
        [TestMethod]
        public void ShouldCreate()
        {
            var httpClientMock = new Mock<IReloadlyHttpClient>().Object;
            var baseUri = ServiceUrls.AirtimeAuth;
            var operation = new BaseOperationImpl(httpClientMock, new Uri(baseUri), Enums.ReloadlyEnvironment.Sandbox);

            Assert.AreEqual(httpClientMock.GetHashCode(), operation.HttpClient.GetHashCode());
            Assert.AreEqual(baseUri, operation.BaseUri);
            Assert.AreEqual(Enums.ReloadlyEnvironment.Sandbox, operation.Environment);
        }
    }
}
