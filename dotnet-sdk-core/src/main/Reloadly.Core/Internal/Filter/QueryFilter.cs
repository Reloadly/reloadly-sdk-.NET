using System;

namespace Reloadly.Core.Internal.Filter
{
    public class QueryFilter : BaseFilter
    {
        /// <summary>
        /// Filter by page
        /// </summary>
        /// <param name="pageNumber">the page number to retrieve.</param>
        /// <param name="pageSize">the amount of items per page to retrieve.</param>
        /// <returns>This filter instance.</returns>
        public QueryFilter WithPage(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Filter page number must greater than zero");

            if (pageSize <= 0)
                throw new ArgumentException("Filter page size must greater than zero");

            Parameters["page"] = pageNumber.ToString();
            Parameters["size"] = pageSize.ToString();
            return this;
        }
    }
}
