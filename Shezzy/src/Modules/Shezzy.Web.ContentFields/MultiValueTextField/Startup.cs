using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Modules;
using Shezzy.Web.ContentFields.MultiValueTextField.Drivers;
using Shezzy.Web.ContentFields.MultiValueTextField.Settings;
using Shezzy.Web.ContentFields.MultiValueTextField.ViewModels;

namespace Shezzy.Web.ContentFields.MultiValueTextField
{
    [Feature(Constants.Features.MultiValueTextField)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentField<Fields.MultiValueTextField>()
                .UseDisplayDriver<MultiValueTextFieldDisplayDriver>();
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, MultiValueTextFieldSettingsDriver>();
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, TextFieldHeaderDisplaySettingsDriver>();
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, MultiValueTextFieldEditorSettingsDriver>();
            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<Fields.MultiValueTextField>();
                o.MemberAccessStrategy.Register<DisplayMultiValueTextFieldViewModel>();
            });
        }
    }
}
