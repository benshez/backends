using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
using Google.Apis.Auth.OAuth2;
using Shezzy.Firebase.Credentials;

namespace Shezzy.Firebase
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
            services.AddControllers();

            services.AddCors(_DefaultCorsPolicy);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                });

            services
                .AddTransient<ICredentials, CredentialsModel>();

            var creds = new CredentialsModel(Configuration);

            //GoogleCredential googleCredential =
            //    GoogleCredential.FromJson(creds.Serialize());

            services
                .AddGoogleDiagnosticsForAspNetCore(projectId: "shezzy-form", loggingOptions: LoggingOptions.Create(logLevel: LogLevel.Debug));

            services
                .AddSwaggerGen()
                .AddApiVersioning()
                .AddControllersWithViews(_ =>
                {
                    _.EnableEndpointRouting = false;
                });
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

            app.UseMvcWithDefaultRoute();

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseCookiePolicy();

            app.Run();
        }

        private void _DefaultCorsPolicy(CorsOptions options)
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