using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System.Security.Claims;
using System.Text.Json;

namespace Shezzy.Authentication.Extentions
{
	public class IssuerFixupActionBase : ClaimAction
	{
		public IssuerFixupActionBase() : base(ClaimTypes.NameIdentifier, string.Empty) { }

		public override void Run(JsonElement userData, ClaimsIdentity? identity, string issuer)
		{
			identity?.Claims.ToList().ForEach(claim =>
			{
				identity?.RemoveClaim(claim);
				identity?.AddClaim(new Claim(claim.Type.Split("/").Last(), claim.Value, claim.ValueType.Split("/").Last(), issuer, claim.OriginalIssuer, claim.Subject));
			});
		}
	}
}
