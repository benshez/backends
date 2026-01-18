using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace Shezzy.Themes.FormBuilderTheme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineScript("form-builder-theme")
                .SetDependencies("form-builder-js")
                .SetUrl("~/FormBuilderTheme/dist/form-builder.js")
                .SetVersion("1.0.0");

            _manifest
               .DefineScript("font-awesome")
               .SetUrl("~/FormBuilderTheme/Vendor/fontawesome-free/js/all.min.js", "~/FormBuilderTheme/Vendor/fontawesome-free/js/all.js")
               .SetCdn("https://cdn.jsdelivr.net/npm/@fortawesome/fontawesome-free@7.1.0/js/all.min.js", "https://cdn.jsdelivr.net/npm/@fortawesome/fontawesome-free@7.1.0/js/all.js")
               .SetCdnIntegrity("sha384-ujbKXb9V3HdK7jcWL6kHL1c+2Lj4MR4Gkjl7UtwpSHg/ClpViddK9TI7yU53frPN", "sha384-Z4FE6Znluj29FL1tQBLSSjn1Pr72aJzuelbmGmqSAFTeFd42hQ4WHTc0JC30kbQi")
               .SetVersion("7.1.0");

            _manifest
                .DefineStyle("bootstrap-css")
                .SetUrl("~/FormBuilderTheme/css/form-builder-theme-bootstrap.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("jQuery")
                .SetUrl("~/FormBuilderTheme/js/jquery.min.js", "~/FormBuilderTheme/js/jquery.js")
                .SetCdn("https://code.jquery.com/jquery-3.6.0.min.js", "https://code.jquery.com/jquery-3.6.0.js")
                .SetCdnIntegrity("sha384-vtXRMe3mGCbOeY7l30aIg8H9p3GdeSe4IFlP6G8JMa7o7lXvnz3GFKzPxzJdPfGK", "sha384-S58meLBGKxIiQmJ/pJ8ilvFUcGcqgla+mWH9EEKGm6i6rKxSTA2kpXJQJ8n7XK4w")
                .SetVersion("3.6.0");

            _manifest
                .DefineScript("popper")
                .SetUrl("~/FormBuilderTheme/js/popper.min.js", "~/FormBuilderTheme/js/popper.js")
                .SetCdn("https://cdn.jsdelivr.net/npm/popper.js@1.16.1/umd/popper.min.js", "https://cdn.jsdelivr.net/npm/popper.js@1.16.1/umd/popper.js")
                .SetCdnIntegrity("sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN", "sha384-cpSm/ilDFOWiMuF2bj03ZzJinb48NO9IGCXcYDtUzdP5y64Ober65chnoOj1XFoA")
                .SetVersion("1.16.1");

            _manifest
                .DefineScript("bootstrap-js")
                .SetDependencies("jQuery", "popper")
                .SetUrl("~/FormBuilderTheme/js/bootstrap.min.js", "~/FormBuilderTheme/js/bootstrap.js")
                .SetCdn("https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/js/bootstrap.min.js", "https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/js/bootstrap.min.js")
                .SetCdnIntegrity("sha384-+YQ4JLhjyBLPDQt//I+STsc9iw4uQqACwlvpslubQzn4u2UU2UFM80nGisd026JF", "sha384-AOHPfOD4WCwCMAGJIzdIL1mo+l1zLNufRq4DA9jDcW1Eh1T3TeQoRaq9jJq0oAR0")
                .SetVersion("4.6.0");

            _manifest
                .DefineScript("details-polyfill")
                .SetUrl("~/FormBuilderTheme/js/details-element-polyfill.js")
                .SetCdn("https://cdn.jsdelivr.net/npm/details-element-polyfill@2.4.0/details-element-polyfill.min.js")
                .SetCdnIntegrity("sha384-pqTuXkYAErLQ9cr0gZChyC6CxAP3nFWj2wFOwcI/C28oy5UKaMfPuKVeuS9wn3MZ")
                .SetVersion("2.4.0");

        }
        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
            options.ResourceManifests.Add(_manifest);
        }
    }
}
