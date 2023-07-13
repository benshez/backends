using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Shezzy.Authentication.User
{
	public class UserService : IUserService
	{
		public UserResponseDTO GetUserInfo(AuthenticateResult authenticateResult)
		{
			UserResponseDTO user = new();
			var claims = GetUserClaims(authenticateResult);

			if(claims != null)
			{
				foreach (var claim in claims)
				{
					if (claim.Type == ClaimTypes.NameIdentifier) user.NameIdentifier = claim.Value;
					if (claim.Type == ClaimTypes.Name) user.Name = claim.Value;
					if (claim.Type == ClaimTypes.GivenName) user.GivenName = claim.Value;
					if (claim.Type == UserClaimTypes.Picture) user.Picture = claim.Value;
					if (claim.Type == ClaimTypes.Email) user.EmailAddress = claim.Value;
					if (claim.Type == UserClaimTypes.PhoneNumber) user.PhoneNumber = claim.Value;
					if (claim.Type == UserClaimTypes.Sub) user.Sub = claim.Value;
					if (claim.Type == UserClaimTypes.EmailVerified) user.EmailVerified = Convert.ToBoolean(claim.Value);
				};
			};

			return user;
		}
		private static IEnumerable<Claim>? GetUserClaims(AuthenticateResult authenticateResult)
		{
			var claims = authenticateResult
				.Principal?
				.Identities?
				.FirstOrDefault()?
				.Claims;

			return claims;
		}
	}
}
