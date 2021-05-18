using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Dto.Response;
using Reloadly.Airtime.Operation;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Enums;
using System;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Unit
{
    [TestClass]
    public class TransactionHistoryOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task List()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("topups/reports/transactions")
                .ResponseBodyFromFile("report/transaction_history_unfiltered_page.json");

            var expected = test.ResponseBody<Page<TopupTransaction>>();
            var operations = new TransactionHistoryOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.ListAsync();

            VerifyList(expected.Content, actual.Content);
        }

        [TestMethod]
        public async Task GetById()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("topups/reports/transactions/1")
                .ResponseBodyFromFile("report/transaction_history_by_id.json");

            var expected = test.ResponseBody<TopupTransaction>();
            var operations = new TransactionHistoryOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.GetByIdAsync(1);

            Verify(expected, actual);
        }
    }
}
