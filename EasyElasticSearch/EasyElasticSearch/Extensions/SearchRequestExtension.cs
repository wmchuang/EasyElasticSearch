using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch
{
    //public static class SearchRequestExtension
    //{
    //    public static ISearchRequest Where<T>(this ISearchRequest request, Expression<Func<T, bool>> expression) where T : class, new()
    //    {
    //        request.Query = ExpressionsGetQuery.GetQuery<T>(expression);
    //        return request;
    //    }

    //    public static ISearchRequest OrderBy<T>(this ISearchRequest request, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc) where T : class, new()
    //    {
    //        request.Sort = new List<ISort> {
    //            new FieldSort{
    //                Field = "name",
    //                Order = SortOrder.Ascending
    //            }
    //        };
    //        return request;
    //    }
    //}
}
