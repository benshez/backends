using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Environment.Shell;
using System.Threading.Tasks;
using Shezzy.Authentication.User;
using Microsoft.AspNetCore.Authentication.Google;

namespace Shezzy.Authentication.Controllers
{
    public class AuthenticationController : Controller
    {
        //private readonly IMapper _mapper;
        private readonly IUserService _userService;
        //private readonly IShellSettingsManager _shellSettingsManager;
        //private readonly Serilog.ILogger _logger;

        public AuthenticationController(IUserService userService) {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var data = GoogleResponse();

            return View();
        }

        public async Task Login(string a)
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = $"./Index"
            });
        }


        public async Task<IActionResult> GoogleResponse()
        {
            //_shellSettings = await _shellSettingsManager.LoadSettingsAsync(_tenant);
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var user = _userService.GetUserInfo(result);

            ViewData["data"] = (user);

            return Json(user);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect($"~/Index");
        }
    }
}
