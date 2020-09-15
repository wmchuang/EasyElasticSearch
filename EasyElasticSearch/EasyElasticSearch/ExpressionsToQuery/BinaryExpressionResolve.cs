using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class BinaryExpressionResolve : BaseResolve
    {
        public BinaryExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = Expression as BinaryExpression;
            var operatorValue = ExpressionTool.GetOperator(expression.NodeType);

            Context.LastQueryBase = operatorValue;
            // if (operatorValue != null)
            //     base.Context.QueryList.Add(operatorValue);
            //
            // if (ExpressionTool.IsOperator(expression.NodeType))
            //     base.Context.OperatorList.Add(expression.NodeType);

            var leftExpression = expression.Left;
            var rightExpression = expression.Right;

            Expression = leftExpression;
            IsLeft = true;
            Start();

            IsLeft = false;
            Expression = rightExpression;
            Start();
            IsLeft = null;

            Context.SetQuery();
        }
    }
}