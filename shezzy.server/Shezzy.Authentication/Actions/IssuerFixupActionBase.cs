using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Shezzy.Authentication.Actions
{
	public class IssuerFixupActionBase : ClaimAction
	{
		public IssuerFixupActionBase() : base(ClaimTypes.NameIdentifier, string.Empty) { }
#nullable enable
		public override void Run(
			JsonElement userData,
			ClaimsIdentity? identity,
			string issuer)
		{
			identity?.Claims.ToList().ForEach(claim =>
			{
				identity?.RemoveClaim(claim);
				identity?.AddClaim(
					new Claim(claim.Type,
					claim.Value,
					claim.ValueType.Split("/").Last(),
					issuer,
					claim.OriginalIssuer,
					claim.Subject));
			});
		}
#nullable disable
	}
}
