using Google.Cloud.Firestore;
using Shezzy.Shared.Abstractions.Credentials;

namespace Shezzy.Shared.Abstractions
{
    public interface IJsonService
  {
    string Serialize(DocumentSnapshot documentSnapshot);
    string Serialize(FirebaseCredentialsModel json);
    FirestoreEvents Deserialize<FirestoreEvents>(DocumentSnapshot documentSnapshot);
    T Deserialize<T>(string json);
  }
}