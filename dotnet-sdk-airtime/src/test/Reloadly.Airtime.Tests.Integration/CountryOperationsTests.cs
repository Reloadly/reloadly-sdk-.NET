using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Integration
{
    [TestClass]
    public class CountryOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task GetCountry()
        {
            var country = await AirtimeApi.Countries.GetByCodeAsync("AF");

            Assert.AreEqual("Afghanistan", country.Name);
        }

        [TestMethod]
        public async Task ListCountries()
        {
            var countries = await AirtimeApi.Countries.ListAsync();

            Assert.IsTrue(countries.Count > 0);
            Assert.IsTrue(countries.Any(c => c.IsoName == "AF"));
        }
    }
}
