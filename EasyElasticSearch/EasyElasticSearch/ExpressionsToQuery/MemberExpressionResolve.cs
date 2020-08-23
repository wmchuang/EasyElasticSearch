using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class MemberExpressionResolve : BaseResolve
    {
        public ExpressionParameter Parameter { get; set; }

        public MemberExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var leftexp = base.Expression as MemberExpression;
            var memberName = leftexp.Member.Name;
            base.Context.MemberNameList.Add(memberName);
        }
    }
}
