using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Dto.Response;
using Reloadly.Airtime.Operation;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Unit
{
    [TestClass]
    public class PromotionOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task GetByCountryCode()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("promotions/countries/CC")
                .ResponseBodyFromFile("promotion/promotion_list_by_country.json");

            var expected = test.ResponseBody<IList<Promotion>>();
            var operations = new PromotionOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.GetByCountryCodeAsync("CC");

            VerifyList(expected, actual);
        }

        [TestMethod]
        public async Task GetById()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("promotions/123")
                .ResponseBodyFromFile("promotion/promotion_by_id.json");

            var expected = test.ResponseBody<Promotion>();
            var operations = new PromotionOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.GetByIdAsync(123);

            Verify(expected, actual);
        }

        [TestMethod]
        public async Task GetByOperatorId()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("promotions/operators/123")
                .ResponseBodyFromFile("promotion/promotion_list_by_operator.json");

            var expected = test.ResponseBody<IList<Promotion>>();
            var operations = new PromotionOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.GetByOperatorIdAsync(123);

            VerifyList(expected, actual);
        }

        [TestMethod]
        public async Task List()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("promotions")
                .ResponseBodyFromFile("promotion/promotion_filtered_page.json");

            var expected = test.ResponseBody<Page<Promotion>>();
            var operations = new PromotionOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.ListAsync();

            VerifyList(expected.Content, actual.Content);
        }
    }
}
