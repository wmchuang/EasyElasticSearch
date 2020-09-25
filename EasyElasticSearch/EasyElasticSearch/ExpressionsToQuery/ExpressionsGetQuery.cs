using System.Linq.Expressions;
using EasyElasticSearch.Entity.Mapping;
using Nest;

namespace EasyElasticSearch
{
    public class ExpressionsGetQuery
    {
        public static QueryContainer GetQuery(Expression expression, MappingIndex mappingIndex)
        {
            var parameter = new ExpressionParameter {CurrentExpression = expression, Context = new ExpressionContext(mappingIndex)};
            new BaseResolve(parameter).Start();
            return parameter.Context.QueryContainer;
        }
    }
}