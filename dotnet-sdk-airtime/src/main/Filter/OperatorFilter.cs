using Reloadly.Core.Internal.Filter;

namespace Reloadly.Airtime.Filter
{
    public class OperatorFilter : QueryFilter
    {
        private const string INCLUDE_PIN = "includePin";
        private const string INCLUDE_DATA = "includeData";
        private const string INCLUDE_BUNDLES = "includeBundles";
        private const string INCLUDE_SUGGESTED_AMOUNTS = "suggestedAmounts";
        private const string INCLUDE_RANGE_DENOMINATION_TYPE = "includeRange";
        private const string INCLUDE_FIXED_DENOMINATION_TYPE = "includeFixed";
        private const string INCLUDE_SUGGESTED_AMOUNTS_MAP = "suggestedAmountsMap";

        public OperatorFilter()
        {
            Parameters[INCLUDE_PIN] = "true";
            Parameters[INCLUDE_DATA] = "true";
            Parameters[INCLUDE_BUNDLES] = "true";
            Parameters[INCLUDE_SUGGESTED_AMOUNTS] = "false";
            Parameters[INCLUDE_SUGGESTED_AMOUNTS_MAP] = "false";
            Parameters[INCLUDE_RANGE_DENOMINATION_TYPE] = "true";
            Parameters[INCLUDE_FIXED_DENOMINATION_TYPE] = "true";
        }

        public new OperatorFilter WithPage(int pageNumber, int pageSize)
        {
            return (OperatorFilter)base.WithPage(pageNumber, pageSize);
        }

        /// <summary>
        /// Whether to include pin-based operators in response 
        /// </summary>
        public OperatorFilter IncludePin(bool includePin)
        {
            Parameters[INCLUDE_PIN] = includePin.ToString().ToLowerInvariant();
            return this;
        }

        /// <summary>
        /// Whether to include data operators in response
        /// </summary>
        /// <param name="includeData"></param>
        /// <returns></returns>
        public OperatorFilter IncludeData(bool includeData)
        {
            Parameters[INCLUDE_DATA] = includeData.ToString().ToLowerInvariant();
            return this;
        }

        /// <summary>
        /// Whether to include bundles operators in response
        /// </summary>
        /// <param name="includeBundles"></param>
        /// <returns></returns>
        public OperatorFilter IncludeBundles(bool includeBundles)
        {
            Parameters[INCLUDE_BUNDLES] = includeBundles.ToString().ToLowerInvariant();
            return this;
        }

        /// <summary>
        /// Whether to include suggestedAmount field in response
        /// </summary>
        /// <param name="includeSuggestedAmounts"></param>
        /// <returns></returns>
        public OperatorFilter IncludeSuggestedAmounts(bool includeSuggestedAmounts)
        {
            Parameters[INCLUDE_SUGGESTED_AMOUNTS] = includeSuggestedAmounts.ToString().ToLowerInvariant();
            return this;
        }

        /// <summary>
        /// Whether to include suggestedAmountsMap field in response
        /// </summary>
        /// <param name="includeSuggestedAmountsMap"></param>
        /// <returns></returns>
        public OperatorFilter IncludeSuggestedAmountsMap(bool includeSuggestedAmountsMap)
        {
            Parameters[INCLUDE_SUGGESTED_AMOUNTS_MAP] = includeSuggestedAmountsMap.ToString().ToLowerInvariant();
            return this;
        }

        /// <summary>
        /// Whether to include operators where denomination type is RANGE in response
        /// </summary>
        /// <param name="includeRangeDenominationType"></param>
        /// <returns></returns>
        public OperatorFilter IncludeRangeDenominationType(bool includeRangeDenominationType)
        {
            Parameters[INCLUDE_RANGE_DENOMINATION_TYPE] = includeRangeDenominationType.ToString().ToLowerInvariant();
            return this;
        }

        /// <summary>
        /// Whether to include operators where denomination type is FIXED in response
        /// </summary>
        /// <param name="includeFixedDenominationType"></param>
        /// <returns></returns>
        public OperatorFilter IncludeFixedDenominationType(bool includeFixedDenominationType)
        {
            Parameters[INCLUDE_FIXED_DENOMINATION_TYPE] = includeFixedDenominationType.ToString().ToLowerInvariant();
            return this;
        }
    }
}
