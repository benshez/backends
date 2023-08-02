using Microsoft.AspNetCore.Authentication;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shezzy.Authentication.Services
{
	public class ClaimsTransformer : IClaimsTransformation
	{
		public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			var transformed = new ClaimsPrincipal();
			transformed.AddIdentities(principal.Identities);
			transformed.AddIdentity(new ClaimsIdentity(new Claim[]
			{
				new Claim("Transformed", DateTime.Now.ToString(CultureInfo.InvariantCulture))
			}));

			return Task.FromResult(transformed);
		}
	}
}
