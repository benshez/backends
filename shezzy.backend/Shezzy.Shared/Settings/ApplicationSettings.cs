using Shezzy.Shared.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Shezzy.Shared.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        private readonly IConfiguration _configuration;
        public ApplicationSettings(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Uri Auth0AuthorityHost => _configuration.GetValue<Uri>("Metadata:Version");
        public string Auth0ClientId => _configuration.GetValue<string>("Metadata:Version");
        public string Auth0ClientSecret => _configuration.GetValue<string>("Metadata:Version");
    }
}
