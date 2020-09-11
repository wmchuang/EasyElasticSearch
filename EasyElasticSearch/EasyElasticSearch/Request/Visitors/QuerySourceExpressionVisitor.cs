using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch
{
    class QuerySourceExpressionVisitor : ExpressionVisitor
    {
        IQueryable sourceQueryable;

        QuerySourceExpressionVisitor()
        {
        }

        public static IQueryable FindSource(Expression e)
        {
            var visitor = new QuerySourceExpressionVisitor();
            visitor.Visit(e);
            return visitor.sourceQueryable;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is IQueryable)
                sourceQueryable = ((IQueryable)node.Value);

            return node;
        }
    }
}
