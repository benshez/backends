using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Shezzy.Shared.Abstractions;

namespace Shezzy.Firebase.Services.Form
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IFirebaseCredentials _credentials;
        public FirestoreDb DataBase { get; }

        public DatabaseService(
            IFirebaseCredentials credentials)
        {
            _credentials = credentials;

            DataBase = FirestoreDb.Create(
                _credentials.ProjectId,
                new FirestoreClientBuilder
                {
                    JsonCredentials = _credentials.Serialize()
                }
                .Build());
        }
    }
}
