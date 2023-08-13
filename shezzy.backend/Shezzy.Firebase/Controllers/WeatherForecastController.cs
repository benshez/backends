using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Shell;
using Shezzy.Firebase.Services.Form;

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
        private readonly ShellSettings _shellHost;
        private readonly IDatabaseQueryService _queryService;
        private readonly FirestoreDb _fireStoreDb;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ShellSettings shellHost,
            IDatabaseQueryService queryService)
        {
            _logger = logger;
            _shellHost = shellHost;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<Dictionary<string, object>> Get()
        {

            Dictionary<string, object> snap = await _queryService
                .Get(_shellHost.RequestUrlPrefix);
            //string x = query.Get().Serialize();

            var rng = new Random();
            return snap;
        }
    }
}
