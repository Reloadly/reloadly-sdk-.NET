using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Dto.Response;
using Reloadly.Airtime.Filter;
using Reloadly.Airtime.Operation;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Enums;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Unit
{
    [TestClass]
    public class OperatorOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task AutoDetect()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("operators/auto-detect/phone/+12345/countries/CC")
                .ResponseBodyFromFile("operator/operator_auto_detect_unfiltered.json");

            var expected = test.ResponseBody<Operator>();
            var operations = new OperatorOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.AutoDetectAsync("+12345", "CC");

            Verify(expected, actual);
        }

        [TestMethod]
        public async Task CalculateFxRate()
        {
            var test = CreateHttpTest()
                .ExpectMethod(HttpMethod.Post)
                .ExpectUriPath("operators/123/fx-rate")
                .ExpectRequestBodyField("amount", 1.2m)
                .ResponseBodyFromFile("operator/operator_fx_rate.json");

            var expected = test.ResponseBody<OperatorFxRate>();
            var operations = new OperatorOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.CalculateFxRateAsync(123, 1.2m);

            Verify(expected, actual);
        }

        [TestMethod]
        public async Task GetById()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("operators/123")
                .ResponseBodyFromFile("operator/operator_auto_detect_filtered.json");

            var expected = test.ResponseBody<Operator>();
            var operations = new OperatorOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.GetByIdAsync(123);

            Verify(expected, actual);
        }

        [TestMethod]
        public async Task List()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("operators")
                .ResponseBodyFromFile("operator/operator_unfiltered_response.json");

            var expected = test.ResponseBody<Page<Operator>>();
            var operations = new OperatorOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.ListAsync();

            VerifyList(expected.Content, actual.Content);
        }

        [TestMethod]
        public async Task ListByCountryCode()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("operators/countries/CC")
                .ResponseBodyFromFile("operator/operators_by_country_code_unfiltered.json");

            var expected = test.ResponseBody<IList<Operator>>();
            var operations = new OperatorOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.ListByCountryCodeAsync("CC");

            VerifyList(expected, actual);
        }
    }
}
