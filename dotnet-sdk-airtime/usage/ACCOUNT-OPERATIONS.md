# Account Operations

Use the account operations to perform account related actions.

## Retrieve account balance info

```csharp
private IAirtimeApi _api;

MyClass(IAirtimeApi api) // constructor
{
    _api = api;
}

var myBalance = await api.Accounts.GetBalanceAsync();
```
