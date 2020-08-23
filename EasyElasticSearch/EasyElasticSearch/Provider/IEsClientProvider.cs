using Nest;

namespace EasyElasticSearch
{
    public interface IEsClientProvider
    {
        ElasticClient Client { get; }
    }
}
