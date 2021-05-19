using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Filter;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Integration
{
    [TestClass]
    public class OperatorOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task AutoDetect()
        {
            var result = await AirtimeApi.Operators.AutoDetectAsync("+905435554433", "TR");

            Assert.IsNotNull(result);
            Assert.AreEqual("Turkey", result.Country.Name);
        }

        [TestMethod]
        public async Task CalculateFxRate()
        {
            var operators = await AirtimeApi.Operators.ListByCountryCodeAsync("GB");
            var result = await AirtimeApi.Operators.CalculateFxRateAsync(operators[0].Id, 1);

            Assert.IsTrue(result.FxRate > 0.5);
            Assert.IsTrue(result.FxRate < 1.5);
        }

        [TestMethod]
        public async Task GetById()
        {
            var operators = await AirtimeApi.Operators.ListByCountryCodeAsync("GB");
            var result = await AirtimeApi.Operators.GetByIdAsync(operators[0].Id);

            Assert.AreEqual(operators[0].Id, result.Id);
        }

        [TestMethod]
        public async Task List()
        {
            var result = await AirtimeApi.Operators.ListAsync();

            Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task ListWithFilters()
        {
            var filter = new OperatorFilter()
                .IncludeData(true)
                .IncludeBundles(true)
                .IncludeFixedDenominationType(true)
                .IncludePin(true)
                .IncludeRangeDenominationType(true)
                .IncludeSuggestedAmounts(true)
                .IncludeSuggestedAmountsMap(true);

            var result = await AirtimeApi.Operators.ListAsync(filter);

            Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task ListByCountryCode()
        {
            var result = await AirtimeApi.Operators.ListByCountryCodeAsync("TR");
            Assert.AreEqual("Turkey", result[0].Country.Name);
        }
    }
}
