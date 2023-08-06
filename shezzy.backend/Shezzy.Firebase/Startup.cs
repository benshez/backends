using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shezzy.Firebase.Credentials;

namespace Shezzy.Firebase
{
    public class Startup 
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IEndpointRouteBuilder endpoints)
        {
        }
        public void ConfigureServices(IServiceCollection services)
        {
           
            //services
            //    .AddTransient<ICredentials, CredentialsModel>();

            //var creds = new CredentialsModel(Configuration);

            //services
            //    .AddGoogleDiagnosticsForAspNetCore(projectId: "shezzy-form", loggingOptions: LoggingOptions.Create(logLevel: LogLevel.Debug));

        }
    }
}
