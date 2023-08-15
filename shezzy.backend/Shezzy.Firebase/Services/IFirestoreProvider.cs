using Google.Cloud.Firestore;

namespace Shezzy.Firebase.Services
{
    public interface IFirestoreProvider
    {
        public FirestoreDb DataBase { get; }
    }
}
