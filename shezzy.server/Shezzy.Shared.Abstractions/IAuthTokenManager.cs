namespace Shezzy.Shared.Abstractions
{
  public interface IAuthTokenManager
  {
    Task<string> GetAuthToken(string tokenCachekey, string audience);
  }
}