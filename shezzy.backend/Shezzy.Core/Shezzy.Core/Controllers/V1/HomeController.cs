using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shezzy.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Shezzy.Core.User;
using AutoMapper;
using OrchardCore.Environment.Shell;

namespace Shezzy.Core.Controllers.V1
{
	[ApiController]
	[Route("v{version:apiVersion}/{tenant}/Home")]
	[ApiVersion("1.0")]
	public class HomeController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IShellSettingsManager _shellSettingsManager;
		private readonly Serilog.ILogger _logger;
		public HomeController(
			IMapper mapper,
			IUserService userService,
			IShellSettingsManager shellSettingsManager)
		{
			_mapper = mapper;
			_userService = userService;
			_shellSettingsManager = shellSettingsManager;
			_logger = Log.ForContext<HomeController>();
			//logger.Information("HomeController Found");

		}
		[Route("Index")]
		public async Task<IActionResult> Index(ApiVersion version, string? tenant="Default")
		{
			ViewData["tenant"] = tenant ?? "Default";

			var data = GoogleResponse();

			var model = await _shellSettingsManager.LoadSettingsAsync(tenant);

			return View(model);
		}
		[Route("About")]
		public async Task<IActionResult> About(string? tenant = "Default")
		{
			ViewData["Message"] = "Your application description page.";
			ViewData["tenant"] = tenant ?? "Default";

			var model = await _shellSettingsManager.LoadSettingsAsync(tenant);

			return View(model);
		}
		[Route("Contact")]
		public async Task<IActionResult> Contact(string? tenant = "Default")
		{
			ViewData["Message"] = "Your contact page.";
			ViewData["tenant"] = tenant ?? "Default";

			var model = await _shellSettingsManager.LoadSettingsAsync(tenant);

			return View(model);
		}
		[Route("Privacy")]
		public async Task<IActionResult> Privacy(string? tenant = "Default")
		{
			ViewData["tenant"] = tenant ?? "Default";

			var model = await _shellSettingsManager.LoadSettingsAsync(tenant);

			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		[Route("Error")]
		public IActionResult Error()
		{
			_logger.Error("HomeController Found {Activity}", Activity.Current?.Id ?? HttpContext.TraceIdentifier );
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		[Route("Login")]
		public async Task Login(ApiVersion version, string? tenant = "Default")
		{
			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
			{
				RedirectUri = $"/v{version}/{tenant}{nameof(HomeController)}/Index"
			});
		}

		[Route("LoginAuth0")]
		public async Task LoginAuth0(ApiVersion version, string? tenant = "Default")
		{
			//var result = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

			//var claims = result.Principal?.Identities?
			//	.FirstOrDefault()?.Claims.Select(claim => new
			//	{
			//		claim.Issuer,
			//		claim.OriginalIssuer,
			//		claim.Type,
			//		claim.Value
			//	});
			//var data = await GoogleResponse();

			//ViewData["data"] = data;
			await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties()
			{
				RedirectUri = $"/v{version}/{tenant}{nameof(HomeController)}/Index"
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
		[Route("Logout")]
		[Authorize]
		public async Task<IActionResult> Logout(ApiVersion version, string? tenant = "Default")
		{
			await HttpContext.SignOutAsync();
			
			return Redirect($"/v{version}/{tenant}/{nameof(HomeController)}/Index");
		}
	}
}
