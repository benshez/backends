using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Shezzy.Authentication.User;
using System.Security.Claims;

namespace Shezzy.Authentication.User
{
	public class UserTransformer : IUser
	{
		public string? Name { get; set; }
		public string? GivenName { get; set; }
		public string? Picture { get; set; }
		public string? EmailAddress { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Sub { get; set; }
		public string? NameIdentifier { get; set; }
		public bool? EmailVerified { get; set; }
		public static UserTransformer TransformUser(AuthenticateResult authenticateResult)
		{
			var claims = authenticateResult
				.Principal?
				.Identities?
				.FirstOrDefault()?
				.Claims
				.Select(claim => new
				{
					claim.Issuer,
					claim.OriginalIssuer,
					claim.Type,
					claim.Value
				});

			UserTransformer user = new();
			UserDTO userDto = new();
			var config = new MapperConfiguration(cfg =>
			{
				claims?.ToList().ForEach(claim =>
				{
					cfg.CreateMap<Claim, UserDTO>()
						.ForMember(dest => dest.NameIdentifier, opt =>
						{
							opt.PreCondition(s => s.Type == ClaimTypes.NameIdentifier);
							opt.MapFrom(src => src.Value);
						});
				});
			});

			IMapper mapper = config.CreateMapper();
			

			claims?.ToList().ForEach(claim =>
				{
					if (claim.Type == ClaimTypes.NameIdentifier) user.NameIdentifier = claim.Value;
					if (claim.Type == ClaimTypes.Name) user.Name = claim.Value;
					if (claim.Type == ClaimTypes.GivenName) user.GivenName = claim.Value;
					if (claim.Type == ShezzyClaimTypes.Picture) user.Picture = claim.Value;
					if (claim.Type == ClaimTypes.Email) user.EmailAddress = claim.Value;
					if (claim.Type == ShezzyClaimTypes.PhoneNumber) user.PhoneNumber = claim.Value;
					if (claim.Type == ShezzyClaimTypes.Sub) user.Sub = claim.Value;
					if (claim.Type == ShezzyClaimTypes.EmailVerified) user.EmailVerified = Convert.ToBoolean(claim.Value);
				});

			mapper.Map<UserDTO>(user);
			return mapper.Map<UserDTO>(user); ;
		}
	}
}
