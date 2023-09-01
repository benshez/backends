using Google.Api;
using Shezzy.Shared.Abstractions;
using Shezzy.Shared.MemoryCacheProvider;
using Shezzy.Shared.OAuth;
using Shezzy.Shared.Settings;
using Shezzy.Shared.Extentions;
using Shezzy.Shared.Abstractions.Credentials;
using Serilog;
using Shezzy.Shared.Logger;
using Shezzy.Authentication.User;
using Shezzy.Authentication.Extentions;
using Shezzy.Authentication.Services;
using Microsoft.AspNetCore.Authentication;
using Shezzy.Firebase.Services.Form;
using Shezzy.Firebase.Services;

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
        .AddTransient<IFirebaseCredentials, FirebaseCredentialsModel>()
        .AddTransient<IFirestoreQueryService, FirestoreQueryService>()
        .AddTransient<IFirestoreProvider, FirestoreProvider>()
        .AddTransient<IClaimsTransformation, ClaimsTransformer>();

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
               })
               .AddCookie(options =>
               {
                   options.Cookie.SameSite = SameSiteMode.Unspecified;
                   options.ExpireTimeSpan = TimeSpan.FromDays(1);
               })
               .RegisterAuthorizationBuilder(builder.Configuration)
               .RegisterAuthenticationBuilder(builder.Configuration);

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
