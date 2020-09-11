using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch.Request.Visitors
{
    internal abstract class CriteriaExpressionVisitor : ExpressionVisitor
    {
        protected Expression BooleanMemberAccessBecomesEquals(Expression e)
        {
            e = Visit(e);

            //var c = e as ConstantExpression;
            //if (c?.Value != null)
            //{
            //    if (c.Value.Equals(true))
            //        return new CriteriaExpression(ConstantCriteria.True);
            //    if (c.Value.Equals(false))
            //        return new CriteriaExpression(ConstantCriteria.False);
            //}

            //var wasNegative = e.NodeType == ExpressionType.Not;

            //if (e is UnaryExpression)
            //    e = Visit(((UnaryExpression)e).Operand);

            //if (e is MemberExpression && e.Type == typeof(bool))
            //    return Visit(Expression.Equal(e, Expression.Constant(!wasNegative)));

            return e;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                //case ExpressionType.OrElse:
                //    return VisitOrElse(node);

                //case ExpressionType.AndAlso:
                //    return VisitAndAlso(node);

                case ExpressionType.Equal:
                    return VisitEquals(Visit(node.Left), Visit(node.Right));

                //case ExpressionType.NotEqual:
                //    return VisitNotEqual(Visit(node.Left), Visit(node.Right));

                //case ExpressionType.GreaterThan:
                //    return VisitRange(RangeComparison.GreaterThan, Visit(node.Left), Visit(node.Right));

                //case ExpressionType.GreaterThanOrEqual:
                //    return VisitRange(RangeComparison.GreaterThanOrEqual, Visit(node.Left), Visit(node.Right));

                //case ExpressionType.LessThan:
                //    return VisitRange(RangeComparison.LessThan, Visit(node.Left), Visit(node.Right));

                //case ExpressionType.LessThanOrEqual:
                //    return VisitRange(RangeComparison.LessThanOrEqual, Visit(node.Left), Visit(node.Right));

                default:
                    throw new NotSupportedException($"Binary expression '{node.NodeType}' is not supported");
            }
        }

        private Expression VisitEquals(Expression left, Expression right)
        {
            var booleanEquals = VisitCriteriaEquals(left, right, true);
            if (booleanEquals != null)
                return booleanEquals;

            var cm = ConstantMemberPair.Create(left, right);

            var query = new TermQuery
            {
                Field = cm.MemberExpression.Member.Name.ToCamelCase(),
                Value = cm.ConstantExpression.Value
            };

            return new QueryExpression(query);

            //if (cm != null)
            //    return cm.IsNullTest
            //        ? CreateExists(cm, true)
            //        : new CriteriaExpression(new TermCriteria(, cm.MemberExpression.Member, cm.ConstantExpression.Value));

            throw new NotSupportedException("Equality must be between a Member and a Constant");
        }


        static Expression VisitCriteriaEquals(Expression left, Expression right, bool positiveCondition)
        {
            var criteria = left as CriteriaExpression ?? right as CriteriaExpression;
            var constant = left as ConstantExpression ?? right as ConstantExpression;

            if (criteria == null || constant == null)
                return null;

            if (constant.Value.Equals(positiveCondition))
                return criteria;

            //if (constant.Value.Equals(!positiveCondition))
            //    return new CriteriaExpression(NotCriteria.Create(criteria.Criteria));

            return null;
        }
    }
}
