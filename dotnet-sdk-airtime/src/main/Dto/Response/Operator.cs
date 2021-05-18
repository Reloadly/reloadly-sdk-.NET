using Newtonsoft.Json;
using Reloadly.Airtime.Enums;
using System;
using System.Collections.Generic;

namespace Reloadly.Airtime.Dto.Response
{
    /// <summary>
    ///  Class that represents a Reloadly airtime operator object. Related to the <see cref="OperatorOperations">.
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// Unique id assign to each operator.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the mobile operator.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Whether the mobile operator is a prepaid data bundle. Prepaid bundles are a mixture of calls, data,
        /// SMS and social media access which the users can purchase other than airtime credits
        /// </summary>
        public bool Bundle { get; set; }

        /// <summary>
        /// Whether the operator is a prepaid data only.
        /// </summary>
        public bool Data { get; set; }

        /// <summary>
        /// Whether the operator is pin based
        /// </summary>
        [JsonProperty("pin")]
        public bool PinBased { get; set; }

        /// <summary>
        /// Whether the operator supports local amounts
        /// </summary>
        public bool SupportsLocalAmounts { get; set; }

        public bool SupportsGeographicalRechargePlans { get; set; }

        public IList<GeographicalRechargePlan> GeographicalRechargePlans { get; set; } = Array.Empty<GeographicalRechargePlan>();

        /// <summary>
        /// Operator amount denomination type
        /// </summary>
        public DenominationType DenominationType;

        /// <summary>
        /// ISO-3 currency code of user account
        /// </summary>
        public string SenderCurrencyCode { get; set; } = default!;

        /// <summary>
        /// User account currency symbol.
        /// </summary>
        public string SenderCurrencySymbol { get; set; } = default!;

        /// <summary>
        /// ISO-3 currency code of operator destination country.
        /// </summary>
        public string DestinationCurrencyCode { get; set; } = default!;

        /// <summary>
        /// Destination currency symbol.
        /// </summary>
        public string DestinationCurrencySymbol { get; set; } = default!;

        /// <summary>
        /// International discount assigned for this operator.
        /// </summary>
        public decimal? InternationalDiscount { get; set; }

        /// <summary>
        /// Local discount assigned for this operator.
        /// </summary>
        public decimal? LocalDiscount { get; set; }

        /// <summary>
        /// Most popular international amount for this operator.
        /// </summary>
        [JsonProperty("mostPopularAmount")]
        public decimal? MostPopularInternationalAmount { get; set; }

        /// <summary>
        /// Most popular local amount for this operator.
        /// </summary>
        public decimal? MostPopularLocalAmount { get; set; }

        /// <summary>
        /// Operator's country.
        /// </summary>
        public SimplifiedCountry Country { get; set; } = default!;

        /// <summary>
        /// Current fx rate for this operator.
        /// </summary>
        [JsonProperty("fx")]
        public FxRate FxRate { get; set; } = default!;

        /// <summary>
        /// Suggested whole amounts when denomination type is 'FIXED'.
        /// </summary>
        public IList<int> SuggestedAmounts { get; set; } = Array.Empty<int>();

        /// <summary>
        /// Suggested amounts map containing (amount in sender currency, amount in recipient currency)
        /// when denomination type is 'FIXED'
        /// </summary>
        public Dictionary<decimal, decimal> SuggestedAmountsMap { get; set; } = new Dictionary<decimal, decimal>();

        /// <summary>
        /// Minimum amount when denomination type is 'RANGE' will be empty/null for 'FIXED' denomination type.
        /// </summary>
        public decimal? MinAmount { get; set; }

        /// <summary>
        /// Maximum amount when denomination type is 'RANGE', will be empty/null for 'FIXED' denomination type.
        /// </summary>
        public decimal? MaxAmount { get; set; }

        /// <summary>
        /// Minimum local amount when denomination type is 'RANGE', will be empty/null for 'FIXED' denomination type.
        /// </summary>
        public decimal? LocalMinAmount { get; set; }

        /// <summary>
        /// Maximum local amount when denomination type is 'RANGE', will be empty/null for 'FIXED' denomination.
        /// </summary>
        public decimal? LocalMaxAmount { get; set; }

        /// <summary>
        /// Available operator amounts when denomination type is 'FIXED', will be empty/null for 'RANGE' denomination type.
        /// </summary>
        public IList<decimal> FixedAmounts { get; set; } = Array.Empty<decimal>();

        /// <summary>
        /// Available operator local amounts when denomination type is 'FIXED', will be empty/null for 'RANGE' denomination type.
        /// </summary>
        public IList<decimal> LocalFixedAmounts { get; set; } = Array.Empty<decimal>();

        /// <summary>
        /// International fixed amounts descriptions.
        /// </summary>
        public Dictionary<decimal, string> FixedAmountsDescriptions { get; set; } = new Dictionary<decimal, string>();

        /// <summary>
        /// Local fixed amounts descriptions.
        /// </summary>
        public Dictionary<decimal, string> LocalFixedAmountsDescriptions { get; set; } = new Dictionary<decimal, string>();

        /// <summary>
        /// Logo url of the mobile operator.
        /// </summary>
        public IList<string> LogoUrls { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Available promotions for this operator if any.
        /// </summary>
        public IList<Promotion> Promotions { get; set; } = Array.Empty<Promotion>();
    }
}
