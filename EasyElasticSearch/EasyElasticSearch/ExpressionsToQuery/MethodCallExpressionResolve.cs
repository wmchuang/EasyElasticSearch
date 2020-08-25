using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch
{
    public class MethodCallExpressionResolve : BaseResolve
    {
        public MethodCallExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var express = base.Expression as MethodCallExpression;
            if (express == null) return;
            string methodName = express.Method.Name;

            if (methodName == "Contains")
            {
                base.Context.QueryList.Add(new QueryStringQuery());
                NativeExtensionMethod(express);
            }
        }

        private void NativeExtensionMethod(MethodCallExpression express)
        {
            var args = express.Arguments.Cast<Expression>().ToList();
            args.Insert(0, express.Object);

            foreach (var item in args)
            {
                var expItem = item;
                if (item is UnaryExpression)
                {
                    expItem = (item as UnaryExpression).Operand;
                }
                base.Expression = item;
                base.Start();
            }
        }
    }
}
