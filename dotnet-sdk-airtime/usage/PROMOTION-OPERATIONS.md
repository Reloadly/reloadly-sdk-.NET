# Promotions Operations

Reloady also support operators promotions. These are provided by the operators and can be activated by sending a
specific topup amount as per the details of the promotion. Using the promotion operations you can retrieve all details
on the different operators promotions and to showcase these to your customers.

```csharp
private IAirtimeApi _api;

MyClass(IAirtimeApi api) // constructor
{
    _api = api;
}

var promotions0 = await airtimeApi.Promotions.GetByCountryCodeAsync("EC");
var promotions1 = await airtimeApi.Promotions.GetByIdAsync(123);
var promotions2 = await airtimeApi.Promotions.GetByOperatorIdAsync(123);
var promotions3 = await airtimeApi.Promotions.ListAsync();
```
