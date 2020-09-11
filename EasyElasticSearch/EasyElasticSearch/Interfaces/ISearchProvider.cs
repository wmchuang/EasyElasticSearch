using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch
{
    public interface ISearchProvider
    {
        ISearchRequest Queryable<T>(string index = "") where T : class, new();
        ISearchResponse<T> SearchPage<T>(ElasticsearchPage<T> page) where T : class, new();

    
    }
}
