namespace Shezzy.Shared.Abstractions
{
  public interface IFirebaseCredentials
  {
    string Type { get; set; }
    string ProjectId { get; set; }
    string PrivateKeyId { get; set; }
    string PrivateKey { get; set; }
    string ClientEmail { get; set; }
    string ClientId { get; set; }
    string AuthUri { get; set; }
    string TokenUri { get; set; }
    string AuthProviderX509CertUrl { get; set; }
    string ClientX509CertUrl { get; set; }
    string Serialize();
  }
}