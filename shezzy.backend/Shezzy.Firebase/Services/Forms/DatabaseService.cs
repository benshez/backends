using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Shezzy.Shared.Abstractions;

namespace Shezzy.Firebase.Services.Forms
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IFirebaseCredentials _credentials;
        public FirebaseApp DataBase { get; }

        public DatabaseService(
            IFirebaseCredentials credentials)
        {
            _credentials = credentials;

            DataBase =  FirebaseApp.Create(
                new AppOptions() {
                    Credential = GoogleCredential.FromJson(_credentials.Serialize()),
            },"tenants");
        }
    }
}
