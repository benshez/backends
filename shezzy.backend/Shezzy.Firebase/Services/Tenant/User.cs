using Google.Cloud.Firestore;
using System;

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
    }
}
