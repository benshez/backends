using Shezzy.Web.ContentPermissions.Drivers;
using Shezzy.Web.ContentPermissions.Liquid;
using Shezzy.Web.ContentPermissions.Models;
using Shezzy.Web.ContentPermissions.Services;
using Shezzy.Web.ContentPermissions.Settings;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Liquid;
using OrchardCore.Modules;

namespace Shezzy.Web.ContentPermissions
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IContentPermissionsService, ContentPermissionsService>();
            
            services.AddContentPart<ContentPermissionsPart>().UseDisplayDriver<ContentPermissionsDisplay>();

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ContentPermissionsPartSettingsDisplayDriver>();

            services.AddLiquidFilter<UserCanViewFilter>("user_can_view");

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
