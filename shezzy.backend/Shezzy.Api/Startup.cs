using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Shezzy.Api.Extentions;

namespace Shezzy.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		private readonly string DefaultCorsPolicyName = "shezzy.backend";
		private readonly string ConfigSectionCorsOriginsName = "CorsOrigins";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(_DefaultCorsPolicy);

			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				.AddCookie()
				.RegisterOAuthAuthenticationBuilder(Configuration);
				//.RegisterMicrosoftAuthenticationBuilder(Configuration)
				//.RegisterOpenIdConnectAuthenticationBuilder(Configuration, "Auth0");

			services
				.AddSwaggerGen()
				.AddApiVersioning()
				.AddControllersWithViews(_ =>
				{
					_.EnableEndpointRouting = false;
				});

			services.AddMvc();
		}

		public void Configure(WebApplication app, IWebHostEnvironment env)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app
				.UseHttpsRedirection()
				.UseStaticFiles()
				.UseCookiePolicy()
				.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			app.Run();
		}

		private void _DefaultCorsPolicy(CorsOptions options)
		{
			var origins = Configuration.GetSection(ConfigSectionCorsOriginsName)
				.Get<string[]>();

			if (origins != null)
			{
				options.AddPolicy(DefaultCorsPolicyName, p => p
				.SetIsOriginAllowedToAllowWildcardSubdomains()
				.WithOrigins(origins)
				.WithHeaders("Authorization")
				.AllowAnyMethod()
				.AllowAnyHeader());
			}
		}
	}
}
