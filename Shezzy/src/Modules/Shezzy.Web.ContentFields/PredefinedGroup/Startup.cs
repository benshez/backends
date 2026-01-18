using Shezzy.Web.ContentFields.PredefinedGroup.Settings;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.ContentTypes.Editors;

namespace Shezzy.Web.ContentFields.PredefinedGroup
{
    [Feature(Constants.Features.PredefinedGroup)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, TextFieldPredefinedGroupEditorSettingsDriver>();
        }
    }
}