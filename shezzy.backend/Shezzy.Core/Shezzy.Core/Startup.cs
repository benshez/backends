using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Shezzy.Core.Extentions;
using Shezzy.Core.User;
using System.Reflection;

namespace Shezzy.Core
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
			services
				.AddControllersWithViews();

			services
				.AddOrchardCore()
				.ConfigureServices(services =>
				{
					services
						.AddCors(DefaultCorsPolicy);

					services
						.AddAuthentication(options =>
						{
							options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
							options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
							options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
						})
						.AddCookie(option =>
						{
							option.Cookie.SameSite = SameSiteMode.Unspecified;
						})
						.RegisterAuthenticationBuilder(Configuration);

					services
						.AddSwaggerGen()
						.AddApiVersioning();

					services
						.AddSingleton<IUserService, UserService>()
						.AddAutoMapper(Assembly.GetExecutingAssembly());

					services.AddMvc();
				})
				.Configure((app, endpoints, services) =>
				{
					app.UseAuthorization();

					endpoints
						.MapAreaControllerRoute(
							name: "Default",
							areaName: "Shezzy.Core",
							pattern: "{api-version=v*}/{tenant=Default}/{controller=*}/{action=Index}/{id?}");

					//app.Use(async (context, next) =>
					//{
					//	await next();
					//	//	if (context.Response.StatusCode == 404)
					//	//	{
					//	//		context.Response.Redirect("/v1/Default/Home/Index");
					//	//		return;
					//	//	}
					//});
				})
				.AddMvc()
				.WithTenants();
		}

		public void Configure(WebApplication app, IWebHostEnvironment env)
		{
			app.UseOrchardCore(builder =>
			{
				if (env.IsEnvironment(Environments.Development))
				{
					app
						.UseDeveloperExceptionPage();
						//.UseSwagger()
						//.UseSwaggerUI();
				}
				else
				{
					app.UseExceptionHandler("/v1/Default/Home/Error");
					app.UseHsts();
				}

				builder
					.UseHttpsRedirection()
					.UseStaticFiles()
					.UseCookiePolicy()
					.UseAuthentication();
			});

			app.Run();
		}

		private void DefaultCorsPolicy(CorsOptions options)
		{
			var origins = Configuration.GetSection(ConfigSectionCorsOriginsName)
				.Get<string[]>();

			if (origins != null)
			{
				options
					.AddPolicy(DefaultCorsPolicyName, p => p
					.SetIsOriginAllowedToAllowWildcardSubdomains()
					.WithOrigins(origins)
					.WithHeaders("Authorization")
					.AllowAnyMethod()
					.AllowAnyHeader());
			}
		}
	}
}
