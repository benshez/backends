using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

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
        public IEnumerable<string> Claims { get; set; }
    }
}
