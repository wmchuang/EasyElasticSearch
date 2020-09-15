using System;
using EasyElasticSearch.Entity.Mapping;
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
            var mapping = InitMappingInfo<T>();
            return new QueryableProvider<T>(mapping, _elasticClient);
        }

        private MappingIndex InitMappingInfo<T>()
        {
            return InitMappingInfo(typeof(T));
        }

        private static MappingIndex InitMappingInfo(Type type)
        {
            var mapping = new MappingIndex {Type = type, IndexName = type.Name};
            foreach (var property in type.GetProperties())
                mapping.Columns.Add(new MappingColumn
                {
                    PropertyInfo = property.PropertyType,
                    PropertyName = property.Name,
                    SearchName = "userName"
                });
            return mapping;
        }
    }
}