using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class MemberConstExpressionResolve : BaseResolve
    {
        public MemberConstExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = Expression as MemberExpression;
            var value = ExpressionTool.GetMemberValue(expression.Member, expression);
            Context.LastValue = value;
            // base.Context.ValueList.Add(value);
        }
    }
}