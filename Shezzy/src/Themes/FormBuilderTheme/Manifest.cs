using OrchardCore.DisplayManagement.Manifest;
using static Shezzy.Web.Manifest.ShezzyManifestConstants;

[assembly: Theme(
    Name = "FormBuilderTheme",
    Author = DigitalInnovationTeam,
    Website = DigitalInnovationWebsite,
    Version = "1.0.0",
    Description = "Form Builder platform theme",
    Dependencies = new[] {
        "OrchardCore.ContentTypes"
    }
)]
