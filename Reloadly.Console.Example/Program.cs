using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reloadly.Airtime;
using Reloadly.Authentication;
using Reloadly.Core.Enums;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reloadly.Console.Example
{
    class Program
    {
        static async Task Main(string[] _)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .Build();

            var credentials = configuration.GetSection("Credentials").Get<Credentials>();

            // Set up dependency injection
            var services = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .AddHttpClient();

            // Register dependencies
            services.AddSingleton<IAirtimeApi, AirtimeApi>(
                    sp => new AirtimeApi(
                        sp.GetRequiredService<IHttpClientFactory>(),
                        credentials.ClientId, credentials.ClientSecret,
                        ReloadlyEnvironment.Sandbox));

            services.AddSingleton<IAuthenticationApi, AuthenticationApi>(
                sp => new AuthenticationApi(
                    sp.GetRequiredService<IHttpClientFactory>(),
                    credentials.ClientId, credentials.ClientSecret,
                    ReloadlyService.AirtimeSandbox
                ));

            services.AddSingleton<AccountService, AccountService>();

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting application");

            // Use services
            var accountService = serviceProvider.GetService<AccountService>()!;
            await accountService.PrintBalanceAsync();

            System.Console.ReadKey();
        }
    }
}
