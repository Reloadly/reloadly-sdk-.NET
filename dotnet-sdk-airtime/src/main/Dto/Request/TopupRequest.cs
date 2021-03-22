using Newtonsoft.Json;

namespace Reloadly.Airtime.Dto.Request
{
    public class TopupRequest
    {
        /// <summary>
        /// Amount (in sender's currency) to credit recipient phone for
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Unique identifier of the destination mobile operator id
        /// </summary>
        [JsonProperty("operatorId")]
        public long OperatorId { get; set; }

        /// <summary>
        /// Phone number of user requesting to credit the recipient phone, this field is optional.
        /// </summary>
        [JsonProperty("senderPhone")]
        public Phone SenderPhone { get; set; } = default!;

        /// <summary>
        /// Indicates whether topup amount is a local amount (as in the same currency as the destination country)
        /// </summary>
        [JsonProperty("useLocalAmount")]
        public bool UseLocalAmount { get; set; }

        /// <summary>
        /// This field can be used to record any kind of info when performing the transaction.
        /// Maximum length allowed for field customIdentifier is 150 characters, this field is optional.
        /// </summary>
        [JsonProperty("customIdentifier")]
        public string CustomIdentifier { get; set; } = default!;
    }
}
