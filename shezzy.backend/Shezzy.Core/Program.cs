using Google.Api;
using Shezzy.Shared.Abstractions;
using Shezzy.Shared.MemoryCacheProvider;
using Shezzy.Shared.OAuth;
using Shezzy.Shared.Settings;
using Shezzy.Shared.Extentions;
using Shezzy.Shared.Abstractions.Credentials;
using Serilog;
using Shezzy.Shared.Logger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shezzy.Authentication.User;
using Shezzy.Authentication.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Shezzy.Authentication.Middleware;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection;
using System.Text;
using Microsoft.Extensions.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/rumble-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
        .AddMemoryCache();

builder
    .Services
        .RegisterHttpClients(builder.Configuration);

builder
    .Services
        .AddTransient<ISLogger, Logger>()
        .AddTransient<IApplicationSettings, ApplicationSettings>()
        .AddTransient<IAuthTokenManager, AuthTokenManager>()
        .AddTransient<IMemoryCacheProvider, MemoryCacheProvider>()
        .AddTransient<IFirebaseCredentials, FirebaseCredentialsModel>();

builder
    .Services
    .AddSingleton<IUserService, UserService>();



builder
    .Services
        .AddOrchardCore()
        .ConfigureServices(services =>
        {
            services.AddHttpContextAccessor();
            services
               .AddAuthentication(options =>
               {
                   options.DefaultScheme = "JWT_OR_COOKIE";
                   options.DefaultChallengeScheme = "JWT_OR_COOKIE";
                   //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                   //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddCookie(options =>
               {
                   options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Unspecified;
                   options.ExpireTimeSpan = TimeSpan.FromDays(1);
               })
            .AddJwtBearer(options =>
            {
                var section = builder.Configuration.GetSection("JWT");
                var secret = string.IsNullOrEmpty(section.GetValue<string>("Secret")) ? "" : section.GetValue<string>("Secret");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                   var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = securityKey,
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
                   //options.SecurityTokenValidators.Clear();
                   //options.SecurityTokenValidators.Add(new GoogleTokenValidator());
               })
                .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options => {
                    options.ForwardDefaultSelector = context =>
                    {
                        string authorization = context.Request.Headers[HeaderNames.Authorization];
                        if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                            return JwtBearerDefaults.AuthenticationScheme;

                        return CookieAuthenticationDefaults.AuthenticationScheme;
                    };
                })
               .RegisterAuthenticationBuilder(builder.Configuration);
            //.RegisterAuthorizationBuilder(builder.Configuration);

            //var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme)
            //    .RequireAuthenticatedUser()
            //    .Build();

            //services
            //    .AddAuthorization(m => m.DefaultPolicy = policy);
            services
                .AddControllersWithViews();
        })
        .Configure((builder, app) =>
        {
            app.MapRazorPages();

            builder.UseStaticFiles();
            builder.UseCookiePolicy();
            builder.UseHttpsRedirection();
            builder.UseRouting();
            builder.UseAuthentication();
            builder.UseAuthorization();
        })
        .AddMvc()
        .WithTenants();

builder
    .Services
        .AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseOrchardCore();

app.Run();
