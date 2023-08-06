using Google.Cloud.Firestore;

namespace Shezzy.Firebase.Services.Form
{
    public interface IDatabaseService
    {
        public FirestoreDb DataBase { get; }
    }
}
