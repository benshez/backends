using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Shezzy.Shared.Abstractions;

namespace Shezzy.Shared.OAuth
{
    public class AuthTokenManager : IAuthTokenManager
    {
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly IApplicationSettings _appSettings;
        private readonly IMemoryCacheProvider _cache;
        private readonly HttpClient _httpClient;

        public AuthTokenManager(
            IApplicationSettings appSettings,
            IHttpClientFactory httpClientFactory,
            IMemoryCacheProvider cache)
        {
            _appSettings = appSettings;
            _cache = cache;

            _httpClient = httpClientFactory.CreateClient("");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetAuthToken(string tokenCachekey, string audience)
        {
            await SemaphoreSlim.WaitAsync();

            try
            {
                return await _cache.GetOrCacheFromResult(tokenCachekey, async () =>
                {
                    return await _GenerateAuthAccessToken(audience);
                }, DateTimeOffset.Now.AddHours(12));
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        private async Task<string> _GenerateAuthAccessToken(string audience)
        {
            var responseMessage = await _httpClient.SendAsync(_GenerateAuthTokenRequest(audience));
            var responseContentAsString = await responseMessage.Content.ReadAsStringAsync();

            responseMessage.EnsureSuccessStatusCode();

            var tokenResponse = JsonConvert.DeserializeObject<AuthorityHostAuthTokenResponse>(responseContentAsString);

            if (tokenResponse != null) return string.IsNullOrEmpty(tokenResponse.AccessToken) ? string.Empty : tokenResponse.AccessToken;

            return string.Empty;

        }

        private HttpRequestMessage _GenerateAuthTokenRequest(string audience)
        {
            var content = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"client_id", string.IsNullOrEmpty(_appSettings.Auth0ClientId) ? string.Empty : _appSettings.Auth0ClientId},
                {"client_secret", string.IsNullOrEmpty(_appSettings.Auth0ClientSecret) ? string.Empty : _appSettings.Auth0ClientSecret},
                {"audience", audience},
                {"grant_type", "client_credentials"}
            });

            var request = new HttpRequestMessage(HttpMethod.Post, _appSettings.Auth0AuthorityHost)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };

            return request;
        }
    }
}