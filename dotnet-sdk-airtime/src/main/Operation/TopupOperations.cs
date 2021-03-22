using Reloadly.Airtime.Dto;
using Reloadly.Airtime.Dto.Request;
using Reloadly.Airtime.Dto.Response;
using Reloadly.Core.Enums;
using Reloadly.Core.Internal.Net;
using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reloadly.Airtime.Operation
{
    public class TopupOperations : BaseAirtimeOperation, ITopupOperations
    {
        private const string END_POINT = "topups";

        public TopupOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string clientId, string clientSecret,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, clientId, clientSecret, environment) { }

        public TopupOperations(
            IReloadlyHttpClient httpClient, Uri baseUri,
            string accessToken,
            ReloadlyEnvironment environment)
            : base(httpClient, baseUri, accessToken, environment) { }

        public async Task<TopupTransaction> SendAsync(TopupRequest request)
        {
            ValidateTopupRequest(request);

            var uri = new Uri(BaseUri, END_POINT);
            var req = await CreatePostRequestAsync<TopupTransaction>(uri, request);
            return await HttpClient.SendAsync(req);
        }

        private void ValidateTopupRequest(TopupRequest request)
        {
            Debug.Assert(request.Amount > 0);
            Debug.Assert(request.OperatorId > 0);

            if (request is PhoneTopupRequest phoneTopupRequest)
            {
                if (request.SenderPhone != null)
                {
                    AssertValidPhone(request.SenderPhone);
                }

                AssertValidPhone(phoneTopupRequest.RecipientPhone);
            }
            else if (request is EmailTopupRequest emailTopupRequest)
            {
                AssertValidEmail(emailTopupRequest.RecipientEmail);
            }
        }

        private void AssertValidEmail(string recipientEmail)
        {
            var _ = new MailAddress(recipientEmail).Address;
        }

        private void AssertValidPhone(Phone phone)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(phone.Number));
            Debug.Assert(!string.IsNullOrWhiteSpace(phone.CountryCode));

            var regex = new Regex("[0-9]+");
            var number = phone.Number.Replace("+", "").Replace(" ", "").Trim();

            if (number.Length <= 3 || !regex.IsMatch(number))
            {
                throw new ArgumentException("Phone number must contain only numbers and an optional leading '+' sign!");
            }
        }
    }
}
