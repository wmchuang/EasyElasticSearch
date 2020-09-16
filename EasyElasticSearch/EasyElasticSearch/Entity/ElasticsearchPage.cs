using System;
using System.Linq.Expressions;
using Nest;

namespace EasyElasticSearch
{
    public class ElasticsearchPage<T> where T : class, new()
    {
        public ElasticsearchPage(string index = "")
        {
            Index = index.GetIndex<T>();
        }

        /// <summary>
        /// </summary>
        public string Index { get; set; }

        public int PageSize { get; set; } = 0;

        public int PageIndex { get; set; }

        public Expression<Func<T, bool>> Query { get; set; }

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