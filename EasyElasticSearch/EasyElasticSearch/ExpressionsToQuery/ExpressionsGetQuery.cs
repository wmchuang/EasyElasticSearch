using Nest;
using System;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class ExpressionsGetQuery
    {
        public static QueryContainer GetQuery<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            var parameter = new ExpressionParameter() { CurrentExpression = expression, Context = new ExpressionContext() };
            new BaseResolve(parameter).Start();
            return parameter.Context.GetQuery<T>();
        }
    }
}
