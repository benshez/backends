using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Shezzy.Api.Extentions
{
	public abstract class AuthenticationAction
	{
		public string ClientId { get; }
		public string ClientSecret { get; }
		public string Authority { get; }
		public string Issuer { get; }
		public AuthenticationAction(
			string clientId,
			string clientSecret,
			string authority,
			string issuer)
		{
			ClientId = clientId;
			ClientSecret = clientSecret;
			Authority = authority;
			Issuer = issuer;
		}
		public abstract void Run(OAuthOptions _);
		public abstract void Run(OpenIdConnectOptions _);
	}
}
