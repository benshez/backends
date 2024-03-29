using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shezzy.Firebase.Services;
using Shezzy.Firebase.Services.Form;

namespace Shezzy.Firebase
{
    public class Startup 
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app, IEndpointRouteBuilder endpoints)
        {
            //app
            //    .UseAuthorization();
        }
        public void ConfigureServices(IServiceCollection services)
        {

            //services
            //    .AddTransient<IFirestoreQueryService, FirestoreQueryService>()
            //    .AddTransient<IFirestoreProvider, FirestoreProvider>();

            //services
            //    .AddAuthorization();

            //var creds = new CredentialsModel(Configuration);

            //services
            //    .AddGoogleDiagnosticsForAspNetCore(projectId: "shezzy-form", loggingOptions: LoggingOptions.Create(logLevel: LogLevel.Debug));

        }
    }
}
