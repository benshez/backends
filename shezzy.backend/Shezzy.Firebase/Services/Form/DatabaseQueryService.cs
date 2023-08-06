using Google.Cloud.Firestore;
using System.Text.Json;
using System.Threading.Tasks;
using Shezzy.Shared.Infrastructure.Json;
using System.Linq;
using System.Collections.Generic;

namespace Shezzy.Firebase.Services.Form
{
    public class DatabaseQueryService : IDatabaseQueryService
    {
        private readonly FirestoreDb _database;
        private string _response;
        public DocumentSnapshot Snapshot { get; set; }

        public DatabaseQueryService(
            FirestoreDb database)
        {
            _database = database;
        }

        async public Task<List<DatabaseResultModel>> CreateSnapshot()
        {
            Query query = _database.Collection("shezzy-form");

            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            IEnumerable<DocumentSnapshot> documents = querySnapshot.Documents;

            return DatabaseQueryResponse.Deserialize(querySnapshot);
        }

        public DatabaseQueryService Get()
        {
            _response = Snapshot.ConvertTo<string>();
            return this;
        }

        public string Serialize() {
            return JsonSerializer.Serialize(_response);
        }
    }
}
