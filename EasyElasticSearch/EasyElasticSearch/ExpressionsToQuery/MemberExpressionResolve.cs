using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class MemberExpressionResolve : BaseResolve
    {
        public MemberExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var leftexp = Expression as MemberExpression;
            var memberName = leftexp.Member.Name;

            Context.LastFiled = memberName;
        }

        public ExpressionParameter Parameter { get; set; }
    }
}