# Report Operations

Retrieve various reports such as transaction history etc...

```csharp
private IAirtimeApi _api;

MyClass(IAirtimeApi api) // constructor
{
    _api = api;
}

var transaction = await airtimeApi.Reports.TransactionsHistory.GetByIdAsync("Your-Transaction-Custom-Identifier");

var transactions0 = await airtimeApi.Reports.TransactionsHistory.ListAsync();
var transactions1 = await airtimeApi.Reports.TransactionsHistory.ListAsync(
    new TransactionHistoryFilter()
        .CountryCode("HT")
        .CustomIdentifier("Your-Transaction-Custom-Identifier")
        .EndDate(DateTime.UtcNow)
        .OperatorId(123)
        .OperatorName("Digicel Haiti")
        .StartDate(DateTime.UtcNow.AddDays(-7))
        .WithPage(pageNumber: 1, pageSize: 20)
);
```
