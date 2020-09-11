
using System.Collections.Generic;
using System.Linq.Expressions;


namespace EasyElasticSearch.Request.Visitors
{
    /// <summary>
    /// Visits the expression tree and for any node in its list
    /// replaces that expression with a constant expression
    /// that resulted from its compilation and invocation.
    /// </summary>
    class EvaluatingExpressionVisitor : ExpressionVisitor
    {
        readonly HashSet<Expression> chosenForEvaluation;

        EvaluatingExpressionVisitor(HashSet<Expression> chosenForEvaluation)
        {
            this.chosenForEvaluation = chosenForEvaluation;
        }

        internal static Expression Evaluate(Expression e, HashSet<Expression> chosenForEvaluation)
        {
            return new EvaluatingExpressionVisitor(chosenForEvaluation).Visit(e);
        }

        public override Expression Visit(Expression node)
        {
            if (node == null || node.NodeType == ExpressionType.Constant)
                return node;

            return chosenForEvaluation.Contains(node)
                ? Expression.Constant(Expression.Lambda(node).Compile().DynamicInvoke(null), node.Type)
                : base.Visit(node);
        }
    }
}
