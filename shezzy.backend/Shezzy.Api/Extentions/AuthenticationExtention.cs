using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

namespace Shezzy.Api.Extentions
{
	public static class AuthenticationExtention
	{
		public static AuthenticationBuilder RegisterAuthenticationBuilder(
			this AuthenticationBuilder builder,
			IConfiguration configuration)
		{
			var providers = configuration.GetSection("AuthenticationProviders");

			foreach (var provider in providers.GetChildren())
			{
				var section = configuration.GetSection(provider.Path);

				if (section.Key == "OpenId")
				{
					AddOpenIdProvider(builder, section);
				}
				else
				{
					AddProvider(builder, section);
				}
			}

			return builder;
		}

		private static void AddProvider(
			AuthenticationBuilder builder,
			IConfigurationSection section)
		{
			var useProvider = section.GetValue<bool>("UseProvider");
			var clientId = section.GetValue<string>("ClientId") ?? "";
			var clientSecret = section.GetValue<string>("ClientSecret") ?? "";
			var authority = section.GetValue<string>("Authority") ?? "";

			if (useProvider)
			{
				if (section.Key == "Google")
				{
					builder.AddGoogle(GoogleDefaults.AuthenticationScheme, _ =>
					{
						new AuthenticationActionBase(clientId, clientSecret, authority, section.Key).Run(_);
					});
				}
				if (section.Key == "Microsoft")
				{
					builder.AddMicrosoftAccount(_ =>
					{
						new AuthenticationActionBase(clientId, clientSecret, authority, section.Key).Run(_);
					});
				}
			}
		}
		private static void AddOpenIdProvider(
			AuthenticationBuilder builder,
			IConfigurationSection section)
		{
			foreach (var openIdProvider in section.GetChildren())
			{
				var useProvider = openIdProvider.GetValue<bool>("UseProvider");

				if (useProvider)
				{
					var clientId = openIdProvider.GetValue<string>("ClientId") ?? "";
					var clientSecret = openIdProvider.GetValue<string>("ClientSecret") ?? "";
					var authority = openIdProvider.GetValue<string>("Authority") ?? "";
					var issuer = openIdProvider.GetValue<string>("Issuer") ?? "";

					builder.AddOpenIdConnect(_ =>
					{
						new AuthenticationActionBase(clientId, clientSecret, authority, section.Key).Run(_);
					});
				}
			}
		}
	}
}
