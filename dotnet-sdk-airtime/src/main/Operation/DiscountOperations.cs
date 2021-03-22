using Reloadly.Airtime.Dto.Response;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Filter;
using Reloadly.Core.Internal.Net;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public class DiscountOperations : BaseAirtimeOperation, IDiscountOperations
    {
        private const string Endpoint = "operators";
        private static readonly string CommissionsPath = $"commissions";

        public DiscountOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, clientId, clientSecret, environment) { }

        public DiscountOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, accessToken, environment) { }

        public async Task<Page<Discount>> ListAsync()
        {
            var uri = new Uri(BaseUri, $"{Endpoint}/{CommissionsPath}");
            var req = await CreateGetRequestAsync<Page<Discount>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<Page<Discount>> ListAsync(QueryFilter filter)
        {
            var uri = BuildUri(filter, Endpoint, CommissionsPath);
            var req = await CreateGetRequestAsync<Page<Discount>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<Discount> GetByOperatorIdAsync(long operatorId)
        {
            Debug.Assert(operatorId > 0, "Operator id");

            var uri = new Uri(BaseUri, $"{Endpoint}/{operatorId}/{CommissionsPath}");
            var req = await CreateGetRequestAsync<Discount>(uri);
            return await HttpClient.SendAsync(req);
        }
    }
}
