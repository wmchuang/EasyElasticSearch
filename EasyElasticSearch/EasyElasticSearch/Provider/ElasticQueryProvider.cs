using EasyElasticSearch.Request.Visitors;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch
{
    public sealed class ElasticQueryProvider : IQueryProvider
    {
        public IElasticClient _elasticClient;

        public ElasticQueryProvider(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new ElasticQuery<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            var request = ElasticQueryTranslator.Translate(expression);

            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var request = ElasticQueryTranslator.Translate(expression);
            throw new NotImplementedException();
        }

        public TResult Execute1<TResult>(Expression expression) where TResult : class
        {
            var request = ElasticQueryTranslator.Translate(expression);
            var response = _elasticClient.Search<TResult>(request);
            throw new NotImplementedException();
        }
    }
}
