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

			providers.GetChildren().ToList().ForEach(provider =>
			{
				var section = configuration.GetSection(provider.Path);
				var useProvider = section.GetValue<bool>("UseProvider");
				var clientId = section.GetValue<string>("ClientId") ?? "";
				var clientSecret = section.GetValue<string>("ClientSecret") ?? "";
				var authority = section.GetValue<string>("Authority") ?? "";
				var issuer = section.Key;

				if (section.Key == "OpenId")
				{
					section.GetChildren().ToList().ForEach(openIdProvider =>
					{
						useProvider = openIdProvider.GetValue<bool>("UseProvider");

						if (useProvider)
						{
							clientId = openIdProvider.GetValue<string>("ClientId") ?? "";
							clientSecret = openIdProvider.GetValue<string>("ClientSecret") ?? "";
							authority = openIdProvider.GetValue<string>("Authority") ?? "";
							issuer = openIdProvider.GetValue<string>("Issuer") ?? "";

							builder.AddOpenIdConnect(_ =>
							{
								new AuthenticationActionBase(clientId, clientSecret, authority, issuer).Run(_);
							});
						}
					});
				}
				else
				{
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
			});
			return builder;
		}
	}
}
