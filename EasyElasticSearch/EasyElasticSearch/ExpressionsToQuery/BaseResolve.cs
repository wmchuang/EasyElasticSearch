using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch
{
    public class BaseResolve
    {
        protected Expression Expression { get; set; }
        protected Expression ExactExpression { get; set; }

        public ExpressionContext Context { get; set; }
        public bool? IsLeft { get; set; }

        //public int ContentIndex { get { return this.Context.Index; } }
        public int Index { get; set; }

        public ExpressionParameter BaseParameter { get; set; }

        private BaseResolve()
        {
        }

        public BaseResolve(ExpressionParameter parameter)
        {
            this.Expression = parameter.CurrentExpression;
            this.Context = parameter.Context;
            this.BaseParameter = parameter;
        }

        public BaseResolve Start()
        {
            Expression expression = this.Expression;
            ExpressionParameter parameter = new ExpressionParameter()
            {
                Context = this.Context,
                CurrentExpression = expression,
                BaseExpression = this.ExactExpression,
                BaseParameter = this.BaseParameter,
            };
            if (expression is LambdaExpression)
            {
                return new LambdaExpressionResolve(parameter);
            }
            //else if (expression is BinaryExpression && expression.NodeType == ExpressionType.Coalesce)
            //{
            //    return new CoalesceResolveItems(parameter);
            //}
            else if (expression is BinaryExpression)
            {
                return new BinaryExpressionResolve(parameter);
            }
            //else if (expression is BlockExpression)
            //{
            //    Check.ThrowNotSupportedException("BlockExpression");
            //}
            //else if (expression is ConditionalExpression)
            //{
            //    return new ConditionalExpressionResolve(parameter);
            //}
            //else if (expression is MethodCallExpression)
            //{
            //    return new MethodCallExpressionResolve(parameter);
            //}
            //else if (expression is MemberExpression && ((MemberExpression)expression).Expression == null)
            //{
            //    return new MemberNoExpressionResolve(parameter);
            //}
            else if (expression is MemberExpression && ((MemberExpression)expression).Expression.NodeType == ExpressionType.Constant)
            {
                return new MemberConstExpressionResolve(parameter);
            }
            //else if (expression is MemberExpression && ((MemberExpression)expression).Expression.NodeType == ExpressionType.New)
            //{
            //    return new MemberNewExpressionResolve(parameter);
            //}
            else if (expression is ConstantExpression)
            {
                return new ConstantExpressionResolve(parameter);
            }
            else if (expression is MemberExpression)
            {
                return new MemberExpressionResolve(parameter);
            }
            //else if (expression is UnaryExpression)
            //{
            //    return new UnaryExpressionResolve(parameter);
            //}
            //else if (expression is MemberInitExpression)
            //{
            //    return new MemberInitExpressionResolve(parameter);
            //}
            //else if (expression is NewExpression)
            //{
            //    return new NewExpressionResolve(parameter);
            //}
            //else if (expression is NewArrayExpression)
            //{
            //    return new NewArrayExpessionResolve(parameter);
            //}
            //else if (expression is ParameterExpression)
            //{
            //    return new TypeParameterExpressionReolve(parameter);
            //}
            //else if (expression != null && expression.NodeType.IsIn(ExpressionType.NewArrayBounds))
            //{
            //    Check.ThrowNotSupportedException("ExpressionType.NewArrayBounds");
            //}
            return null;
        }
    }
}