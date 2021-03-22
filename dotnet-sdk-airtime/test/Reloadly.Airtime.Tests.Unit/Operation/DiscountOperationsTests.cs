using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Dto.Response;
using Reloadly.Airtime.Operation;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Filter;
using System;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Unit
{
    [TestClass]
    public class DiscountOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task List()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("operators/commissions")
                .ResponseBodyFromFile("discount/discount_page.json");

            var expected = test.ResponseBody<Page<Discount>>();
            var operations = new DiscountOperations(
                test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.ListAsync();

            VerifyList(expected.Content, actual.Content);
        }

        [TestMethod]
        public async Task GetByOperatorId()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("operators/123/commissions")
                .ResponseBodyFromFile("discount/discount_page.json");

            var expected = test.ResponseBody<Discount>();
            var operations = new DiscountOperations(
                test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.GetByOperatorIdAsync(123);

            Verify(expected, actual);
        }
    }
}
