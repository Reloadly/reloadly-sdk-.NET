using Newtonsoft.Json;
using System;

namespace Reloadly.Airtime.Dto.Response
{
    public class TopupTransaction
    {
        /// <summary>
        /// Unique Id of the transaction
        /// </summary>
        [JsonProperty("transactionId")]
        public long Id { get; set; }

        /// <summary>
        /// Unique Id of the transaction from the mobile operator if available
        /// </summary>
        public string OperatorTransactionId { get; set; } = default!;

        /// <summary>
        /// Unique Id of the transaction provided by the user during at transaction request if any
        /// </summary>
        public string CustomIdentifier { get; set; } = default!;

        /// <summary>
        /// Unique id of the operator the transaction was sent to
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// Topup recipient phone number (with country prefix)
        /// </summary>
        public string RecipientPhone { get; set; } = default!;

        /// <summary>
        /// Topup recipient email
        /// </summary>
        public string RecipientEmail { get; set; } = default!;

        /// <summary>
        /// Topup sender phone number that was provided at transaction request if any
        /// </summary>
        public string SenderPhone { get; set; } = default!;

        /// <summary>
        /// ISO 3166-1 alpha-2 country code of topup destination country. See https://www.iso.org/obp/ui/#search
        /// </summary>
        public string CountryCode { get; set; } = default!;

        /// <summary>
        /// Name of the mobile operator.
        /// </summary>
        public string OperatorName { get; set; } = default!;

        /// <summary>
        /// Topup amount that was requested by sender
        /// </summary>
        public decimal RequestedAmount { get; set; }

        /// <summary>
        /// Discount amount that was applied to the request sender's account balance for this transaction
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// ISO-4217 3 letter currency code of discount field. See https://www.iso.org/iso-4217-currency-codes.html
        /// </summary>
        public string DiscountCurrencyCode { get; set; } = default!;

        /// <summary>
        /// ISO-4217 3 letter currency code of requestedAmount field. See https://www.iso.org/iso-4217-currency-codes.html
        /// </summary>
        public string RequestedAmountCurrencyCode { get; set; } = default!;

        /// <summary>
        /// Amount that was delivered in local currency
        /// </summary>
        public decimal DeliveredAmount { get; set; }

        /// <summary>
        /// ISO-4217 3 letter currency code of deliveredAmount field. See https://www.iso.org/iso-4217-currency-codes.html
        /// </summary>
        public string DeliveredAmountCurrencyCode { get; set; } = default!;

        /// <summary>
        /// Time stamp recorded for this transaction
        /// </summary>
        [JsonProperty("transactionDate")]
        public DateTime Date { get; set; }

        /// <summary>
        /// User (you) account balance info after this transaction
        /// </summary>
        public TransactionBalanceInfo BalanceInfo { get; set; } = default!;

        /// <summary>
        /// PIN detail info for PIN-based transactions
        /// </summary>
        public PinDetail PinDetail { get; set; } = default!;
    }
}
