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
        private readonly IDatabaseService _service;
        private string _response;
        public DocumentSnapshot Snapshot { get; set; }

        public DatabaseQueryService(
            IDatabaseService service)
        {
            _service = service;
        }

        async public Task<DocumentSnapshot> CreateSnapshot(string tenant)
        {
            CollectionReference collection = _service.DataBase.Collection(tenant);
            QuerySnapshot snapshot = await collection.GetSnapshotAsync();

            DocumentSnapshot document = snapshot.Documents.FirstOrDefault();
  
            return document;
            //IEnumerable<DocumentSnapshot> documents = querySnapshot.Documents(;

            // return DatabaseQueryResponse.Deserialize(querySnapshot);
        }

        public Task<DatabaseResultModel> Get()
        {
            _response = Snapshot.ConvertTo<string>();
            return null;
        }

        public string Serialize() {
            return JsonSerializer.Serialize(_response);
        }

        public Task<string> GetJson(string tenant)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Dictionary<string, object>> Get(string tenant)
        {
            DocumentSnapshot document =  await CreateSnapshot(tenant);
            return (Dictionary<string,object>)document.ToDictionary();
        }
    }
}
