using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EasyElasticSearch.Entity.Mapping;
using Nest;

namespace EasyElasticSearch
{
    public class QueryableProvider<T> : IEsQueryable<T> where T : class
    {
        private readonly IElasticClient _client;

        private readonly MappingIndex _mappingIndex;

        private readonly ISearchRequest _request;

        public QueryableProvider(MappingIndex mappingIndex, IElasticClient client)
        {
            _mappingIndex = mappingIndex;
            _request = new SearchRequest(_mappingIndex.IndexName);
            _client = client;
        }

        public QueryContainer QueryContainer { get; set; }

        public IEsQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            _Where(expression);
            return this;
        }

        public virtual List<T> ToList()
        {
            return _ToList<T>();
        }

        private List<TResult> _ToList<TResult>() where TResult : class
        {
            _request.Query = QueryContainer;

            var response = _client.Search<TResult>(_request);

            if (!response.IsValid)
                throw new Exception($"查询失败:{response.OriginalException.Message}");
            return response.Documents.ToList();
        }

        private void _Where(Expression expression)
        {
            QueryContainer = ExpressionsGetQuery.GetQuery(expression, _mappingIndex);
        }
    }
}