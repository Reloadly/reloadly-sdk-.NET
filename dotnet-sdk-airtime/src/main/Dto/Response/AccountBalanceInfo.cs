using Newtonsoft.Json;
using System;

namespace Reloadly.Airtime.Dto
{
    public class AccountBalanceInfo
    {
        /// <summary>
        /// Current account balance amount
        /// </summary>
        [JsonProperty("balance")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Account ISO-4217 3 letter currency code. See https://www.iso.org/iso-4217-currency-codes.html.
        /// Example : USD
        /// </summary>
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; } = default!;

        /// <summary>
        /// Account currency name for the given currency code, example "United States Dollar"
        /// </summary>
        [JsonProperty("currencyName")]
        public string CurrencyName { get; set; } = default!;

        /// <summary>
        /// Account balance last updated date
        /// </summary>
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
