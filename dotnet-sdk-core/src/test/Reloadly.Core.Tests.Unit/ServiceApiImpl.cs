using Reloadly.Core.Internal.Net;

namespace Reloadly.Core.Tests.Unit
{
    internal class ServiceApiImpl : ServiceApi
    {
        public ServiceApiImpl(string accessToken) 
            : base(accessToken)
        {
        }

        public ServiceApiImpl(string clientId, string clientSecret) 
            : base(clientId, clientSecret)
        {
        }
    }
}
