using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch.Request.Visitors
{
    public class BranchSelectExpressionVisitor : ExpressionVisitor
    {
        readonly HashSet<Expression> matches = new HashSet<Expression>();
        readonly Func<Expression, bool> predicate;
        bool decision;

        BranchSelectExpressionVisitor(Func<Expression, bool> predicate)
        {
            this.predicate = predicate;
        }

        internal static HashSet<Expression> Select(Expression e, Func<Expression, bool> predicate)
        {
            var visitor = new BranchSelectExpressionVisitor(predicate);
            visitor.Visit(e);
            return visitor.matches;
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            Visit(node.NewExpression);
            Visit(node.Bindings, VisitMemberBinding);

            if (matches.Contains(node.NewExpression) && node.Bindings.Count > 0)
            {
                // We should never consider the newExpression in isolation from the bindings
                matches.Remove(node.NewExpression);
            }

            return node;
        }

        public override Expression Visit(Expression node)
        {
            if (node == null)
                return null;

            var priorDecision = decision;
            decision = false;
            base.Visit(node);

            if (!decision)
            {
                if (predicate(node))
                    matches.Add(node);
                else
                    decision = true;
            }

            decision |= priorDecision;
            return node;
        }
    }
}
