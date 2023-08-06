using Google.Cloud.Firestore;

namespace Shezzy.Firebase.Services.Form
{
    public interface IDatabaseQueryService
    {
        DocumentSnapshot Snapshot
        {
            get; set;
        }
    }
}
