using Reloadly.Core.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reloadly.Core.Exception.Oauth
{
    [Serializable]
    public class ReloadlyOAuthException<TResponse> : ReloadlyOAuthException where TResponse : class
    {
        /// <summary>
        /// The request that the exception was thrown for.
        /// </summary>
        public new ReloadlyRequest<TResponse> Request { get; }

        public ReloadlyOAuthException()
        {
            Request = default!;
        }

        public ReloadlyOAuthException(string message) : base(message)
        {
            Request = default!;
        }

        public ReloadlyOAuthException(string message, System.Exception innerException) : base(message, innerException)
        {
            Request = default!;
        }

        protected ReloadlyOAuthException(SerializationInfo info, StreamingContext context)
            : base(info, context) { Request = default!; }

        public ReloadlyOAuthException(ReloadlyRequest<TResponse> request)
            : base(request) { Request = request; }

        public ReloadlyOAuthException(ReloadlyRequest<TResponse> request, string message)
            : base(request, message) { Request = request; }

        public ReloadlyOAuthException(ReloadlyRequest<TResponse> request, string message, System.Exception innerException)
            : base(request, message, innerException) { Request = request; }

        public ReloadlyOAuthException(
            ReloadlyRequest<TResponse> request, string message, int httpStatusCode, string path)
            : base(request, message, httpStatusCode, path) { Request = request; }

        public ReloadlyOAuthException(
            ReloadlyRequest<TResponse> request, string message, int httpStatusCode, string path, string errorCode,
            IEnumerable<object> details, System.Exception inner)
            : base(request, message, httpStatusCode, path, errorCode, details, inner) { Request = request; }
    }
}
