using Newtonsoft.Json;

namespace Reloadly.Airtime.Dto
{
    public class Phone
    {
        /// <summary>
        /// Creates a new <see cref="Phone"/>.
        /// </summary>
        /// <param name="number">Phone number</param>
        /// <param name="countryCode">
        /// ISO 3166-1 alpha-2 Country code. https://www.iso.org/obp/ui/#search.
        /// </param>
        public Phone(string number, string countryCode)
        {
            Number = number;
            CountryCode = countryCode;
        }

        public Phone()
        {
        }

        /// <summary>
        /// Phone number
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; } = default!;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; } = default!;
    }
}
