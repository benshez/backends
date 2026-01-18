namespace Shezzy.Web.ContentPermissions.Models
{
    public class ContentPermissionsPartSettings
    {
        public string RedirectUrl { get; set; }

        public bool HasRedirectUrl
        {
            get { return !string.IsNullOrWhiteSpace(RedirectUrl); }
        }
    }
}
