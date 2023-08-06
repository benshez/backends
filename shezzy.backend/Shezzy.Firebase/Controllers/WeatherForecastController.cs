using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Shell;

namespace Shezzy.Firebase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IShellHost _shellHost;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IShellHost shellHost)
        {
            _logger = logger;
            _shellHost = shellHost;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var shellScope = await _shellHost.GetScopeAsync("Default");

            await shellScope.UsingAsync(async scope =>
            {
                var x = scope.ServiceProvider.ToString();
            //    // You can resolve any service from the shell's Service Provider. This serves instead of injecting
            //    // services in the constructor.
            //    var contentManager = scope.ServiceProvider.GetRequiredService<IApplicationSettings>();

                //    // We can use IContentManager as usual, it'll just work.
                //    // Note that for the sake of simplicity there is no error handling for missing content items here, or
                //    // any authorization. It's up to you to add those :).
                //    //var contentItem = await contentManager.GetAsync(contentItemId);
            });

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
