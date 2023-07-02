using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using System.Text.Json;

namespace Shezzy.Api.Extentions
{
	public class AuthenticationsActions : IAuthenticationsActions
	{
		public string? ClientId { get; set; }
		public string? ClientSecret { get; set; }

		public void GetAction<T>(T _)
		{
			if (_ is OAuthOptions action)
			{
				action.ClientId = ClientId;
				action.ClientSecret = ClientSecret;
				action.SaveTokens = true;
				action.Events = new OAuthEvents()
				{
					OnRemoteFailure = new AuthenticationBase().HandleOnRemoteFailure,
					OnTicketReceived = (context) =>
					{
						if (context != null && context.Principal != null)
						{
							ClaimsIdentity? identity = context.Principal.Identity as ClaimsIdentity;
							new IssuerFixupAction().Run(new JsonElement(), identity, identity?.AuthenticationType?.ToString());
						}

						return Task.FromResult(0);
					}
				};
			}

			if (_ is GoogleOptions googleAction)
			{
				googleAction.AccessType = "offline";
			}

			if (_ is MicrosoftAccountOptions microsoftAccountAction)
			{
				microsoftAccountAction.Scope.Add("offline_access");
			}
		}
	}
}
