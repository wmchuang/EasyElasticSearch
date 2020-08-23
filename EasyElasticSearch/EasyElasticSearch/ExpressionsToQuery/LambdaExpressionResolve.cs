using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class LambdaExpressionResolve : BaseResolve
    {
        public LambdaExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            LambdaExpression lambda = base.Expression as LambdaExpression;
            var expression = lambda.Body;
            base.Expression = expression;
            base.Start();
        }
    }
}
