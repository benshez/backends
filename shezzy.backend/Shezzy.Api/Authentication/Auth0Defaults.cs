namespace Shezzy.Api.Authentication
{
    public class Auth0Defaults
    {
        public const string AuthenticationScheme = "Auth0";
        public const string ClaimsIssuer = "Auth0";

        public static readonly string CallbackPath = new PathString("/callback");
    }
}
