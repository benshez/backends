namespace Shezzy.Shared.Abstractions
{
    public interface IApplicationSettings
    {
        Uri Auth0AuthorityHost { get; }
        string Auth0ClientId { get; }
        string Auth0ClientSecret { get; }
    }
}
