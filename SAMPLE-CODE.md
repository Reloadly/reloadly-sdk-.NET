# Usage And Sample Code

## Usage

Set up dependency injection:

```csharp
services.AddHttpClient();
services.AddSingleton<IAirtimeApi, AirtimeApi>(
    sp => new AirtimeApi(
        sp.GetRequiredService<IHttpClientFactory>(),
        credentials.ClientId, credentials.ClientSecret,
        ReloadlyEnvironment.Sandbox))
```

Use the SDK:

```csharp
var airtimeApi = serviceProvider.GetService<IAirtimeApi>();
var myBalance = await airtimeApi!.Accounts.GetBalanceAsync();
```

## Customizing The API Client Instance

### Configuring Timeouts

You may configure HTTP timeout while registering the IHttpClient:

```csharp
services
    .AddHttpClient<AirtimeApi>(httpClient =>
    {
        httpClient.Timeout = TimeSpan.FromSeconds(5);
    });
```

### Proxy Configuration

```csharp
var httpProxy = new WebProxy(
    "proxy address", BypassOnLocal: false, Array.Empty<string>(), new NetworkCredential("username", "password"));

services
    .AddHttpClient<AirtimeApi>(httpClient => { })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
    {
        Proxy = httpProxy
    });
```

## Handling Authentication Exceptions

An operation may throw a `ReloadlyOAuthException` when the access token expires. When this happens, you will need to refresh the access token. Once the token is refreshed, you may want to repeat the request rather than displaying an error to the end user. To easily repeat a request, the exception thrown will have a `Request` property containing a repeatable `ReloadlyRequest` object.

You may see a working example of this implementation in the [Reloadly.Console.Example](Reloadly.Console.Example/README.md) project,

```csharp
public MyService(
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

    // Use the response
    var myBalance = balance.Amount;
}
```

### Request Latency Telemetry

By default, the library sends request latency telemetry to Reloadly. These numbers help Reloadly improve the overall
latency of its API for all users.

You can disable this behavior if you prefer:

```csharp
services.AddSingleton<IAirtimeApi, AirtimeApi>(
    sp => new AirtimeApi(
        sp.GetRequiredService<IHttpClientFactory>(),
        "client id", "client secret",
        ReloadlyEnvironment.Sandbox,
        disableTelemetry: true))
```

## Sample Code

### Authentication SDK

* [Usage](dotnet-sdk-authentication/USAGE.md)

### Airtime SDK

* [Account Operations](dotnet-sdk-airtime/usage/ACCOUNT-OPERATIONS.md)
* [Country Operations](dotnet-sdk-airtime/usage/COUNTRY-OPERATIONS.md)
* [Discount Operations](dotnet-sdk-airtime/usage/DISCOUNT-OPERATIONS.md)
* [Operator Operations](dotnet-sdk-airtime/usage/OPERATOR-OPERATIONS.md)
* [Promotion Operations](dotnet-sdk-airtime/usage/PROMOTION-OPERATIONS.md)
* [Report Operations](dotnet-sdk-airtime/usage/REPORT-OPERATIONS.md)
* [Topup Operations](dotnet-sdk-airtime/usage/TOPUP-OPERATIONS.md)