# Countries Operations

Reloadly supports 140+ Countries around the globe. You can get a list of all or specific supported countries. The
response will give you a list with complete details, iso, flag as well as calling codes for each country. You can also
further filter the countries by getting details for a specific country by its ISO-Alpha2 code.
See https://www.nationsonline.org/oneworld/country_code_list.htm for more details regarding country ISO codes.

```csharp
private IAirtimeApi _api;

MyClass(IAirtimeApi api) // constructor
{
    _api = api;
}

// Get Countries
var countries = await _api.Countries.ListAsync();

// Get Country by ISO Code
var country = await _api.Countries.GetByCodeAsync("GB");
```
