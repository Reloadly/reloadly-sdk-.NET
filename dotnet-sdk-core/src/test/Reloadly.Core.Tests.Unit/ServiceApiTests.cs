using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Reloadly.Core.Tests.Unit
{
    [TestClass]
    public class ServiceApiTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldValidateCredentials()
        {
            new ServiceApiImpl(string.Empty, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldValidateAccessToken()
        {
            new ServiceApiImpl("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MjE2MzU2MzcsImV4cCI6MTY1MzE3MTYzNywiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkpvaG5ueSIsIlN1cm5hbWUiOiJSb2NrZXQiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjpbIk1hbmFnZXIiLCJQcm9qZWN0IEFkbWluaXN0cmF0b3IiXX0.3rYYkCF8CuVUI98ZbO6JM-Wibq7AMMvhl7kOBl0BKDg");
        }
    }
}
