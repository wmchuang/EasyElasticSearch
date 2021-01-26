using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class ConstantExpressionResolve : BaseResolve
    {
        public ConstantExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            if (!(Expression is ConstantExpression expression)) return;
            var value = ExpressionTool.GetValue(expression.Value);
            Context.LastValue = value;
        }
    }
}