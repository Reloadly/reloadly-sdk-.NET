using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ReloadlyEnvironment = Reloadly.Core.Enums.ReloadlyEnvironment;

namespace Reloadly.Airtime.Tests.Integration
{
    public abstract class BaseOperationsTests
    {
        private readonly Credentials _credentials = new Credentials();

        [TestInitialize]
        public void Initialize()
        {
            var dict = Environment.GetEnvironmentVariables();
            foreach (var key in dict.Keys)
            {
                if (key?.ToString().StartsWith("Credentials") ?? false)
                {
                    Console.WriteLine(key?.ToString() + "=" + dict[key]?.ToString());
                }
            }

            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Credentials>()
                .AddEnvironmentVariables();

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
