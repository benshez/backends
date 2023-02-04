using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shezzy.Api.Authentication;
using System.Text.Json;
using Shezzy.Api.Models;



var DefaultCorsPolicyName = "shezzy.backend";
var ConfigSectionCorsOriginsName = "CorsOrigins";
var builder = WebApplication.CreateBuilder(args);
var context = builder.Environment;

builder.Configuration.AddJsonFile("appsettings.json", false, true);

if (context.IsEnvironment("Development"))
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", true, true);
}

var origins = builder.Configuration
    .GetSection(ConfigSectionCorsOriginsName)
    .Get<string[]>();

builder.Configuration.AddEnvironmentVariables();
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DefaultCorsPolicyName,
        policy =>
        {
            policy
            .WithOrigins(origins)
            .WithHeaders("Authorization");
        });
});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie()
//.AddAuth0(options =>
//{
//    options.Domain = builder.Configuration["Auth0:Domain"];
//    options.ClientId = builder.Configuration["Auth0:ClientId"];
//    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
//});
var dom = builder.Configuration["Auth0:Domain"];
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = $"https://{dom}/";
//        options.Audience = builder.Configuration["Auth0:Audience"];

//        options.Events = new JwtBearerEvents
//        {
//            OnChallenge = context =>
//            {
//                context.Response.OnStarting(async () =>
//                {
//                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ApiResponse("You are not authorized!")));
//                });

//                return Task.CompletedTask;
//            }
//        };
//    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-ne-r1w-2.au.auth0.com/";
    options.Audience = "http://localhost:4025/";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                        (c.Type == "permissions" &&
                        c.Value == "read:admin_messages") &&
                        c.Issuer == $"https://{dom}/")));

});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(DefaultCorsPolicyName);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
