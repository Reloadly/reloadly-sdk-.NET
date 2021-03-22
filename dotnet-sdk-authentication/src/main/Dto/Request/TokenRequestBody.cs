using Newtonsoft.Json;

namespace Reloadly.Authentication.Dto.Request
{
    public class TokenRequestBody
    {
        public TokenRequestBody()
        {
            ClientId = default!;
            ClientSecret = default!;
            GrantType = default!;
            Audience = default!;
        }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }
    }
}
