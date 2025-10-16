using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shezzy.Authentication.User
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration _config;
        private readonly UserResponseDTO _user;
        private readonly string _jwtSecret;

        public UserService(
            IHttpContextAccessor context,
            IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
            _user = new UserResponseDTO();
            _jwtSecret = _config?
                .GetSection("JWT")?
                .GetValue<string>("Secret");
        }
        public async Task<UserResponseDTO> Authenticate(string schema = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            if (_context != null && _context.HttpContext != null)
            { 
                var user = GetUserInfo();

                var indentity = _context.HttpContext.User.Identity;

                if (indentity != null && !indentity.IsAuthenticated) return null;
                 
                return await Task.FromResult(user);
            }

            return null;
        }
        public UserResponseDTO GetUserInfo()
        {
            GetUserClaims();

            _user.AccessToken = GenerateJwtToken();

            return _user;
        }
        public IEnumerable<Claim> GetUserClaims()
        {
            _user.Claims = _context?.HttpContext?.User.Claims;

            return _user.Claims;
        }
        private object GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.GetValue<string>("ValidIssuer"),
                audience: _config.GetValue<string>("ValidAudience"),
                claims: _user.Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}