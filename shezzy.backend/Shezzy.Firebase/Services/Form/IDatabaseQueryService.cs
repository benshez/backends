using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shezzy.Firebase.Services.Form
{
    public interface IDatabaseQueryService
    {
        Task<string> GetJson(string tenant);
        Task<Dictionary<string, object>> Get(string tenant);
    }
}
