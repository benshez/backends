//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Firebase.Auth;
//using Firebase.Auth.Providers;

//namespace Shezzy.Api.Controllers.V1;

//[Route("api/v{version:apiVersion}")]
//[ApiVersion("1.0")]
//[ApiController]
//public class WeatherForecastController : ControllerBase
//{
//    private static readonly string[] Summaries = new[]
//    {
//        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//    };

//    private readonly ILogger<WeatherForecastController> _logger;
//    private readonly IFirebaseAuthClient _client;
//    public WeatherForecastController(ILogger<WeatherForecastController> logger, IFirebaseAuthClient client)
//    {
//        _logger = logger;
//        _client = client;
//    }

//    [HttpGet("GetWeatherForecast")]
//    [Produces("application/json")]
//    //[Authorize(Policy = "Admin")]
//    public async Task<IEnumerable<WeatherForecast>> Get()
//    {
//        UserCredential userCredential = await _client.SignInWithRedirectAsync(FirebaseProviderType.Google, uri =>
//        {
//            //WriteLine($"Go to \n{uri}\n and paste here the redirect uri after you finish signing in");
//            return Task.FromResult("http://localhost:4024");
//        });
//        //UserCredential userCredential = await _client.SignInAnonymouslyAsync();
//        //await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = "http://localhost:4025" });
//        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//        {
//            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            TemperatureC = Random.Shared.Next(-20, 55),
//            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//        })
//        .ToArray();
//    }
//}
