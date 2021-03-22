using Newtonsoft.Json;

namespace Reloadly.Airtime.Dto.Request
{
    public class PhoneTopupRequest : TopupRequest
    {
        [JsonProperty("recipientPhone")]
        public Phone RecipientPhone { get; set; } = default!;
    }
}