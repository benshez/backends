﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shezzy.Authentication.Actions
{
	public class AuthenticationActionBase : AuthenticationAction
	{
		public AuthenticationActionBase(
			string clientId,
			string clientSecret,
			string authority,
			string issuer) : base(
				clientId,
				clientSecret,
				authority,
				issuer)
		{ }

		public override void Run(OAuthOptions _)
		{
			_.ClientId = ClientId;
			_.ClientSecret = ClientSecret;
			_.ClaimActions.MapJsonKey("image", "picture");
			_.SaveTokens = true;
			_.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
			_.Events = new OAuthEvents()
			{
				OnRemoteFailure = new AuthenticationBase().HandleOnRemoteFailure,
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

		public override void Run(OpenIdConnectOptions _)
		{
			_.ClientId = ClientId;
			_.ClientSecret = ClientSecret;
			_.Authority = Authority;
			_.ClaimActions.MapJsonKey("image", "picture");
			_.SaveTokens = true;
			_.Scope.Add("openid");
			_.Scope.Add("profile");
			_.Scope.Add("email");
			_.Scope.Add("offline_access");
			_.GetClaimsFromUserInfoEndpoint = true;
			_.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
			_.Events = new OpenIdConnectEvents()
			{
				OnRemoteFailure = new AuthenticationBase().HandleOnRemoteFailure,
			};

			_.ClaimsIssuer = Issuer;
		}
    }
}
