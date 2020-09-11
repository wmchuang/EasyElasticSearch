using Nest;
using System;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class ElasticsearchPage<T> where T : class, new()
    {
        public string Index { get; set; }

        public int PageSize { get; set; } = 0;

        public int PageIndex { get; set; }

        public Expression<Func<T, bool>> Query { get; set; }

        public ElasticsearchPage(string index = "")
        {
            Index = index.GetIndex<T>();
        }

        public ISearchRequest InitSearchRequest()
        {
            return new SearchRequest(Index)
            {
                From = ((PageIndex < 1 ? 1 : PageIndex) - 1) * PageSize,
                Size = PageSize
            };
        }
    }
}
