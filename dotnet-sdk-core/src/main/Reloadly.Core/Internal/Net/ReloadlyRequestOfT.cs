using System;
using System.Net.Http;

namespace Reloadly.Core.Internal
{
    public class ReloadlyRequest<TResponse> : ReloadlyRequest
        where TResponse : class
    {
        public ReloadlyRequest(HttpMethod method, Uri uri)
            : base(method, uri)
        {
            Method = method;
            Uri = uri;
        }

        public ReloadlyRequest<TResponse> AddHeader(string name, string value)
        {
            RequestHeaders[name] = value;
            return this;
        }

        public ReloadlyRequest<TResponse> AddParameter(string name, string value)
        {
            QueryParameters[name] = value;
            return this;
        }

        public new ReloadlyRequest<TResponse> SetBody(object value)
        {
            Body = value;
            return this;
        }
    }
}
