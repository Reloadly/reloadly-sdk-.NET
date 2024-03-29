﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace Reloadly.Core.Internal
{
    public class ReloadlyRequest
    {
        public Uri Uri { get; set; }
        public HttpMethod Method { get; set; }

        public object? Body { get; set; }
        public Dictionary<string, string> RequestHeaders { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> QueryParameters { get; set; } = new Dictionary<string, string>();

        public ReloadlyRequest(HttpMethod method, Uri uri)
        {
            Method = method;
            Uri = uri;
        }

        public ReloadlyRequest SetBody(object value)
        {
            Body = value;
            return this;
        }

        public ReloadlyRequest SetHeader(string name, string value)
        {
            RequestHeaders[name] = value;
            return this;
        }

        public ReloadlyRequest SetQueryParameter(string name, string value)
        {
            QueryParameters[name] = value;
            return this;
        }

        public HttpRequestMessage CreateHttpRequestMessage()
        {
            var uriBuilder = new UriBuilder(Uri)
            {
                Query =
                    string.Join("=", QueryParameters.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"))
            };

            var requestMessage = new HttpRequestMessage(Method, uriBuilder.Uri);

            foreach (var header in RequestHeaders)
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (Body != null)
            {
                requestMessage.Content =
                    new StringContent(JsonConvert.SerializeObject(Body), Encoding.UTF8, MediaTypeNames.Application.Json);
            }

            return requestMessage;
        }
    }
}
