using Newtonsoft.Json;

namespace Reloadly.Airtime.Dto.Request
{
    public class FxRateRequest
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
     
        public FxRateRequest(decimal amount)
        {
            Amount = amount;
        }
    }
}
