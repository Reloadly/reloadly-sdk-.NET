using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Dto;
using Reloadly.Airtime.Operation;
using Reloadly.Core.Enums;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Unit
{
    [TestClass]
    public class AccountOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task GetBalance()
        {
            var test = CreateHttpTest()
                .ExpectUriPath("accounts/balance")
                .ResponseBodyFromFile("account/account_balance.json");

            var expected = test.ResponseBody<AccountBalanceInfo>();

            var operations = new AccountOperations(test.HttpClient, BaseUri, ClientId, ClientSecret, ReloadlyEnvironment.Sandbox);
            var actual = await operations.GetBalanceAsync();

            Verify(expected, actual);
        }
    }
}
