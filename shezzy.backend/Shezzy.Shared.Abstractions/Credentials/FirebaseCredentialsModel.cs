using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace Shezzy.Shared.Abstractions.Credentials
{
    public class FirebaseCredentialsModel : IFirebaseCredentials
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("project_id")]
        public string ProjectId { get; set; }
        [JsonPropertyName("private_key_id")]
        public string PrivateKeyId { get; set; }
        [JsonPropertyName("private_key")]
        public string PrivateKey { get; set; }
        [JsonPropertyName("client_email")]
        public string ClientEmail { get; set; }
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }
        [JsonPropertyName("auth_uri")]
        public string AuthUri { get; set; }
        [JsonPropertyName("token_uri")]
        public string TokenUri { get; set; }
        [JsonPropertyName("auth_provider_x509_cert_url")]
        public string AuthProviderX509CertUrl { get; set; } = "";
        [JsonPropertyName("client_x509_cert_url")]
        public string ClientX509CertUrl { get; set; }

        public FirebaseCredentialsModel(IConfiguration configuration)
        {
            var sections = configuration.GetSection("Firebase");

            Type = sections.GetValue<string>("type") ?? string.Empty;
            ProjectId = sections.GetValue<string>("project_Id") ?? string.Empty;
            PrivateKeyId = sections.GetValue<string>("private_key_id") ?? string.Empty;
            PrivateKey = sections.GetValue<string>("private_key") ?? string.Empty;
            ClientEmail = sections.GetValue<string>("client_email") ?? string.Empty;
            ClientId = sections.GetValue<string>("client_id") ?? string.Empty;
            AuthUri = sections.GetValue<string>("auth_uri") ?? string.Empty;
            TokenUri = sections.GetValue<string>("token_uri") ?? string.Empty;
            AuthProviderX509CertUrl = sections.GetValue<string>("auth_provider_x509_cert_url") ?? string.Empty;
            ClientX509CertUrl = sections.GetValue<string>("client_x509_cert_url") ?? string.Empty;
        }
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}