using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shezzy.Firebase.Services.Form
{
    public interface IFirestoreQueryService
    {
        Task<string> GetJson(string tenant);
        Task<Dictionary<string, object>> Get(string tenant);
        Task AddOrUpdate<T>(T entity, CancellationToken ct) where T : IFirebaseEntity;
        Task<T> Get<T>(string id, CancellationToken ct) where T : IFirebaseEntity;
        Task<IReadOnlyCollection<T>> GetAll<T>(string tenantId, CancellationToken ct) where T : IFirebaseEntity;
        Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct) where T : IFirebaseEntity;
        Task<IReadOnlyCollection<T>> GetList<T>(Query query, CancellationToken ct) where T : IFirebaseEntity;
    }
}
