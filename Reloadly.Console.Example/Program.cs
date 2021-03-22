using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reloadly.Airtime;
using Reloadly.Core.Enums;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reloadly.Console.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .Build();

            var credentials = configuration.GetSection("Credentials").Get<Credentials>();

            // Set up dependency injection
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .AddHttpClient()
                .AddSingleton<IAirtimeApi, AirtimeApi>(
                    sp => new AirtimeApi(
                        sp.GetRequiredService<IHttpClientFactory>(),
                        credentials.ClientId, credentials.ClientSecret,
                        ReloadlyEnvironment.Sandbox))
                .BuildServiceProvider();

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting application");

            // Use the SDK
            var airtimeApi = serviceProvider.GetService<IAirtimeApi>();
            var myBalance = await airtimeApi!.Accounts.GetBalanceAsync();

            logger.LogInformation($"Remaining balance: {myBalance.Amount} {myBalance.CurrencyCode}");
            System.Console.ReadKey();
        }
    }
}
