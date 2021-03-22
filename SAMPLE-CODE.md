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