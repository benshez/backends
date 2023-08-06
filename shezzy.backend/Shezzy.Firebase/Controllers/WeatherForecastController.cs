using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Shell;
using Shezzy.Shared.Abstractions;
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
        private readonly IShellHost _shellHost;
        private readonly IDatabaseService _databaseService;
        private readonly FirestoreDb _fireStoreDb;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IShellHost shellHost,
            IDatabaseService databaseService)
        {
            _logger = logger;
            _shellHost = shellHost;
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IEnumerable<DatabaseResultModel>> Get()
        {

            DatabaseQueryService query = new DatabaseQueryService( _databaseService.DataBase );
            List<DatabaseResultModel> snap = await query.CreateSnapshot();
            //string x = query.Get().Serialize();

            var rng = new Random();
            return snap;
        }
    }
}
