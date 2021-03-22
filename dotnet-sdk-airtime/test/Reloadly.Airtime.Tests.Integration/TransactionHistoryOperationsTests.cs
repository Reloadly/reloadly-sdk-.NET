using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Integration
{
    [TestClass]
    public class TransactionHistoryOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task List()
        {
            var transactions = await AirtimeApi.Reports.TransactionsHistory.ListAsync();
            Assert.IsTrue(transactions.Content.Count > 0);
        }

        [TestMethod]
        public async Task GetById()
        {
            var transactions = await AirtimeApi.Reports.TransactionsHistory.ListAsync();
            var transaction = await AirtimeApi.Reports.TransactionsHistory.GetByIdAsync(transactions.Content[0].Id);

            Assert.AreEqual(transactions.Content[0].Id, transaction.Id);
        }
    }
}
