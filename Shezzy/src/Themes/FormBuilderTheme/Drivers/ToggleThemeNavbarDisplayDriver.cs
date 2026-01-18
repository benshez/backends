using OrchardCore.Admin.Models;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Themes.Services;
using Shezzy.Web.Manifest;

namespace Shezzy.Themes.FormBuilderTheme.Drivers;

public sealed class ToggleThemeNavbarDisplayDriver : DisplayDriver<Navbar>
{
    private readonly ISiteThemeService _siteThemeService;

    public ToggleThemeNavbarDisplayDriver(ISiteThemeService siteThemeService)
    {
        _siteThemeService = siteThemeService;
    }

    public override IDisplayResult Display(Navbar model, BuildDisplayContext context)
    {
        return View("ToggleTheme", model)
            .RenderWhen(async () => await _siteThemeService.GetSiteThemeNameAsync() == "FormBuilderTheme")
            .Location(ShezzyManifestConstants.DisplayType.Detail, "Content:10");
    }
}
