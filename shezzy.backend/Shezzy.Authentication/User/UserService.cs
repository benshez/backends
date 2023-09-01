using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shezzy.Firebase.Services.Tenant;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private AuthenticateResult _authenticateResult = null;
        private IEnumerable<Claim> _claims = new List<Claim>();

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
                _authenticateResult = await _context.HttpContext.AuthenticateAsync(schema);
       
                var usr = GetUserInfo();
                var indentity = _context.HttpContext.User.Identity;

                if (indentity != null && !indentity.IsAuthenticated) return null;
                 
                return user;
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
            _claims = _authenticateResult
                .Principal?
                .Identities?
                .FirstOrDefault()?
                .Claims;

            if (_claims != null)
            {
                foreach (var claim in _claims)
                {
                    if (claim.Type == ClaimTypes.NameIdentifier) _user.NameIdentifier = claim.Value;
                    if (claim.Type == ClaimTypes.Name) _user.Name = claim.Value;
                    if (claim.Type == ClaimTypes.GivenName) _user.GivenName = claim.Value;
                    if (claim.Type == UserClaimTypes.Picture) _user.Picture = claim.Value;
                    if (claim.Type == ClaimTypes.Email) _user.EmailAddress = claim.Value;
                    if (claim.Type == UserClaimTypes.PhoneNumber) _user.PhoneNumber = claim.Value;
                    if (claim.Type == UserClaimTypes.Sub) _user.Sub = claim.Value;
                    if (claim.Type == UserClaimTypes.Audience) _user.Sub = claim.Value;
                    if (claim.Type == UserClaimTypes.EmailVerified) _user.EmailVerified = Convert.ToBoolean(claim.Value);
                    _user.Claims ??= new List<Claim>();

                    _user.Claims.Append(claim);
                };
            };

            return _claims;
        }
        private object GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.GetValue<string>("ValidIssuer"),
                audience: _config.GetValue<string>("ValidAudience"),
                claims: _context.HttpContext.User.Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}