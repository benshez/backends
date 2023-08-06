using System.Text.Json.Serialization;

namespace Shezzy.Shared.OAuth
{
    public class AuthorityHostAuthTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
    }
}
