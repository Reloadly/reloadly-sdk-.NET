using Newtonsoft.Json;

namespace Reloadly.Authentication.Dto.Response
{
    /// <summary>
    /// Class that contains the Tokens obtained after a call to the <see cref="Reloadly.Authentication.Client.AuthenticationAPI"/>.
    /// </summary>
    public class OAuthTokenResponse
    {
        /// <summary>
        /// Gets or sets the Reloadly access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        [JsonProperty("token_type")]
        public string? TokenType { get; set; }

        /// <summary>
        /// Gets or sets the expiration time ('exp' claim) of the recently issued token.
        /// </summary>
        [JsonProperty("expires_in")]
        public long? ExpiresIn { get; set; }
    }
}
