using System;
using System.Collections.Generic;

namespace Reloadly.Core.Exception
{
    /// <summary>
    /// Represents a server error when a rate limit has been exceeded.
    /// Getters for {@code limit, remaining} and {@code reset} corresponds to {@code X-RateLimit-Limit, X-RateLimit-Remaining} and {@code X-RateLimit-Reset} HTTP headers.
    /// If the value of any headers is missing, then a default value -1 will assigned.
    /// To learn more about rate limits, visit <a href="https://api.reloadly.com/docs/policies/rate-limits">https://api.reloadly.com/docs/policies/rate-limits</a>
    /// </summary>
    public class RateLimitException : ApiException
    {
        /// <summary>
        /// Gets or sets the maximum number of requests available in the current time frame.
        /// </summary>
        public long? Limit { get; set; }

        /// <summary>
        /// Gets or sets the number of remaining requests in the current time frame.
        /// </summary>
        public long? Remaining { get; set; }

        /// <summary>
        /// Gets or sets the expected time when the rate limit will reset.
        /// </summary>
        public DateTime? ExpectedResetTimestamp { get; set; }

        public RateLimitException(string message, string path, int httpStatusCode, string errorCode)
            :base(message, httpStatusCode, path, errorCode)
        {
        }

        public RateLimitException(string message, int httpStatusCode, string path, string errorCode, IEnumerable<object> details)
            :base(message, httpStatusCode, path, errorCode, details)
        {
        }
    }
}
