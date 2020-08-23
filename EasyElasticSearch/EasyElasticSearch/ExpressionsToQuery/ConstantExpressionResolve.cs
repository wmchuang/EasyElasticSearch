using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class ConstantExpressionResolve : BaseResolve
    {
        public ConstantExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = base.Expression as ConstantExpression;
            object value = ExpressionTool.GetValue(expression.Value);
            base.Context.ValueList.Add(value);
        }
    }
}
