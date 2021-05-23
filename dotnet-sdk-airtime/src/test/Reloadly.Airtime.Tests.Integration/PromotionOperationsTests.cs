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
            var promotions = (await AirtimeApi.Promotions.ListAsync()).Content;
            var @operator = await AirtimeApi.Operators.GetByIdAsync(promotions[0].OperatorId);
            var country = @operator.Country.IsoName;

            promotions = await AirtimeApi.Promotions.GetByCountryCodeAsync(country);
            Assert.IsTrue(promotions.Count > 0);
        }

        [TestMethod]
        public async Task GetById()
        {
            var promotions = (await AirtimeApi.Promotions.ListAsync()).Content;
            var result = await AirtimeApi.Promotions.GetByIdAsync(promotions[0].Id);

            var @operator = await AirtimeApi.Operators.GetByIdAsync(promotions[0].OperatorId);
            var country = @operator.Country.IsoName;

            Assert.AreEqual(promotions[0].Id, result.Id);
        }

        [TestMethod]
        public async Task GetByOperatorId()
        {
            var promotions = (await AirtimeApi.Promotions.ListAsync()).Content;
            promotions = await AirtimeApi.Promotions.GetByOperatorIdAsync(promotions[0].OperatorId);

            Assert.AreEqual(promotions[0].OperatorId, promotions[0].OperatorId);
        }
    }
}
