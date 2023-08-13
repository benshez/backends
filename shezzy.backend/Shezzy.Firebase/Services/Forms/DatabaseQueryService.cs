using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shezzy.Firebase.Services.Forms
{
    public class DatabaseQueryService : IDatabaseQueryService
    {
        private readonly IDatabaseService _service;

        public DatabaseQueryService(
            IDatabaseService service)
        {
            _service = service;
        }

        private async Task<Dictionary<string, object>> CreateSnapshot(string tenant)
        {
            //CollectionReference collection = _service.DataBase..Collection(tenant);
            //QuerySnapshot snapshot = await collection.GetSnapshotAsync();

            //CollectionReference snapshot = await _service.DataBase
            //  .Collection(tenant);
            //.Document("forms")
            //.GetSnapshotAsync();
            //var firebase = new FirebaseClient("https://dinosaur-facts.firebaseio.com/");

            //return await _service.DataBase
            //    .Child("tenants")
            //    .AsObservable<Dictionary<string,object>>()
            //    .Subscribe(_ => Console.WriteLine("dasda" + _));
            return null;
        }

        async public Task<string> GetJson(string tenant)
        {
            var response = await CreateSnapshot(tenant);
            return null;
            //return DatabaseQueryResponse.Serialize(response);
        }
        async public Task<Form> Get(string tenant)
        {
            var response = await CreateSnapshot(tenant);

            //DocumentReference docRef = _service.DataBase.Collection("cities").Document("LA");
            Dictionary<string, object> city = new Dictionary<string, object>
            {
                { "name", "Los Angeles" },
                { "state", "CA" },
                { "country", "USA" }
            };
            //await docRef.SetAsync(city);

            //return DatabaseQueryResponse.Deserialize(response);
            return null;
        }
    }
}
