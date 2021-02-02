using Nest;

namespace EasyElasticSearch
{
    public sealed class SearchProvider : ISearchProvider
    {
        private readonly IElasticClient _elasticClient;

        public SearchProvider(IEsClientProvider esClientProvider)
        {
            _elasticClient = esClientProvider.Client;
        }

        public IEsQueryable<T> Queryable<T>() where T : class
        {
            return new QueryableProvider<T>(_elasticClient);
        }
    }
}