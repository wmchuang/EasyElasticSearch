using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EasyElasticSearch.Request.Visitors
{
    class ElasticQueryTranslator : CriteriaExpressionVisitor
    {
        private SearchRequest searchRequest;


        internal static ISearchRequest Translate(Expression e)
        {
            var requesrt = new ElasticQueryTranslator().Translate1(e);

            return requesrt;
        }

        ISearchRequest Translate1(Expression e)
        {
            var type = FindSourceType(e).Name.ToFirstLower();
            searchRequest = new SearchRequest(type);
            var evaluated = PartialEvaluator.Evaluate(e);
            CompleteHitTranslation(evaluated);

            //  return EvaluatingExpressionVisitor.Evaluate(e, chosenForEvaluation);
            // return new ElasticQueryTranslator(FindSourceType(e)).Translate(e);

            return searchRequest;
        }

        static Type FindSourceType(Expression e)
        {
            var sourceQuery = QuerySourceExpressionVisitor.FindSource(e);
            if (sourceQuery == null)
                throw new NotSupportedException("Unable to identify an IQueryable source for this query.");
            return sourceQuery.ElementType;
        }

        void CompleteHitTranslation(Expression evaluated)
        {
            Visit(evaluated);
            //searchRequest.DocumentType = Mapping.GetDocumentType(SourceType);

            //if (materializer == null)
            //    materializer = new ListHitsElasticMaterializer(itemProjector ?? DefaultItemProjector, finalItemType ?? SourceType);
            //else if (materializer is ChainMaterializer && ((ChainMaterializer)materializer).Next == null)
            //    ((ChainMaterializer)materializer).Next = new ListHitsElasticMaterializer(itemProjector ?? DefaultItemProjector, finalItemType ?? SourceType);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable))
                return VisitQueryableMethodCall(node);

            //if (node.Method.DeclaringType == typeof(ElasticQueryExtensions))
            //    return VisitElasticQueryExtensionsMethodCall(node);

            //if (node.Method.DeclaringType == typeof(ElasticMethods))
            //    return VisitElasticMethodsMethodCall(node);

            return base.VisitMethodCall(node);
        }

        internal Expression VisitQueryableMethodCall(MethodCallExpression m)
        {
            switch (m.Method.Name)
            {
                case "Select":
                    if (m.Arguments.Count == 2)
                        return VisitSelect(m.Arguments[0], m.Arguments[1]);
                    throw GetOverloadUnsupportedException(m.Method);

                //case "First":
                //case "FirstOrDefault":
                //case "Single":
                //case "SingleOrDefault":
                //    if (m.Arguments.Count == 1)
                //        return VisitFirstOrSingle(m.Arguments[0], null, m.Method.Name);
                //    if (m.Arguments.Count == 2)
                //        return VisitFirstOrSingle(m.Arguments[0], m.Arguments[1], m.Method.Name);
                //    throw GetOverloadUnsupportedException(m.Method);

                case "Where":
                    if (m.Arguments.Count == 2)
                        return VisitWhere(m.Arguments[0], m.Arguments[1]);
                    throw GetOverloadUnsupportedException(m.Method);

                    //case "Skip":
                    //    if (m.Arguments.Count == 2)
                    //        return VisitSkip(m.Arguments[0], m.Arguments[1]);
                    //    throw GetOverloadUnsupportedException(m.Method);

                    //case "Take":
                    //    if (m.Arguments.Count == 2)
                    //        return VisitTake(m.Arguments[0], m.Arguments[1]);
                    //    throw GetOverloadUnsupportedException(m.Method);

                    //case "OrderBy":
                    //case "OrderByDescending":
                    //    if (m.Arguments.Count == 2)
                    //        return VisitOrderBy(m.Arguments[0], m.Arguments[1], m.Method.Name == "OrderBy");
                    //    throw GetOverloadUnsupportedException(m.Method);

                    //case "ThenBy":
                    //case "ThenByDescending":
                    //    if (m.Arguments.Count == 2)
                    //        return VisitOrderBy(m.Arguments[0], m.Arguments[1], m.Method.Name == "ThenBy");
                    //    throw GetOverloadUnsupportedException(m.Method);

                    //case "Count":
                    //case "LongCount":
                    //    if (m.Arguments.Count == 1)
                    //        return VisitCount(m.Arguments[0], null, m.Method.ReturnType);
                    //    if (m.Arguments.Count == 2)
                    //        return VisitCount(m.Arguments[0], m.Arguments[1], m.Method.ReturnType);
                    //    throw GetOverloadUnsupportedException(m.Method);

                    //case "Any":
                    //    if (m.Arguments.Count == 1)
                    //        return VisitAny(m.Arguments[0], null);
                    //    if (m.Arguments.Count == 2)
                    //        return VisitAny(m.Arguments[0], m.Arguments[1]);
                    //    throw GetOverloadUnsupportedException(m.Method);
            }

            throw new NotSupportedException($"Queryable.{m.Method.Name} method is not supported");
        }

        static NotSupportedException GetOverloadUnsupportedException(MethodInfo methodInfo)
        {
            return new NotSupportedException(
                $"Queryable.{methodInfo.ToString().Substring(methodInfo.ReturnType.ToString().Length + 1)} method overload is not supported");
        }

        Expression VisitWhere(Expression source, Expression lambdaPredicate)
        {
            var lambda = lambdaPredicate.GetLambda();

            var criteriaExpression = lambda.Body as QueryExpression ?? BooleanMemberAccessBecomesEquals(lambda.Body) as QueryExpression;

            if (criteriaExpression == null)
                throw new NotSupportedException($"Where expression '{lambda.Body}' could not be translated");

            searchRequest.Query = criteriaExpression.Query;

            return Visit(source);
        }

        Expression VisitSelect(Expression source, Expression selectExpression)
        {
            var lambda = selectExpression.GetLambda();

            if (lambda.Parameters.Count != 1)
                throw new NotSupportedException("Select method with T parameter is supported, additional parameters like index are not");

            var selectBody = lambda.Body;

            //if (selectBody is MemberExpression)
            //    RebindPropertiesAndElasticFields(selectBody);

            //if (selectBody is NewExpression)
            //    RebindSelectBody(selectBody, ((NewExpression)selectBody).Arguments, lambda.Parameters);

            //if (selectBody is MethodCallExpression)
            //    RebindSelectBody(selectBody, ((MethodCallExpression)selectBody).Arguments, lambda.Parameters);

            //if (selectBody is MemberInitExpression)
            //    RebindPropertiesAndElasticFields(selectBody);

            //finalItemType = selectBody.Type;

            return Visit(source);
        }



    }
}
