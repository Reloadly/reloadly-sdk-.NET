using Newtonsoft.Json;
using Reloadly.Core.Exception;
using Reloadly.Core.Exception.Oauth;
using Reloadly.Core.Internal.Utility;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reloadly.Core.Internal.Net
{
    public class ReloadlyHttpClient : IReloadlyHttpClient
    {
        private const int STATUS_CODE_TOO_MANY_REQUEST = 429;
        private const string TelemetryHeaderName = "Reloadly-Client";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string? _apiVersion;
        private readonly bool _disableTelemetry;

        public ReloadlyHttpClient(
            IHttpClientFactory httpClientFactory, string? apiVersion,
            bool disableTelemetry = false)
        {
            _httpClientFactory = httpClientFactory;
            _apiVersion = apiVersion;
            _disableTelemetry = disableTelemetry;
        }

        public ReloadlyHttpClient Clone(bool skipApiVersion = false)
        {
            var apiVersion = !skipApiVersion ? _apiVersion : null;
            return new ReloadlyHttpClient(_httpClientFactory, apiVersion, _disableTelemetry);
        }

        public async Task<TResponse> SendAsync<TResponse>(ReloadlyRequest<TResponse> request)
            where TResponse : class
        {
            var reqMessage = request.CreateHttpRequestMessage();

            if (!_disableTelemetry)
            {
                var telemetry = TelemetryUtility.Create(_apiVersion).HttpHeaderValue();
                reqMessage.Headers.TryAddWithoutValidation(TelemetryHeaderName, telemetry);
            }

            var httpClient = _httpClientFactory.CreateClient();
            var resMessage = await httpClient.SendAsync(reqMessage);
            return await ParseResponse<TResponse>(request, resMessage);
        }
        private async Task<TResponse> ParseResponse<TResponse>(ReloadlyRequest<TResponse> request, HttpResponseMessage responseMessage)
            where TResponse : class
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw await CreateResponseException(request, responseMessage);
            }

            var payload = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                TResponse body = JsonConvert.DeserializeObject<TResponse>(payload);
                return body;
            }
            catch (System.Exception ex)
            {
                var path = responseMessage.RequestMessage.RequestUri.AbsolutePath;
                throw new ApiException("Failed to parse json body.", (int)responseMessage.StatusCode, path, ex);
            }
        }

        private async Task<ReloadlyException> CreateResponseException<TResponse>(
            ReloadlyRequest<TResponse> request, HttpResponseMessage responseMessage)
            where TResponse : class
        {
            if ((int)responseMessage.StatusCode == STATUS_CODE_TOO_MANY_REQUEST)
            {
                return await CreateRateLimitException(request, responseMessage);
            }

            var body = responseMessage.Content != null
                ? await responseMessage.Content.ReadAsStringAsync()
                : null;

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ApiException(
                    "No response from server, please try again or contact support",
                    (int)responseMessage.StatusCode, responseMessage.RequestMessage.RequestUri.AbsolutePath);
            }

            if (responseMessage.RequestMessage.RequestUri.AbsolutePath.TrimStart('/') == "oauth/token")
            {
                throw new ReloadlyOAuthException<TResponse>(
                    request, body, (int)responseMessage.StatusCode, responseMessage.RequestMessage.RequestUri.AbsolutePath);
            }

            throw new ApiException(body, (int)responseMessage.StatusCode, responseMessage.RequestMessage.RequestUri.AbsolutePath);
        }

        private async Task<ReloadlyException> CreateRateLimitException<TResponse>(
            ReloadlyRequest<TResponse> request, HttpResponseMessage response)
            where TResponse : class
        {
            var exception = await CreateResponseException(request, response);

            if (!(exception is RateLimitException rateLimitException))
            {
                return exception;
            }

            string limitValue = response.Headers.GetValues("X-RateLimit-Limit").FirstOrDefault();
            string remainingValue = response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault();
            string resetValue = response.Headers.GetValues("X-RateLimit-Reset").FirstOrDefault();

            var limit = long.TryParse(limitValue, out var parsedLimit) ? parsedLimit : (long?)null;
            var remaining = long.TryParse(remainingValue, out var parsedRemaining) ? parsedRemaining : (long?)null;
            var reset = double.TryParse(resetValue, out var parsedReset) ? parsedReset : (double?)null;

            rateLimitException.Limit = limit;
            rateLimitException.Remaining = remaining;
            rateLimitException.ExpectedResetTimestamp = reset != null ? UnixTimeStampToDateTime(reset.Value) : (DateTime?)null;

            return rateLimitException;
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
