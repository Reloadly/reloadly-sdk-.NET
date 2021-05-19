using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Dto.Response;
using Reloadly.Airtime.Operation;
using Reloadly.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Unit
{
    [TestClass]
    public class CountryOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task GetCountryByCode()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("countries/CC")
                .ResponseBodyFromFile("country/country.json");

            var expected = test.ResponseBody<Country>();
            var operations = new CountryOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.GetByCodeAsync("CC");

            Verify(expected, actual);
        }

        [TestMethod]
        public async Task ListCountries()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("countries")
                .ResponseBodyFromFile("country/country_list.json");

            var expected = test.ResponseBody<IList<Country>>();
            var operations = new CountryOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.ListAsync();

            VerifyList(expected, actual);
        }
    }
}
