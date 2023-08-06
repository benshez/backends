using Google.Cloud.Firestore;

namespace Shezzy.Shared.Abstractions
{
  public interface IJsonService
  {
    string Serialize(DocumentSnapshot documentSnapshot);
    string Serialize(CredentialsModel json);
    FirestoreEvents Deserialize<FirestoreEvents>(DocumentSnapshot documentSnapshot);
    T Deserialize<T>(string json);
  }
}