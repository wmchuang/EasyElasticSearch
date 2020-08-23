using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class BinaryExpressionResolve : BaseResolve
    {
        public BinaryExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = this.Expression as BinaryExpression;
            var operatorValue = ExpressionTool.GetOperator(expression.NodeType);
            if (operatorValue != null)
                base.Context.QueryList.Add(operatorValue);

            if (ExpressionTool.IsOperator(expression.NodeType))
                base.Context.OperatorList.Add(expression.NodeType);

            var leftExpression = expression.Left;
            var rightExpression = expression.Right;

            base.Expression = leftExpression;
            base.IsLeft = true;
            base.Start();

            base.IsLeft = false;
            base.Expression = rightExpression;
            base.Start();
            base.IsLeft = null;
        }
    }
}
