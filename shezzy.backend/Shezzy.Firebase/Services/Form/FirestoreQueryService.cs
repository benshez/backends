using Google.Cloud.Firestore;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Shezzy.Firebase.Services.Form
{
    public class FirestoreQueryService : IFirestoreQueryService
    {
        private readonly IFirestoreProvider _service;
        private string _response;
        public DocumentSnapshot Snapshot { get; set; }

        public FirestoreQueryService(
            IFirestoreProvider service)
        {
            _service = service;
        }

        async public Task<DocumentSnapshot> CreateSnapshot(string tenant)
        {
            CollectionReference collection = _service.DataBase.Collection(tenant);
            QuerySnapshot snapshot = await collection.GetSnapshotAsync();
            DocumentReference docRef = _service.DataBase.Collection(tenant).Document("forms");
            //DocumentSnapshot document = snapshot.Documents.FirstOrDefault();
            DocumentSnapshot document = await docRef.GetSnapshotAsync();
            Home home = document.ConvertTo<Home>();
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

        public async Task AddOrUpdate<T>(T entity, CancellationToken ct) where T : IFirebaseEntity
        {
            var document = _service.DataBase.Collection(typeof(T).Name).Document(entity.Id);

            await document.SetAsync(entity, cancellationToken: ct);
        }

        public async Task<T> Get<T>(string id, CancellationToken ct) where T : IFirebaseEntity
        {
            var document = _service.DataBase.Collection(typeof(T).Name).Document(id);
            var snapshot = await document.GetSnapshotAsync(ct);

            return snapshot.ConvertTo<T>();
        }

        public async Task<IReadOnlyCollection<T>> GetAll<T>(string id, CancellationToken ct) where T : IFirebaseEntity
        {
            var collection = _service.DataBase.Collection(typeof(T).Name);
            var snapshot = await collection.GetSnapshotAsync(ct);
       
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct) where T : IFirebaseEntity
        {
            return await GetList<T>(_service.DataBase.Collection(typeof(T).Name).WhereEqualTo(fieldPath, value), ct);
        }

        public async Task<IReadOnlyCollection<T>> GetList<T>(Query query, CancellationToken ct) where T : IFirebaseEntity
        {
            var snapshot = await query.GetSnapshotAsync(ct);

            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }
    }
}
