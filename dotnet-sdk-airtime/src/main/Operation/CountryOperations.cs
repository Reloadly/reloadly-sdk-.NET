using Reloadly.Airtime.Dto.Response;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public class CountryOperations : BaseAirtimeOperation, ICountryOperations
    {
        private const string Endpoint = "countries";

        public CountryOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, clientId, clientSecret, environment) { }

        public CountryOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, accessToken, environment) { }

        public async Task<IList<Country>> ListAsync()
        {
            var uri = new Uri(BaseUri, Endpoint);
            var req = await CreateGetRequestAsync<IList<Country>>(uri);
            return await HttpClient.SendAsync(req);
        }

        public async Task<Country> GetByCodeAsync(string countryCode)
        {
            var uri = new Uri(BaseUri, $"{Endpoint}/{countryCode}");
            var req = await CreateGetRequestAsync<Country>(uri);
            return await HttpClient.SendAsync(req);
        }
    }
}
