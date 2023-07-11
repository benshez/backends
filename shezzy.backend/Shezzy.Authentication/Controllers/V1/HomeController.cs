using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shezzy.Authentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Shezzy.Authentication.User;
using AutoMapper;

namespace Shezzy.Authentication.Controllers.V1
{
	public class HomeController : Controller
	{
		private readonly IMapper _mapper;
		public HomeController(IMapper mapper)
		{
			_mapper = mapper;
			//var logger = Log.ForContext<HomeController>();
			//logger.Information("HomeController Found");

		}
		public IActionResult Index()
		{
			var data = GoogleResponse();

			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task Login()
		{
			//var data = await GoogleResponse();

			//ViewData["data"] = data;

			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
			{

				//RedirectUri = Url.Action("GoogleResponse")
				RedirectUri = Url.Action("Index")
			});
		}

		public async Task LoginAuth0()
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

				//RedirectUri = Url.Action("GoogleResponse")
				RedirectUri = Url.Action("Index")
			});

		}

		public async Task<IActionResult> GoogleResponse()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			var user = UserTransformer.TransformUser(result);

			var claims = result.Principal?.Identities?
				.FirstOrDefault()?.Claims.Select(claim => new
				{
					claim.Issuer,
					claim.OriginalIssuer,
					claim.Type,
					claim.Value
				});
			ViewData["data"] = (user);
			return Json(claims);
		}
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index");
		}
	}

}
