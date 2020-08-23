using Nest;

namespace EasyElasticSearch
{
    public interface IUpdateProvider
    {
        IUpdateResponse<T> Update<T>(string key, T entity, string index = "") where T : class;
    }
}
