using System.Collections.Generic;

namespace Reloadly.Airtime.Dto.Response
{
    public class Country
    {
        /// <summary>
        /// ISO 3166-1 alpha-2 Country code. See https://www.iso.org/obp/ui/#search
        /// </summary>
        public string IsoName { get; set; } = default!;

        /// <summary>
        /// Full country name
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Account ISO-4217 3 letter currency code for the given country.
        /// See https://www.iso.org/iso-4217-currency-codes.html
        /// </summary>
        public string CurrencyCode { get; set; } = default!;

        /// <summary>
        /// Full currency name
        /// </summary>
        public string CurrencyName { get; set; } = default!;

        /// <summary>
        /// Symbol of currency
        /// </summary>
        public string CurrencySymbol { get; set; } = default!;

        /// <summary>
        /// Url of country flag image
        /// </summary>
        public string Flag { get; set; } = default!;

        /// <summary>
        /// Calling codes of the country
        /// </summary>
        public IList<string> CallingCodes { get; set; } = new List<string>();
    }
}
