using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class ConstantExpressionResolve : BaseResolve
    {
        public ConstantExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = Expression as ConstantExpression;
            var value = ExpressionTool.GetValue(expression.Value);
            Context.LastValue = value;
            // base.Context.ValueList.Add(value);
        }
    }
}