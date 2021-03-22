using Reloadly.Authentication.Operation;

namespace Reloadly.Authentication
{
    public interface IAuthenticationApi
    {
        OAuth2Operation ClientCredentials { get; }
    }
}