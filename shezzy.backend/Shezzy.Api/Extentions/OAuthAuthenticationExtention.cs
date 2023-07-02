using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

namespace Shezzy.Api.Extentions
{
	public static class OAuthAuthenticationExtention
	{
		public static AuthenticationBuilder RegisterOAuthAuthenticationBuilder(
			this AuthenticationBuilder builder,
			IConfiguration configuration)
		{
			var providers = configuration.GetSection("AuthenticationProviders");

			foreach (var provider in providers.GetChildren())
			{
				var providerSection = configuration.GetSection(provider.Path);
				var useProvider = providerSection.GetValue<bool>("UseProvider");
				var clientId = providerSection.GetValue<string>("ClientId");
				var clientSecret = providerSection.GetValue<string>("ClientSecret");

				if (useProvider)
				{
					AuthenticationsActions actions = new()
					{
						ClientId = clientId,
						ClientSecret = clientSecret
					};

					if (providerSection.Key == "Google")
					{
						builder.AddGoogle(GoogleDefaults.AuthenticationScheme, actions.GetAction);
					}
					if (providerSection.Key == "Microsoft")
					{
						builder.AddMicrosoftAccount(actions.GetAction);
					}
				}
			}

			return builder;
		}
	}
}
