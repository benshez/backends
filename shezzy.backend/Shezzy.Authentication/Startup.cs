using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell;
using Shezzy.Authentication.Extentions;
using Shezzy.Authentication.User;

namespace Shezzy.Authentication
{
    public class Startup : StartupBase
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public override void ConfigureServices(IServiceCollection services)
        {
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
                .AddSingleton<IUserService, UserService>();
        }
        //public void Configure(IEndpointRouteBuilder endpoints, WebApplication app, IWebHostEnvironment env)
        //{
        //    app.UseCookiePolicy()
        //        .UseAuthentication();
        //}

        public override void Configure(IApplicationBuilder app)
        {
            app
                .UseCookiePolicy()
                .UseAuthorization()
                .UseAuthentication();
        }

        //public void Configure(IEndpointRouteBuilder endpoints)
        //{

        //    //endpoints.MapGet("/authentication/hello", async context =>
        //    //{
        //    //    await context.Response.WriteAsync("Hello from Module2!");
        //    //});

        //    //endpoints.MapGet("/authentication/info", async context =>
        //    //{
        //    //    var shellSettings = context.RequestServices.GetRequiredService<ShellSettings>();
        //    //    await context.Response.WriteAsync($"Request from tenant: {shellSettings.Name}");
        //    //});

        //    //		builder
        //    //			.UseHttpsRedirection()
        //    //			.UseStaticFiles()
        //    //			.UseCookiePolicy()
        //    //			.UseAuthentication();
        //}
    }
}
