using Nest;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    internal class QueryExpression : Expression
    {
        public QueryBase Query { get; }

        public QueryExpression(QueryBase query)
        {
            Query = query;
        }
    }
}
