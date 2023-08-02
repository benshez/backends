using Microsoft.AspNetCore.Authentication;

namespace Shezzy.Authentication.User
{
	public interface IUserService
	{
		public UserResponseDTO GetUserInfo(AuthenticateResult authenticateResult);
	}
}
