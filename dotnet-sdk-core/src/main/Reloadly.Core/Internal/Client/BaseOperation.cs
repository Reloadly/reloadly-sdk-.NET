using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Net;
using System;

namespace Reloadly.Core.Internal.Client
{
    public abstract class BaseOperation
    {
        public BaseOperation(
            IReloadlyHttpClient httpClient,
            Uri baseUri,
            ReloadlyEnvironment environment)
        {
            HttpClient = httpClient;
            BaseUri = baseUri;
            Environment = environment;
        }

        protected IReloadlyHttpClient HttpClient { get; }
        protected Uri BaseUri { get; set; }
        public ReloadlyEnvironment Environment { get; }
    }
}
