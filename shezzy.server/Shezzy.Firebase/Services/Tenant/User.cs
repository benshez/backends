using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace Shezzy.Firebase.Services.Tenant
{
    [FirestoreData]
    public class User : IFirebaseEntity
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        [FirestoreProperty]
        public string TenantId { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string EmailAddress { get; set; }
        [FirestoreProperty]
        public UserRoles UserRoles { get; set; }

        public List<Claim> GetClaims() {
            var claims = new List<Claim> { };

            claims.Add(new Claim(ClaimTypes.Sid, Id));
            claims.Add(new Claim(ClaimTypes.GroupSid, TenantId));
            UserRoles.Tenant.ToList().ForEach(_ => claims.Add(new Claim(ClaimTypes.Role, $"Tenant.{_}", DateTime.Now.ToString(CultureInfo.InvariantCulture))));
            UserRoles.User.ToList().ForEach(_ => claims.Add(new Claim(ClaimTypes.Role, $"User.{_}", DateTime.Now.ToString(CultureInfo.InvariantCulture))));

            return claims;
        }
    }

    [FirestoreData]
    public class UserRoles
    {
        [FirestoreProperty]
        public IEnumerable<string> Tenant { get; set; }
        [FirestoreProperty]
        public IEnumerable<string> User { get; set; }
    }
}
