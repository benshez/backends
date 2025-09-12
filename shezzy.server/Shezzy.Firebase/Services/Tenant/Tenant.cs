using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace Shezzy.Firebase.Services.Tenant
{
    [FirestoreData]
    public class Tenant : IFirebaseEntity
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public ICollection<string> Users { get; set; }
    }
}
