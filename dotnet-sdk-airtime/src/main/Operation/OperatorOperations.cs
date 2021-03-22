using Reloadly.Airtime.Dto.Request;
using Reloadly.Airtime.Dto.Response;
using Reloadly.Airtime.Filter;
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
    public class OperatorOperations : BaseAirtimeOperation, IOperatorOperations
    {
        private const string Endpoint = "operators";
        private const string PATH_SEGMENT_FX_RATE = "fx-rate";
        private const string PATH_SEGMENT_COUNTRIES = "countries";
        private const string PATH_SEGMENT_AUTO_DETECT = "auto-detect";
        private const string PATH_SEGMENT_AUTO_DETECT_PHONE = "phone";

        public OperatorOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, clientId, clientSecret, environment) { }

        public OperatorOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, accessToken, environment) { }

        public async Task<Page<Operator>> ListAsync(OperatorFilter? filter = null)
        {
            var uri = BuildUri(filter, Endpoint);
            var req = await CreateGetRequestAsync<Page<Operator>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<Operator> GetByIdAsync(long operatorId, OperatorFilter? filter = null)
        {
            ValidateOperatorId(operatorId);
            var uri = BuildUri(filter, Endpoint, operatorId.ToString());
            var req = await CreateGetRequestAsync<Operator>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<Operator> AutoDetectAsync(string phone, string countryCode, OperatorFilter? filter = null)
        {
            ValidatePhoneAndCountryCode(phone, countryCode);
            var req = await CreateGetRequestAsync<Operator>(BuildAutoDetectRequestUri(phone, countryCode, filter));
            return await HttpClient.SendAsync(req);
        }

        public async Task<IList<Operator>> ListByCountryCodeAsync(string countryCode, OperatorFilter? filter = null)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(countryCode));
            var uri = BuildListByCountryCodeRequestUrl(countryCode, filter);
            var req = await CreateGetRequestAsync<IList<Operator>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<OperatorFxRate> CalculateFxRateAsync(long operatorId, decimal amount)
        {
            ValidateOperatorId(operatorId);
            Debug.Assert(amount > 0);
            var req = await CreatePostRequestAsync<OperatorFxRate>(BuildCalculateFxRateRequestUri(operatorId), new FxRateRequest(amount));
            return await HttpClient.SendAsync(req);
        }

        private Uri BuildListByCountryCodeRequestUrl(string countryCode, QueryFilter? filter = null)
        {
            return filter == null
                ? BuildUri(Endpoint, PATH_SEGMENT_COUNTRIES, countryCode)
                : BuildUri(filter, Endpoint, PATH_SEGMENT_COUNTRIES, countryCode);
        }

        private Uri BuildCalculateFxRateRequestUri(long operatorId)
        {
            return BuildUri(Endpoint, operatorId.ToString(), PATH_SEGMENT_FX_RATE);
        }

        private Uri BuildAutoDetectRequestUri(string phone, string countryCode, QueryFilter? filter = null)
        {
            phone = $"+{phone.Trim().TrimStart('+')}";

            if (filter != null)
                return BuildUri(filter, Endpoint, PATH_SEGMENT_AUTO_DETECT,
                    PATH_SEGMENT_AUTO_DETECT_PHONE, phone,
                    PATH_SEGMENT_COUNTRIES, countryCode);

            return BuildUri(Endpoint, PATH_SEGMENT_AUTO_DETECT,
                PATH_SEGMENT_AUTO_DETECT_PHONE, phone,
                PATH_SEGMENT_COUNTRIES, countryCode);
        }

        private void ValidateOperatorId(long operatorId)
        {
            Debug.Assert(operatorId > 0);
        }

        private void ValidatePhoneAndCountryCode(string phone, string countryCode)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(phone));
            Debug.Assert(!string.IsNullOrWhiteSpace(countryCode));
        }
    }
}
