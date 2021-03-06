using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Reloadly.Core.Internal.Filter;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Integration
{
    [TestClass]
    public class DiscountOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task GetAccountBalanceWithPage()
        {
            var result1 = await AirtimeApi.Discounts.ListAsync();
            var page2 = new QueryFilter().WithPage(2, 5);
            var result2 = await AirtimeApi.Discounts.ListAsync(page2);

            Assert.AreNotEqual(JsonConvert.SerializeObject(result1), JsonConvert.SerializeObject(result2));
        }

        [TestMethod]
        public async Task GetByOperatorId()
        {
            var page = new QueryFilter().WithPage(2, 5);
            var result = await AirtimeApi.Discounts.ListAsync(page);
            var operatorId = result.Content[0].Operator.Id;
            var discountResult = await AirtimeApi.Discounts.GetByOperatorIdAsync(operatorId);

            Assert.AreEqual(operatorId, discountResult.Operator.Id);
        }
    }
}
