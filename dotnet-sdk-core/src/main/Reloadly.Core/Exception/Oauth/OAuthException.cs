using Reloadly.Core.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reloadly.Core.Exception.Oauth
{
    [Serializable]
    public class ReloadlyOAuthException : ApiException
    {
        /// <summary>
        /// The request that the exception was thrown for.
        /// </summary>
        public ReloadlyRequest Request { get; }

        protected ReloadlyOAuthException(SerializationInfo info, StreamingContext context)
            : base(info, context) { Request = default!; }

        public ReloadlyOAuthException(ReloadlyRequest request)
            : base() { Request = request; }

        public ReloadlyOAuthException(ReloadlyRequest request, string message)
            : base(message) { Request = request; }

        public ReloadlyOAuthException(ReloadlyRequest request, string message, System.Exception innerException)
            : base(message, innerException) { Request = request; }

        public ReloadlyOAuthException(
            ReloadlyRequest request, string message, int httpStatusCode, string path)
            : base(message, httpStatusCode, path) { Request = request; }

        public ReloadlyOAuthException(
            ReloadlyRequest request, string message, int httpStatusCode, string path, string errorCode,
            IEnumerable<object> details, System.Exception inner)
            : base(message, httpStatusCode, path, errorCode, details, inner) { Request = request; }
    }
}
