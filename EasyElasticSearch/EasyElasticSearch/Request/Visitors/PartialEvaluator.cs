using System;
using System.Linq;
using System.Linq.Expressions;

namespace EasyElasticSearch.Request.Visitors
{
    /// <summary>
    /// Determines which part of the tree can be locally
    /// evaluated before execution and substitutes those parts
    /// with constant values obtained from local execution of that part.
    /// </summary>
    static class PartialEvaluator
    {
      //  static readonly Type[] doNotEvaluateMethodsDeclaredOn = { typeof(Enumerable), typeof(Queryable), typeof(ElasticQueryExtensions), typeof(ElasticMethods) };

        public static Expression Evaluate(Expression e)
        {
            var chosenForEvaluation = BranchSelectExpressionVisitor.Select(e, ShouldEvaluate);
            return EvaluatingExpressionVisitor.Evaluate(e, chosenForEvaluation);
        }

        internal static bool ShouldEvaluate(Expression e)
        {
            if (e.NodeType == ExpressionType.Parameter || e.NodeType == ExpressionType.Lambda)
                return false;

            //if (
            //   (e is MethodCallExpression && doNotEvaluateMethodsDeclaredOn.Contains(((MethodCallExpression)e).Method.DeclaringType)))
            //    return false;

            return true;
        }
    }
}
