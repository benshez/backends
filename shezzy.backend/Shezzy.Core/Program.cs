using Google.Api;
using Shezzy.Shared.Abstractions;
using Shezzy.Shared.MemoryCacheProvider;
using Shezzy.Shared.OAuth;
using Shezzy.Shared.Settings;
using Shezzy.Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
        .AddMemoryCache();

builder
    .Services
        .RegisterHttpClients(builder.Configuration);

builder
    .Services
        .AddTransient<IApplicationSettings, ApplicationSettings>()
        .AddTransient<IAuthTokenManager, AuthTokenManager>()
        .AddTransient<IMemoryCacheProvider, MemoryCacheProvider>()
        .AddTransient<ICredentials, CredentialsModel>();

builder
    .Services
        .AddOrchardCore()
        .AddMvc()
        .WithTenants();

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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseOrchardCore();

app.Run();
