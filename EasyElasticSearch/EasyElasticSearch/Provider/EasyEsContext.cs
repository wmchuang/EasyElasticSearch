using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class EasyEsContext : IEasyEsContext
    {
        public IElasticClient _elasticClient;

        public EasyEsContext(IEsClientProvider esClientProvider)
        {
            _elasticClient = esClientProvider.Client;
        }

        public virtual IQueryable<T> Query<T>() where T : class
        {
            return new ElasticQuery<T>(new ElasticQueryProvider(_elasticClient));
        }
    }
}
