using Newtonsoft.Json;

namespace Reloadly.Airtime.Dto.Request
{
    public class EmailTopupRequest : TopupRequest
    {
        [JsonProperty("recipientEmail")]
        public string RecipientEmail { get; set; } = default!;
    }
}
