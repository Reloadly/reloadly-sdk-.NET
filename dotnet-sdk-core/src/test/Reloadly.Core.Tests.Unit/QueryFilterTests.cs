using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Core.Internal.Filter;

namespace Reloadly.Core.Tests.Unit
{
    [TestClass]
    public class QueryFilterTests
    {
        [TestMethod]
        public void ShouldSetPageAndPageSize()
        {
            var filter = new QueryFilter()
                .WithPage(2, 3);

            Assert.AreEqual("2", filter.Parameters["page"]);
            Assert.AreEqual("3", filter.Parameters["size"]);
        }
    }
}
