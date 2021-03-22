using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Net;
using System;

namespace Reloadly.Airtime.Operation
{
    public class ReportOperations : BaseAirtimeOperation, IReportOperations
    {
        public ReportOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, clientId, clientSecret, environment) { }

        public ReportOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, accessToken, environment) { }

        public TransactionHistoryOperations TransactionsHistory =>
            string.IsNullOrEmpty(AccessToken)
                ? new TransactionHistoryOperations(HttpClient, BaseUri, ClientId!, ClientSecret!, Environment)
                : new TransactionHistoryOperations(HttpClient, BaseUri, AccessToken, Environment);

    }
}
