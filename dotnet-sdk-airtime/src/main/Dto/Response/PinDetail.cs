using Newtonsoft.Json;

namespace Reloadly.Airtime.Dto.Response
{
    public class PinDetail
    {
        /// <summary>
        /// Serial number
        /// </summary>
        private string Serial { get; set; } = default!;

        /// <summary>
        /// Info part 1
        /// </summary>
        [JsonProperty("info1")]
        private string Info { get; set; } = default!;

        /// <summary>
        /// Info part 2
        /// </summary>
        [JsonProperty("info2")]
        private string InfoPart2 { get; set; } = default!;

        /// <summary>
        /// Info part 3
        /// </summary>
        [JsonProperty("info3")]
        private string InfoPart3 { get; set; } = default!;

        /// <summary>
        /// PIN value
        /// </summary>
        private decimal Value { get; set; }

        /// <summary>
        /// PIN code
        /// </summary>
        private string Code { get; set; } = default!;

        /// <summary>
        /// PIN IVR info
        /// </summary>
        private string Ivr { get; set; } = default!;

        /// <summary>
        /// PIN validity info
        /// </summary>
        private string Validity { get; set; } = default!;
    }
}
