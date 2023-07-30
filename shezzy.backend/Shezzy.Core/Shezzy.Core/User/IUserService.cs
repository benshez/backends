using Microsoft.AspNetCore.Authentication;

namespace Shezzy.Core.User
{
	public interface IUserService
	{
		public UserResponseDTO GetUserInfo(AuthenticateResult authenticateResult);
	}
}
