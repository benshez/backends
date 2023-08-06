using Google.Cloud.Firestore;
using Shezzy.Shared.Infrastructure.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Shezzy.Firebase.Services.Form
{
    public class DatabaseQueryResponse
    {
        List<DatabaseResultModel>? Bookings { get; set; }

        public static List<DatabaseResultModel>? Deserialize(QuerySnapshot querySnapshot)
        {
            if (querySnapshot == null) return null;

            return new DatabaseQueryResponse()
            {
                Bookings = (from DocumentSnapshot documentSnapshot in querySnapshot.Documents
                            where documentSnapshot.Exists
                            select new DatabaseResultModel()
                            {
                                shezzy = JsonSerializer.Deserialize<Shezzy>(Serialize(documentSnapshot), JsonOptions.Default),
                                Id = documentSnapshot.Id
                            }).ToList()
            }.Bookings;
        }

        public static string Serialize(DocumentSnapshot documentSnapshot)
        {
            return JsonSerializer.Serialize(documentSnapshot.ToDictionary(), JsonOptions.Default);
        }
    }
}
