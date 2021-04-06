using Reloadly.Airtime.Dto.Response;
using Reloadly.Airtime.Filter;
using Reloadly.Core.Dto.Response;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Net;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public class TransactionHistoryOperations : BaseAirtimeOperation, ITransactionHistoryOperations
    {
        private const string Endpoint = "topups/reports/transactions";

        public TransactionHistoryOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, clientId, clientSecret, environment) { }

        public TransactionHistoryOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, accessToken, environment) { }

        public async Task<Page<TopupTransaction>> ListAsync()
        {
            var uri = BuildUri(Endpoint);
            var req = await CreateGetRequestAsync<Page<TopupTransaction>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<Page<TopupTransaction>> ListAsync(TransactionHistoryFilter filter)
        {
            ValidateStartAndEndDate(filter);
            var uri = BuildUri(filter, Endpoint);
            var req = await CreateGetRequestAsync<Page<TopupTransaction>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<TopupTransaction> GetByIdAsync(long transactionId)
        {
            Debug.Assert(transactionId > 0);
            var uri = BuildUri(Endpoint, transactionId.ToString());
            var req = await CreateGetRequestAsync<TopupTransaction>(uri);
            return await HttpClient.SendAsync(req);
        }

        private void ValidateStartAndEndDate(TransactionHistoryFilter filter)
        {
            var start = filter.Parameters.TryGetValue(TransactionHistoryFilter.START_DATE, out var _)
                ? DateTime.ParseExact(TransactionHistoryFilter.DateFormat, "", CultureInfo.InvariantCulture)
                : (DateTime?)null;

            var end = filter.Parameters.TryGetValue(TransactionHistoryFilter.END_DATE, out var _)
                ? DateTime.ParseExact(TransactionHistoryFilter.DateFormat, "", CultureInfo.InvariantCulture)
                : (DateTime?)null;

            if (start.HasValue ^ end.HasValue)
            {
                var msg = "If start date is set, end date must be set as well and vice-versa.";
                throw new ArgumentException(msg);
            }

            if (start.HasValue && start > end)
            {
                var msg = "The start date must NOT be greater than the end date.";
                throw new ArgumentException(msg);
            }
        }
    }
}
