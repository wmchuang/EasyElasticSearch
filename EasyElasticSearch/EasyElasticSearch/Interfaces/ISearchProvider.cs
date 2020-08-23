using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyElasticSearch
{
    public interface ISearchProvider
    {
        ISearchResponse<T> SearchPage<T>(ElasticsearchPage<T> page) where T : class, new();
    }
}
