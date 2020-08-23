using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class MemberConstExpressionResolve : BaseResolve
    {
        public MemberConstExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = base.Expression as MemberExpression;
            object value = ExpressionTool.GetMemberValue(expression.Member, expression);
            base.Context.ValueList.Add(value);
        }
    }
}
