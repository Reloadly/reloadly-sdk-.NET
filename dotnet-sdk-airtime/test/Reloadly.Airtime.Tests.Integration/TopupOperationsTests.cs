using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reloadly.Airtime.Dto;
using Reloadly.Airtime.Dto.Request;
using System.Linq;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Tests.Integration
{
    [TestClass]
    public class TopupOperationsTests : BaseOperationsTests
    {
        [TestMethod]
        public async Task Topup()
        {
            var operatorId = (await AirtimeApi.Operators.ListByCountryCodeAsync("GB"))[0].Id;

            var transaction = await AirtimeApi.Topups.SendAsync(new PhoneTopupRequest
            {
                RecipientPhone = new Phone("+447772235236", "GB"),
                OperatorId = operatorId,
                Amount = 10,
                SenderPhone = new Phone("+447772235235", "GB"),
                UseLocalAmount = true
            });

            Assert.AreEqual("447772235236", transaction.RecipientPhone);
        }
    }
}
