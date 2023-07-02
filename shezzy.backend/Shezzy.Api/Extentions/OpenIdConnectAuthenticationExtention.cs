using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using System.Text.Json;

namespace Shezzy.Api.Extentions
{
	public static class OpenIdConnectAuthenticationExtention
	{
		public static AuthenticationBuilder RegisterOpenIdConnectAuthenticationBuilder(this AuthenticationBuilder builder,
			IConfiguration configuration, string issuer = "Auth0")
		{
			return builder
				.AddOpenIdConnect(options =>
				{
					options.ClientId = configuration[$"Authentication:{issuer}:ClientId"];
					options.ClientSecret = configuration[$"Authentication:{issuer}:ClientSecret"];
					options.Authority = configuration[$"Authentication:{issuer}:Domain"];
					options.Scope.Add("openid");
					options.Scope.Add("profile");
					options.Scope.Add("email");
					options.Scope.Add("offline_access");

					options.SaveTokens = true;
					options.GetClaimsFromUserInfoEndpoint = true;
					options.Events = new OpenIdConnectEvents()
					{
						OnRemoteFailure = new AuthenticationBase().HandleOnRemoteFailure,
						OnTicketReceived = (context) =>
						{
							if (context != null && context.Principal != null)
							{
								ClaimsIdentity? identity = context.Principal.Identity as ClaimsIdentity;
								new IssuerFixupAction().Run(new JsonElement(), identity, issuer);
							}

							return Task.FromResult(0);
						}
					};
					options.ClaimsIssuer = issuer;
				});
		}
	}
}
