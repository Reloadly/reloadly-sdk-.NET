using Reloadly.Core.Internal.Filter;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Reloadly.Airtime.Filter
{
    /// <summary>
    /// Class used to filter the results received when calling topup transaction history endpoint.
    /// Related to the <see cref="TopupOperations">.
    /// </summary>
    public class TransactionHistoryFilter : QueryFilter
    {
        internal const string END_DATE = "endDate";
        internal const string START_DATE = "startDate";
        private const string OPERATOR_ID = "operatorId";
        private const string COUNTRY_CODE = "countryCode";
        private const string OPERATOR_NAME = "operatorName";
        private const string CUSTOM_IDENTIFIER = "customIdentifier";

        internal const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public new TransactionHistoryFilter WithPage(int pageNumber, int pageSize)
        {
            base.WithPage(pageNumber, pageSize);
            return this;
        }

        public TransactionHistoryFilter OperatorId(long operatorId)
        {
            Debug.Assert(operatorId > 0);
            Parameters[OPERATOR_ID] = operatorId.ToString();
            return this;
        }

        public TransactionHistoryFilter CountryCode(string countryCode)
        {
            Parameters[COUNTRY_CODE] = countryCode;
            return this;
        }

        public TransactionHistoryFilter OperatorName(string operatorName)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(operatorName));
            Parameters[OPERATOR_NAME] = operatorName;
            return this;
        }

        public TransactionHistoryFilter CustomIdentifier(string customIdentifier)
        {
            Debug.Assert(string.IsNullOrWhiteSpace(customIdentifier));
            Parameters[CUSTOM_IDENTIFIER] = customIdentifier;
            return this;
        }

        /// <summary>
        /// Sets the local start ate.
        /// </summary>
        /// <param name="startDate">Local start date.</param>
        public TransactionHistoryFilter StartDate(DateTime startDate)
        {
            Parameters[START_DATE] = startDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            return this;
        }

        /// <summary>
        /// Sets the local end date.
        /// </summary>
        /// <param name="endDate">Local end date.</param>
        /// <returns></returns>
        public TransactionHistoryFilter EndDate(DateTime endDate)
        {
            Parameters[END_DATE] = endDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            return this;
        }
    }
}
