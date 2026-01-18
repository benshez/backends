using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin.Models;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;
using Shezzy.Themes.FormBuilderTheme.Drivers;

namespace Shezzy.Themes.FormBuilderTheme
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
            services.AddDisplayDriver<Navbar, ToggleThemeNavbarDisplayDriver>();
        }
    }
}
