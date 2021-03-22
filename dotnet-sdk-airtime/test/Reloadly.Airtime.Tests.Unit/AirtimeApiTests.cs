using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reloadly.Core.Enums;
using System;
using System.Net.Http;

namespace Reloadly.Airtime.Tests.Unit
{
    [TestClass]
    public class AirtimeApiTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldFailToCreateWhenInvalidAccessToken()
        {
            new AirtimeApi(
                new Mock<IHttpClientFactory>().Object,
                "invalidaccesstoken", ReloadlyEnvironment.Sandbox);
        }
    }
}
