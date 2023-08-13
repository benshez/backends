using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shezzy.Firebase.Services.Forms
{
    public interface IDatabaseQueryService
    {
        Task<string> GetJson(string tenant);
        Task<DatabaseResultModel> Get(string tenant);
    }
}
