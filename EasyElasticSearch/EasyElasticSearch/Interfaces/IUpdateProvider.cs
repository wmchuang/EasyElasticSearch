using System.Threading.Tasks;
using Nest;

namespace EasyElasticSearch
{
    public interface IUpdateProvider
    {
        Task<IUpdateResponse<T>> UpdateAsync<T>(string key, T entity, string index = "") where T : class;
    }
}
