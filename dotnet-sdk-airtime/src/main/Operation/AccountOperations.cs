using Reloadly.Airtime.Dto;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Net;
using System;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public class AccountOperations : BaseAirtimeOperation, IAccountOperations
    {
        private const string Endpoint = "accounts";
        private static readonly string BalancePath = $"balance";

        public AccountOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, clientId, clientSecret, environment) { }

        public AccountOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, accessToken, environment) { }

        public async Task<AccountBalanceInfo> GetBalanceAsync()
        {
            var uri = BuildUri(Endpoint, BalancePath);
            var req = await CreateGetRequestAsync<AccountBalanceInfo>(uri);
            return await HttpClient.SendAsync(req);
        }
    }
}
