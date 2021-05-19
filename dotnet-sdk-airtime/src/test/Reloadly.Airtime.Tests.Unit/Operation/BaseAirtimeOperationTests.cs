using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Operation;
using Reloadly.Core.Constant;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Filter;
using System;

namespace Reloadly.Airtime.Tests.Unit.Operation
{
    [TestClass]
    public class BaseAirtimeOperationTests
    {
        [TestMethod]
        public void ShouldCreateQueryParameters()
        {
            var filter = new QueryFilter { Parameters = { { "p1", "v1" }, { "p2", "v2" } } };
            var baseUri = new Uri(ServiceUrls.AirtimeSandbox);
            var testOperation = new TestOperationWithImplicitCredentials(baseUri);
            var actual = testOperation.BuildUriImpl(filter, "endpoint", "s1", "s2");

            Assert.AreEqual(
                new Uri($"{ServiceUrls.AirtimeSandbox}/endpoint/s1/s2?p1=v1&p2=v2"),
                actual);
        }
    }

    sealed class TestOperationWithImplicitCredentials : BaseAirtimeOperation
    {
        public TestOperationWithImplicitCredentials(Uri baseUri) :
            base(null!, baseUri, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), ReloadlyEnvironment.Sandbox)
        { }

        public Uri BuildUriImpl(QueryFilter filter, string endpoint, params string[] segments)
            => BuildUri(filter, endpoint, segments);
    }
}
