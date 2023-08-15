using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OrchardCore.Environment.Shell;
using Shezzy.Firebase.Services.Form;
using Shezzy.Firebase.Services.Tenant;

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
        private readonly IFirestoreQueryService _queryService;
        private readonly CancellationToken _cancellationToken;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ShellSettings shellHost,
            IFirestoreQueryService queryService)
        {
            _logger = logger;
            _shellHost = shellHost;
            _queryService = queryService;
            _cancellationToken = new CancellationTokenSource().Token;
        }

        [HttpGet]
        public async Task<Dictionary<string, object>> Get()
        {


            //var user = new User() { FirstName = "Artur" };
            //await _queryService.AddOrUpdate(user, token);
            IReadOnlyCollection<User> usr = await _queryService.GetAll<User>("", _cancellationToken);

            Dictionary<string, object> snap = await _queryService
                .Get(_shellHost.RequestUrlPrefix);
            //string x = query.Get().Serialize();

            var rng = new Random();
            return snap;
        }

        [HttpPost]
        public async Task<User> AddOrUpdate(User user) 
        {
            await _queryService.AddOrUpdate(user, _cancellationToken);

            return await Get(user.Id);
        }

        [HttpGet]
        public async Task<User> Get(string id)
        {
            return await _queryService.Get<User>(id, _cancellationToken);
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<User>> GetAll()
        {
            return await _queryService.GetAll<User>("", _cancellationToken);
        }
    }
}
