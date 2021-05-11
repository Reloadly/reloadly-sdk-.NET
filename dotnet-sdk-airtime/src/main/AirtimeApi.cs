using Microsoft.Net.Http.Headers;
using Reloadly.Airtime.Operation;
using Reloadly.Core.Constant;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal;
using Reloadly.Core.Internal.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ReloadlyEnvironment = Reloadly.Core.Enums.ReloadlyEnvironment;

namespace Reloadly.Airtime
{
    public class AirtimeApi : ServiceApi, IAirtimeApi
    {
        private readonly Uri _baseUrl;
        private readonly IReloadlyHttpClient _httpClient;
        private readonly ReloadlyEnvironment _environment;

        public AirtimeApi(
            IHttpClientFactory httpClientFactory,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment,
            bool disableTelemetry = false)
            : base(clientId, clientSecret)
        {
            _httpClient = new ReloadlyHttpClient(httpClientFactory, ReloadlyApiVersion.AirtimeV1, disableTelemetry);
            _environment = environment;
            _baseUrl = CreateBaseUrl();
        }

        public AirtimeApi(
            IHttpClientFactory httpClientFactory,
            string accessToken,
            ReloadlyEnvironment environment,
            bool disableTelemetry = false)
            : base(accessToken)
        {
            _httpClient = new ReloadlyHttpClient(httpClientFactory, ReloadlyApiVersion.AirtimeV1, disableTelemetry);
            _environment = environment;
            _baseUrl = CreateBaseUrl();
        }

        public IOperatorOperations Operators =>
                string.IsNullOrEmpty(AccessToken)
                    ? new OperatorOperations(_httpClient, _baseUrl, ClientId!, ClientSecret!, _environment)
                    : new OperatorOperations(_httpClient, _baseUrl, AccessToken, _environment);

        public ICountryOperations Countries =>
                string.IsNullOrEmpty(AccessToken)
                    ? new CountryOperations(_httpClient, _baseUrl, ClientId!, ClientSecret!, _environment)
                    : new CountryOperations(_httpClient, _baseUrl, AccessToken, _environment);

        public IAccountOperations Accounts =>
                string.IsNullOrEmpty(AccessToken)
                    ? new AccountOperations(_httpClient, _baseUrl, ClientId!, ClientSecret!, _environment)
                    : new AccountOperations(_httpClient, _baseUrl, AccessToken, _environment);

        public IDiscountOperations Discounts =>
                string.IsNullOrEmpty(AccessToken)
                    ? new DiscountOperations(_httpClient, _baseUrl, ClientId!, ClientSecret!, _environment)
                    : new DiscountOperations(_httpClient, _baseUrl, AccessToken, _environment);

        public IPromotionOperations Promotions =>
                string.IsNullOrEmpty(AccessToken)
                    ? new PromotionOperations(_httpClient, _baseUrl, ClientId!, ClientSecret!, _environment)
                    : new PromotionOperations(_httpClient, _baseUrl, AccessToken, _environment);

        public ITopupOperations Topups =>
                string.IsNullOrEmpty(AccessToken)
                    ? new TopupOperations(_httpClient, _baseUrl, ClientId!, ClientSecret!, _environment)
                    : new TopupOperations(_httpClient, _baseUrl, AccessToken, _environment);

        public IReportOperations Reports =>
                string.IsNullOrEmpty(AccessToken)
                    ? new ReportOperations(_httpClient, _baseUrl, ClientId!, ClientSecret!, _environment)
                    : new ReportOperations(_httpClient, _baseUrl, AccessToken, _environment);

        public Task<TResponse> RefreshTokenForRequest<TResponse>(ReloadlyRequest<TResponse> request, string accessToken)
            where TResponse : class
        {
            AccessToken = accessToken;
            request.SetHeader(HeaderNames.Authorization, $"Bearer {accessToken}");
            return _httpClient.SendAsync(request);
        }

        private Uri CreateBaseUrl()
        {
            var serviceUrl = ServiceUrls.ByEnvironment(_environment);
            var uri = new Uri(serviceUrl);
            return uri;
        }
    }
}
