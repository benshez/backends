using Microsoft.AspNetCore.Authentication;

namespace Shezzy.Authentication.Services
{
	public class UserService : IUser
	{
		public string? Name { get; set; }
		public string? GivenName { get; set; }
		public string? Picture { get; set; }
		public string? EmailAddress { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Sub { get; set; }
		public bool? EmailVerified { get; set; }

		public static UserService FromAuthenticateResult(AuthenticateResult authenticateResult)
		{
			var claims = authenticateResult.Principal?.Identities?
				.FirstOrDefault()?.Claims.Select(claim => new
				{
					claim.Issuer,
					claim.OriginalIssuer,
					claim.Type,
					claim.Value
				});

			UserService user = new();

			claims?.ToList().ForEach(claim =>
				{
					if (claim.Type == "name")
					{
						user.Name = claim.Value;
					}
					if (claim.Type == "givenname")
					{
						user.GivenName = claim.Value;
					}
					if (claim.Type == "emailaddress")
					{
						user.EmailAddress = claim.Value;
					}
				});

			return user;
			//	return principal.?.Identities ?

		}
	}
}
