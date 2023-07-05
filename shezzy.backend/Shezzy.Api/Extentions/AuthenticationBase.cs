using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Shezzy.Api.Extentions
{
	public class AuthenticationBase
	{
		public async Task HandleOnRemoteFailure(RemoteFailureContext context)
		{
			context.Response.StatusCode = 500;
			context.Response.ContentType = "text/html";

			await context.Response.WriteAsync("<html><body>");
			await context.Response.WriteAsync($"A remote failure has occurred: <br>" +
				context?.Failure?.Message.Split(Environment.NewLine)
				.Select(s => $"{HtmlEncoder.Default.Encode(s)}<br>")
				.Aggregate((s1, s2) => $"{s1} {s2}"));

			if (context?.Properties != null)
			{
				await context.Response.WriteAsync("Properties:<br>");

				//context.Properties.Items.ToList().ForEach(async pair =>
				//{
				//	await context.Response.WriteAsync($"-{HtmlEncoder.Default.Encode(pair.Key)}={HtmlEncoder.Default.Encode(pair.Value ?? "")}<br>");
				//});
				foreach (var pair in context.Properties.Items)
				{
					await context.Response.WriteAsync($"-{HtmlEncoder.Default.Encode(pair.Key)}={HtmlEncoder.Default.Encode(pair.Value ?? "")}<br>");
				}
			}

			if (context != null)
			{
				await context.Response.WriteAsync("<a href=\"/\">Home</a>");
				await context.Response.WriteAsync("</body></html>");
			}

			// context.Response.Redirect("/error?FailureMessage=" + UrlEncoder.Default.Encode(context.Failure.Message));

			context?.HandleResponse();
		}
	}

	public class IssuerFixupAction : ClaimAction
	{
		public IssuerFixupAction() : base(ClaimTypes.NameIdentifier, string.Empty) { }

		public override void Run(JsonElement userData, ClaimsIdentity? identity, string issuer)
		{
			identity?.Claims.ToList().ForEach(claim =>
			{
				identity?.RemoveClaim(claim);
				identity?.AddClaim(new Claim(claim.Type.Split("/").Last(), claim.Value, claim.ValueType.Split("/").Last(), issuer, claim.OriginalIssuer, claim.Subject));
			});
		}
	}

	public class AuthenticationActions : AuthenticationAction
	{
		public AuthenticationActions(
			string clientId,
			string clientSecret,
			string authority,
			string issuer) : base(
				clientId, 
				clientSecret, 
				authority, 
				issuer) { }

		public override void Run(
			OAuthOptions _)
		{
			_.ClientId = ClientId;
			_.ClientSecret = ClientSecret;
			_.SaveTokens = true;
			_.Events = new OAuthEvents()
			{
				OnRemoteFailure = new AuthenticationBase().HandleOnRemoteFailure,
				OnTicketReceived = (context) =>
				{
					if (context != null && context.Principal != null)
					{
						if (context.Principal.Identity is ClaimsIdentity identity)
						{
							string? authenticationType = identity.AuthenticationType;
							if (!string.IsNullOrEmpty(authenticationType))
							{
								new IssuerFixupAction().Run(new JsonElement(), identity, authenticationType.ToString());
							}
						}
					}

					return Task.FromResult(0);
				}
			};

			if (_ is GoogleOptions googleAction)
			{
				googleAction.AccessType = "offline";
			}

			if (_ is MicrosoftAccountOptions microsoftAccountAction)
			{
				microsoftAccountAction.Scope.Add("offline_access");
			}
		}

		public override void Run(
			OpenIdConnectOptions _)
		{
			_.ClientId = ClientId;
			_.ClientSecret = ClientSecret;
			_.Authority = Authority;
			_.SaveTokens = true;
			_.Scope.Add("openid");
			_.Scope.Add("profile");
			_.Scope.Add("email");
			_.Scope.Add("offline_access");
			_.GetClaimsFromUserInfoEndpoint = true;
			_.Events = new OpenIdConnectEvents()
			{
				OnRemoteFailure = new AuthenticationBase().HandleOnRemoteFailure,
				OnTicketReceived = (context) =>
				{
					if (context != null && context.Principal != null)
					{
						ClaimsIdentity? identity = context.Principal.Identity as ClaimsIdentity;
						new IssuerFixupAction().Run(new JsonElement(), identity, Issuer);
					}

					return Task.FromResult(0);
				}
			};
			_.ClaimsIssuer = Issuer;
			_.UseTokenLifetime = true;
		}
	}
}
