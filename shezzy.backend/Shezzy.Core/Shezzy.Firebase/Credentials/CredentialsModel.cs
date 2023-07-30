using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shezzy.Firebase.Credentials
{
	public class CredentialsModel : ICredentials
	{
		[JsonPropertyName("type")]
		public string Type { get; set; } = "";
		[JsonPropertyName("project_id")]
		public string ProjectId { get; set; } = "";
		[JsonPropertyName("private_key_id")]
		public string PrivateKeyId { get; set; } = "";
		[JsonPropertyName("private_key")]
		public string PrivateKey { get; set; } = "";
		[JsonPropertyName("client_email")]
		public string ClientEmail { get; set; } = "";
		[JsonPropertyName("client_id")]
		public string ClientId { get; set; } = "";
		[JsonPropertyName("auth_uri")]
		public string AuthUri { get; set; } = "";
		[JsonPropertyName("token_uri")]
		public string TokenUri { get; set; } = "";
		[JsonPropertyName("auth_provider_x509_cert_url")]
		public string AuthProviderX509CertUrl { get; set; } = "";
		[JsonPropertyName("client_x509_cert_url")]
		public string ClientX509CertUrl { get; set; } = "";

		public CredentialsModel(IConfiguration configuration)
		{
			Type = configuration["Firebase:type"];
			ProjectId = configuration["Firebase:project_Id"];
			PrivateKeyId = configuration["Firebase:private_key_id"];
			PrivateKey = configuration["Firebase:private_key"];
			ClientEmail = configuration["Firebase:client_email"];
			ClientId = configuration["Firebase:client_id"];
			AuthUri = configuration["Firebase:auth_uri"];
			TokenUri = configuration["Firebase:token_uri"];
			AuthProviderX509CertUrl = configuration["Firebase:auth_provider_x509_cert_url"];
			ClientX509CertUrl = configuration["Firebase:client_x509_cert_url"];
		}
		public string Serialize()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}