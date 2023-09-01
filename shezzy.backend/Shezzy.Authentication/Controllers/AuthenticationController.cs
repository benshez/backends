using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shezzy.Authentication.User;
using Microsoft.AspNetCore.Authentication.Google;
using OrchardCore.Environment.Shell;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Shezzy.Authentication.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ShellSettings _shellHost;

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
            //ViewData["data"] = GetUser();
            return View();
        }
        public async Task<IActionResult> LoggedIn()
        {
            //return null;
            return Ok(await GetUser());
        }
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action(nameof(LoggedIn))
            });
        }        
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }
        private async Task<IActionResult> GetUser()
        {
            var user = await _userService.Authenticate(JwtBearerDefaults.AuthenticationScheme);

            if (user != null)
            {
                return Ok(user.AccessToken);
            }

            return Ok(user);
        }
    }
}
