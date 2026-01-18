using Shezzy.Web.ContentPermissions.Models;

namespace Shezzy.Web.ContentPermissions.ViewModels
{
    public class ContentPermissionsPartEditViewModel
    {
        public ContentPermissionsPart ContentPermissionsPart { get; set; }

        public bool Enabled { get; set; }

        public string[] PossibleRoles { get; set; }

        public string[] Roles { get; set; }
    }
}
