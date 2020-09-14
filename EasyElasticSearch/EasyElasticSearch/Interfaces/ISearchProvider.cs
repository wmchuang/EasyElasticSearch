using Nest;

namespace EasyElasticSearch
{
    public interface ISearchProvider
    {
        ISearchResponse<T> SearchPage<T>(ElasticsearchPage<T> page) where T : class, new();
    }
}
