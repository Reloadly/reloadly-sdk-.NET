using Newtonsoft.Json;
using System;

namespace Reloadly.Airtime.Dto.Response
{
    public class TransactionBalanceInfo
    {
        /// <summary>
        /// Account balance prior to the transaction
        /// </summary>
        [JsonProperty("oldBalance")]
        private decimal PreviousBalance { get; set; }

        /// <summary>
        /// Current account balance amount
        /// </summary>
        [JsonProperty("newBalance")]
        private decimal CurrentBalance { get; set; }

        /// <summary>
        /// Account ISO-4217 3 letter currency code. See https://www.iso.org/iso-4217-currency-codes.html.
        /// </summary>
        /// <example>USD</example>
        private string CurrencyCode { get; set; } = default!;

        /// <summary>
        /// Account currency name for the given currency code, example "United States Dollar"
        /// </summary>
        private string CurrencyName { get; set; } = default!;

        /// <summary>
        /// Account balance last updated date
        /// </summary>
        private DateTime UpdatedAt { get; set; }
    }
}
