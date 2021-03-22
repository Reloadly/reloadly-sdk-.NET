using System;
using System.Collections.Generic;
using System.Linq;

namespace Reloadly.Core.Exception
{
    [Serializable]
    public class ApiException : ReloadlyException
    {
        protected ApiException() : base() { }

        protected ApiException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            Path = default!;
        }

        /// <summary>
        /// Additional details that might be helpful in understanding the error(s) that occurred.
        /// </summary>
        public IList<object> Details { get; set; } = new List<object>();

        /// <summary>
        /// For some errors that could be handled programmatically, a string summarizing the error reported.
        /// </summary>
        public string? ErrorCode { get; set; }

        /// <summary>
        /// HTTP status indicate whether a specific HTTP request has been successfully completed.
        /// Responses are grouped in five classes: informational responses, successful responses,
        /// redirects, client errors, and servers errors.
        /// See <a href="https://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html">https://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html</a>
        /// </summary>
        public int? HttpStatusCode { get; set; }

        /// <summary>
        /// The end-point that was used when the error occurred
        /// </summary>
        public string? Path { get; set; } = default!;

        /// <summary>
        /// The timestamp when the usage occurred
        /// </summary>
        public DateTime TimeStamp { get; set; }

        public ApiException(string message, int httpStatusCode, string path) : base(message)
        {
            Path = path;
            HttpStatusCode = httpStatusCode;
            TimeStamp = DateTime.Now;
        }

        public ApiException(string message, int httpStatusCode, string path, System.Exception inner) : base(message, inner)
        {
            Path = path;
            HttpStatusCode = httpStatusCode;
            TimeStamp = DateTime.Now;
        }

        public ApiException(string message, int httpStatusCode, string path, string errorCode) : base(message)
        {
            Path = path;
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
            TimeStamp = DateTime.Now;
        }

        public ApiException(string message, int httpStatusCode, string path, string errorCode, System.Exception inner) : base(message, inner)
        {
            Path = path;
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
            TimeStamp = DateTime.Now;
        }

        public ApiException(string message, int httpStatusCode, string path, string errorCode, IEnumerable<object> details) : base(message)
        {
            Path = path;
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
            Details = details.ToList();
            TimeStamp = DateTime.Now;
        }

        public ApiException(string message, int httpStatusCode, string path, string errorCode, IEnumerable<object> details, System.Exception inner) : base(message, inner)
        {
            Path = path;
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
            Details = details.ToList();
            TimeStamp = DateTime.Now;
        }

        public ApiException(string message) : base(message)
        {
            TimeStamp = DateTime.Now;
        }

        public ApiException(string message, System.Exception inner) : base(message, inner)
        {
            TimeStamp = DateTime.Now;
        }
    }
}
