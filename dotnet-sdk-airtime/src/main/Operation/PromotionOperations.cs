using Reloadly.Airtime.Dto.Response;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Filter;
using Reloadly.Core.Internal.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public class PromotionOperations : BaseAirtimeOperation, IPromotionOperations
    {
        private const string Endpoint = "promotions";
        private const string PATH_SEGMENT_COUNTRIES = "countries";
        private const string PATH_SEGMENT_OPERATORS = "operators";

        public PromotionOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, clientId, clientSecret, environment) { }

        public PromotionOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, accessToken, environment) { }

        public async Task<Page<Promotion>> ListAsync(QueryFilter? filter = null)
        {
            var uri = BuildUri(filter, Endpoint);
            var req = await CreateGetRequestAsync<Page<Promotion>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<Promotion> GetByIdAsync(long promotionId)
        {
            Debug.Assert(promotionId > 0);
            var uri = BuildUri(Endpoint, promotionId.ToString());
            var req = await CreateGetRequestAsync<Promotion>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<IList<Promotion>> GetByCountryCodeAsync(string countryCode)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(countryCode), nameof(countryCode));
            var uri = BuildUri(Endpoint, PATH_SEGMENT_COUNTRIES, countryCode);
            var req = await CreateGetRequestAsync<IList<Promotion>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<IList<Promotion>> GetByOperatorIdAsync(long operatorId)
        {
            Debug.Assert(operatorId > 0, nameof(operatorId));
            var uri = BuildUri(Endpoint, PATH_SEGMENT_OPERATORS, operatorId.ToString());
            var req = await CreateGetRequestAsync<IList<Promotion>>(uri);
            return await HttpClient.SendAsync(req);
        }
    }
}
