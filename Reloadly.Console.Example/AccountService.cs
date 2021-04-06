using Reloadly.Airtime;
using Reloadly.Airtime.Dto;
using Reloadly.Authentication;
using Reloadly.Core.Exception.Oauth;
using System.Threading.Tasks;

namespace Reloadly.Console.Example
{
    public class AccountService
    {
        private readonly IAuthenticationApi _authenticationApi;
        private readonly IAirtimeApi _airtimeApi;

        public AccountService(
            IAuthenticationApi authenticationApi,
            IAirtimeApi airtimeApi)
        {
            _authenticationApi = authenticationApi;
            _airtimeApi = airtimeApi;
        }

        public async Task PrintBalanceAsync()
        {
            AccountBalanceInfo balance;

            try
            {
                balance = await _airtimeApi.Accounts.GetBalanceAsync();
            }
            catch (ReloadlyOAuthException<AccountBalanceInfo> ex)
            {
                var accessToken = await _authenticationApi.ClientCredentials.GetAccessTokenAsync();
                balance = await _airtimeApi.RefreshTokenForRequest(ex.Request, accessToken.AccessToken!);
            }

            System.Console.WriteLine($"Current Balance: {balance.Amount}");
        }
    }
}
