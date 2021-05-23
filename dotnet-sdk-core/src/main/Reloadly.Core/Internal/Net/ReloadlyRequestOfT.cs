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

        public new ReloadlyRequest<TResponse> SetHeader(string name, string value)
        {
            base.SetHeader(name, value);
            return this;
        }

        public new ReloadlyRequest<TResponse> SetQueryParameter(string name, string value)
        {
            base.SetQueryParameter(name, value);
            return this;
        }

        public new ReloadlyRequest<TResponse> SetBody(object value)
        {
            base.SetBody(value);
            return this;
        }
    }
}
