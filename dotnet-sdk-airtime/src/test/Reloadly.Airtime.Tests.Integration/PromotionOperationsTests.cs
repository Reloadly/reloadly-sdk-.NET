using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Integration
{
    [TestClass]
    public class PromotionOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task GetByCountryCode()
        {
            var promotions = await AirtimeApi.Promotions.GetByCountryCodeAsync("CA");
            var @operator = await AirtimeApi.Operators.GetByIdAsync(promotions[0].OperatorId);

            Assert.AreEqual("United Kingdom", @operator.Country.Name);
        }

        [TestMethod]
        public async Task GetById()
        {
            var promotions = (await AirtimeApi.Promotions.ListAsync()).Content;
            var result = await AirtimeApi.Promotions.GetByIdAsync(promotions[0].Id);

            Assert.AreEqual(promotions[0].Id, result.Id);
        }

        [TestMethod]
        public async Task GetByOperatorId()
        {
            var operatorId = (await AirtimeApi.Operators.ListAsync()).Content[0].Id;
            var promotions = await AirtimeApi.Promotions.GetByOperatorIdAsync(operatorId);

            Assert.AreEqual(operatorId, promotions[0].OperatorId);
        }
    }
}
