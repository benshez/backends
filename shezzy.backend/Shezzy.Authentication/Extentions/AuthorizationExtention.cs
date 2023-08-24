using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Shezzy.Authentication.Extentions
{
    //https://spin.atomicobject.com/2020/07/25/net-core-jwt-cookie-authentication/
    public static class AuthorizationExtention
    {

        public static AuthenticationBuilder RegisterAuthorizationBuilder(
            this AuthenticationBuilder builder,
            IConfiguration configuration)
        {
            var section = configuration.GetSection("JWT");
            var secret = string.IsNullOrEmpty(section.GetValue<string>("Secret")) ? "" : section.GetValue<string>("Secret");

            if (section != null && !string.IsNullOrEmpty(secret))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                builder
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = section.GetValue<string>("ValidIssuer"),
                            ValidAudience = section.GetValue<string>("ValidAudience"),
                            IssuerSigningKey = securityKey
                        };
                    })
                    .AddPolicyScheme("Identity.Application", "Bearer", options =>
                    {
                        options.ForwardDefaultSelector = context =>
                        {
                            string authorization = context.Request.Headers[HeaderNames.Authorization];

                            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                            {
                                return JwtBearerDefaults.AuthenticationScheme;
                            }

                            return CookieAuthenticationDefaults.AuthenticationScheme;
                        };
                    }); ;
            }
            return builder;
        }
    }
}
