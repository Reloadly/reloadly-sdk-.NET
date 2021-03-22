# Operators Operations

Apart from supporting Over 140 Countries, Reloadly also supports 600+ Operators. The SDK operators options allows for
the retrieval complete detail of each operator, including what type of operator this is, what topup types it support and
even details on the commissions for the operator.

Within the reloadly platform, There exists two types of Operators. One that support Range values (Anything between the
minimum and maximum range). While the other that support Fixed values (Only a certain values are supported). Reloadly
will return you the type of the operator within the response in denominationType variable. If this is set to ```RANGE```
you will receive the minimum and maximum values in the minAmount and maxAmount variables for that operator. However, if
the denomination type is ```FIXED``` you will not get these values but rather get an array of all values supported in
the fixedAmounts variable. **Now a point to remember here is that these values are already converted into your account's
currency**.

```csharp
private IAirtimeApi _api;

MyClass(IAirtimeApi api) // constructor
{
    _api = api;
}

var @operator0 = await airtimeApi.Operators.AutoDetectAsync("+50936377111", "HT");
var @operator1 = await airtimeApi.Operators.GetByIdAsync(123);
var @operator2 = await airtimeApi.Operators.ListAsync(new OperatorFilter()
    .IncludeBundles(true) //Whether to include bundle operators in the returned resource list. See field "bundle" on the [API Docs](https://developers.reloadly.com/api.html#list-all-operators).
    .IncludeData(true) //Whether to include data (internet) operators in the returned resource list. See field "data" on the [API Docs](https://developers.reloadly.com/api.html#list-all-operators).
    .IncludeFixedDenominationType(true) //Whether to include operators with denomination type FIXED in the returned resource list. See field "denominationType" on the [API Docs](https://developers.reloadly.com/api.html#list-all-operators).
    .IncludePin(true) //Whether to include PIN based operators in the returned resource list. See field "pin" on the [API Docs](https://developers.reloadly.com/api.html#list-all-operators).
    .IncludeRangeDenominationType(true) //Whether to include operators with denomination type RANGE in the returned resource list. See field "denominationType" on the [API Docs](https://developers.reloadly.com/api.html#list-all-operators).
    .IncludeSuggestedAmounts(true) //Whether to populate the suggestedAmounts field on the operators in the returned resource list, this only applies to operators where denominationType is RANGE. See field "suggestedAmounts" on the [API Docs](https://developers.reloadly.com/api.html#list-all-operators).
    .IncludeSuggestedAmountsMap(true)); //Whether to populate the suggestedAmountsMap field on the operators in the returned resource list. This field represents a map of international amounts to local amounts for a given operator where applicable. See field "suggestedAmountsMap" on the [API Docs](https://developers.reloadly.com/api.html#list-all-operators).
var @operator3 = await airtimeApi.Operators.ListByCountryCodeAsync("ES");

// In order to estimate what amount will be received on the receiver end. For example, If your account is in US Dollar and you are trying to send a transaction to a nigerian operator, you can quickly calculate what amount you will receive in Nigerian Naira.
var fxRate = await airtimeApi.Operators.CalculateFxRateAsync(operatorId: 123, 12.3m);
```
