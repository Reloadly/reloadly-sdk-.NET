using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Client;
using Reloadly.Core.Internal.Net;
using System;

namespace Reloadly.Core.Tests.Unit
{
    public class BaseOperationImpl : BaseOperation
    {
        public BaseOperationImpl(IReloadlyHttpClient httpClient, Uri baseUri, ReloadlyEnvironment environment) 
            : base(httpClient, baseUri, environment)
        {
        }

        public new IReloadlyHttpClient HttpClient => base.HttpClient;
        public new Uri BaseUri => base.BaseUri;
    }
}
