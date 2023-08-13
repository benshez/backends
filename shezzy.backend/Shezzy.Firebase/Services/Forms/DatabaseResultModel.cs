using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shezzy.Firebase.Services.Forms
{
    public class DatabaseResultModel
    {
        public IEnumerable<Form> Forms { get; set; }
        public string Id { get; set; }
    }

    [FirestoreData]
    public class Form
    {
        [FirestoreProperty]
        public int id { get; set; }

        [FirestoreProperty]
        public DocumentReference formReference { get; set; }
    }




}
