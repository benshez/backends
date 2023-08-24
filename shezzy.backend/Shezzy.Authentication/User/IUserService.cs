using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace Shezzy.Authentication.User
{
	public interface IUserService
	{
        public Task<UserResponseDTO> Authenticate(string schema = CookieAuthenticationDefaults.AuthenticationScheme);
        public UserResponseDTO GetUserInfo();
    }
}
