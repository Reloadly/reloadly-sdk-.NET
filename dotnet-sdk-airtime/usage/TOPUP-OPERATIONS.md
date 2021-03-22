# Topup Operations

In order to send a successful topup. There are a few prerequisites to the system. We need to know the phone number to
send the topup to, the operator id of the phone number, the country of the operator, the amount for the topup.

Reloadly also provides the ability to top up using local values. By default, the values will be available in your
account’s currency, but you can maintain one wallet and top up in different local values from many countries. **Please
note that this is only possible when local values are supported in Reloadly system**. If you want to send exact amounts
in Local/Operator's/Receiver's currency, then simply set the `.UseLocalAmount(true)` on the request object. This
will tell the platform that you are sending the amount in local currency and not in your dashboard's currency or
international pipe. Not all operator's support a local amount yet so make sure to check the operator's details to know
whether it supports local or not.

Reloadly also supports Nauta Cuba for top-ups. However the process is a bit different from sending phone topups. Instead
of using `PhoneTopupRequest` use `EmailTopupRequest`, substitute `RecipientPhone(phone)` with
`RecipientEmail(email)` and that's it. The rest of the process is exactly the same as sending any other topup.

Note, There are two types of email domains that are allowed for Nauta Cuba Top-up : `@nauta.com.cu`
and `@nauta.co.cu`.

```csharp
private IAirtimeApi _api;

MyClass(IAirtimeApi api) // constructor
{
    _api = api;
}

var transaction0 = await airtimeApi.Topups.SendAsync(new PhoneTopupRequest
{
    Amount = 15m, //The amount is in your Reloadly account currency
    CustomIdentifier = Guid.NewGuid().ToString(), // Optional, your own internal reference.
    SenderPhone = new Phone("+17862541236", "US"),
    RecipientPhone = new Phone("+50936377111", "HT")
});

var transaction1 = await airtimeApi.Topups.SendAsync(new PhoneTopupRequest
{
    UseLocalAmount = true, // Since 'UseLocalAmount' is set to 'true' ...
    Amount = 2000, // ... the amount is two thousand Nigerian Naira
    SenderPhone = new Phone("+17862541236", "US"),
    RecipientPhone = new Phone("+2349045150334", "NG")
});

// Nauta Cuba
var transaction2 = await airtimeApi.Topups.SendAsync(new EmailTopupRequest
{
    Amount = 15m, //The amount is in your Reloadly account currency
    SenderPhone = new Phone("+17862541236", "US"),
    RecipientEmail = "example@nauta.com.cu"
});
```
