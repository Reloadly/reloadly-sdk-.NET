using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Integration
{
    [TestClass]
    public class AccountOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task GetAccountBalance()
        {
            var balance = await AirtimeApi.Accounts.GetBalanceAsync();

            Assert.IsTrue(balance.UpdatedAt > DateTime.MinValue);
        }
    }
}
