using Newtonsoft.Json;

namespace Reloadly.Airtime.Dto.Response
{
    public class OperatorFxRate
    {
        [JsonProperty("id")]
        public long OperatorId;

        [JsonProperty("name")]
        public string OperatorName { get; set; } = default!;

        public float FxRate;
        public string CurrencyCode { get; set; } = default!;
    }
}
