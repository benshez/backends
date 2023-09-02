using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrchardCore.Modules;
using Shezzy.Authentication.User;
using Shezzy.Firebase.Services;
using Shezzy.Firebase.Services.Form;
using Shezzy.Firebase.Services.Tenant;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Shezzy.Authentication.Services
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IConfiguration _configuration;
        private readonly IFirestoreQueryService _queryService;
        private readonly UserResponseDTO _user;
        private readonly CancellationToken _cancellationToken;
        public ClaimsTransformer(
            IConfiguration configuration,
            IFirestoreQueryService queryService)
        {
            _configuration = configuration;
            _queryService = queryService;
            _user = new UserResponseDTO();
            _cancellationToken = new CancellationTokenSource().Token;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var transformed = new ClaimsPrincipal();

            if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
            {
                transformed.AddIdentities(principal.Identities);

                if (principal.Claims != null)
                {
                    foreach (var claim in principal.Claims)
                    {
                        if (claim.Type == ClaimTypes.NameIdentifier) _user.NameIdentifier = claim.Value;
                        if (claim.Type == ClaimTypes.Name) _user.Name = claim.Value;
                        if (claim.Type == ClaimTypes.GivenName) _user.GivenName = claim.Value;
                        if (claim.Type == UserClaimTypes.Picture) _user.Picture = claim.Value;
                        if (claim.Type == ClaimTypes.Email) _user.EmailAddress = claim.Value;
                        if (claim.Type == UserClaimTypes.PhoneNumber) _user.PhoneNumber = claim.Value;
                        if (claim.Type == UserClaimTypes.Sub) _user.Sub = claim.Value;
                        if (claim.Type == UserClaimTypes.Audience) _user.Sub = claim.Value;
                        if (claim.Type == UserClaimTypes.EmailVerified) _user.EmailVerified = Convert.ToBoolean(claim.Value);
                    }
                }

                var user = _queryService
                    .WhereEqualTo<Firebase.Services.Tenant.User>("EmailAddress", _user.EmailAddress, _cancellationToken)
                    .GetAwaiter()
                    .GetResult();

                if (user != null && user.Count != 0)
                {
                    transformed.AddIdentity(
                        new ClaimsIdentity(user.FirstOrDefault()?.GetRoles()));
                }
            }

            return Task.FromResult(transformed);
        }
    }
}
