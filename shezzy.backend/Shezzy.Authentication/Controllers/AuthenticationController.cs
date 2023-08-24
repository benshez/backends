using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shezzy.Authentication.User;
using Microsoft.AspNetCore.Authentication.Google;
using OrchardCore.Environment.Shell;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

namespace Shezzy.Authentication.Controllers
{
    public class AuthenticationController : Controller
    {
        //private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ShellSettings _shellHost;
        private SymmetricSecurityKey _securityKey;
        private SymmetricSecurityKey SecurityKey
        {
            get
            {
                if (_securityKey == null)
                {
                    _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").GetValue<string>("Secret")));
                }

                return _securityKey;
            }
        }
        //private readonly IShellSettingsManager _shellSettingsManager;
        //private readonly Serilog.ILogger _logger;

        public AuthenticationController(
            ShellSettings shellHost,
            IConfiguration configuration,
            IUserService userService
            )
        {
            _shellHost = shellHost;
            _configuration = configuration;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var data = GoogleResponse();
            return View();
        }
        public async Task<IActionResult> LoggedIn()
        {
            var acccessToken = await HttpContext.GetTokenAsync("access_token");
            //var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var user = await _userService.Authenticate(JwtBearerDefaults.AuthenticationScheme);

            if (user != null)
            {
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, HttpContext.User, new AuthenticationProperties
                //{
                //    IsPersistent = true,
                //    AllowRefresh = true,
                //    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                //});
                //var user = _userService.GetUserInfo(result);


                // var token = _userService.GenerateJwtToken();
                //await HttpContext.GetTokenAsync("access_token");
                //HttpContext.Request.Cookies.Append("token", token);
                // return Ok(return Ok(_userService.GetUserInfo().AccessToken););
                return Ok(user.AccessToken);
            }

            return Ok(user);
        }
        public async Task Login()
        {
            var redirectUrl = Url.Action("LoggedIn", "Authentication");

            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action(nameof(LoggedIn))
                //RedirectUri = $"./Index"
                //RedirectUri = redirectUrl//"https://localhost:44380/dream-weddings/Shezzy.Authentication/Authentication/LoggedIn"
            });
            //RedirectToAction("LoggedIn", "Authentication");

        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            //var acccessToken = await HttpContext.GetTokenAsync("access_token");
            //var result = await _userService.Authenticate();
       
            //if (result != null)
            //{
            //    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, HttpContext.User, new AuthenticationProperties
            //    //{
            //    //    IsPersistent = true,
            //    //    AllowRefresh = true,
            //    //    ExpiresUtc = DateTime.UtcNow.AddDays(1)
            //    //});
            //   // result.Principal.AddIdentity(HttpContext.User); 
            //    // var token = _userService.GenerateJwtToken(_configuration, user);
            //    //await HttpContext.GetTokenAsync("access_token");
            //    //HttpContext.Request.Cookies.Append("token", token);
            //    return Ok(result.AccessToken);
            //}

            return Ok();

        }
        public async Task<IActionResult> GoogleResponse()
        {
            //var user = await _userService.Authenticate();
            //var user = _userService.GetUserInfo();

            //if (user != null)
            //{
            //    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, HttpContext.User, new AuthenticationProperties
            //    //{
            //    //    IsPersistent = true,
            //    //    AllowRefresh = true,
            //    //    ExpiresUtc = DateTime.UtcNow.AddDays(1)
            //    //});

            //    // var token = _userService.GenerateJwtToken(_configuration, user);
            //    //await HttpContext.GetTokenAsync("access_token");
            //    //HttpContext.Request.Cookies.Append("token", token);
            //}
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            //// var token = user.AccessToken;

            // if (token == null && user.Name != null)
            // {
            //     user.AccessToken = GetToken(_userService.GetUserClaims(result).ToList());
            // }
            //var user = _userService.GetUserInfo();
            //ViewData["data"] = (user);

            //return Json(user);
            return Ok();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect($"~/Index");
        }

        //private string GetToken(List<Claim> authClaims)
        //{
        //    var configuration = _configuration.GetSection("JWT");

        //    var token = new JwtSecurityToken(
        //        issuer: configuration.GetValue<string>("ValidIssuer"),
        //        audience: configuration.GetValue<string>("ValidAudience"),
        //        expires: DateTime.Now.AddHours(3),
        //        claims: new List<Claim> {},
        //        signingCredentials: new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256));

        //    var tkn = new
        //    {
        //        token = new JwtSecurityTokenHandler().WriteToken(token),
        //        expiration = token.ValidTo
        //    }.token;

        //    return tkn;
        //}
    }
}
