# Authentication API

The implementation is based on the [Authentication API Docs](https://developers.reloadly.com/#authentication-api).

Register an `IAuthenticationApi` instance by providing the Application credentials details (client id & secret) from
the [dashboard](https://www.reloadly.com/developers/api-settings).

Setting up the dependency injection:

```csharp
var services = new ServiceCollection()
    .AddHttpClient()
    .AddLogging(builder => builder.AddConsole())
    .AddSingleton<IAuthenticationApi, AuthenticationApi>(
        sp => new AuthenticationApi(
            sp.GetRequiredService<IHttpClientFactory>(),
            credentials.ClientId, credentials.ClientSecret,
            ReloadlyService.AirtimeSandbox));

var authenticationApi = serviceProvider.GetService<IAuthenticationApi>(); // optional
```

```csharp
public MyClass(IAuthenticationApi api) { _api = api; }

public Task<string> GetAccessToken()
{
    return _api.ClientCredentials.GetAccessTokenAsync();
}
```
