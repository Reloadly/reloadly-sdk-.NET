using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReloadlyEnvironment = Reloadly.Core.Enums.ReloadlyEnvironment;

namespace Reloadly.Airtime.Tests.Integration
{
    public abstract class BaseOperationsTests
    {
        private readonly Credentials _credentials = new Credentials();

        [TestInitialize]
        public void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Credentials>();

            var configuration = builder.Build();
            configuration.Bind("Credentials", _credentials);
        }

        protected AirtimeApi AirtimeApi
        {
            get
            {
                return new AirtimeApi(
                    new DefaultHttpClientFactory(),
                    _credentials.ClientId,
                    _credentials.ClientSecret,
                    ReloadlyEnvironment.Sandbox);
            }
        }
    }
}
