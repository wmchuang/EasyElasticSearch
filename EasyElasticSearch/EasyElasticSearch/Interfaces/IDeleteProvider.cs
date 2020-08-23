using Nest;
using System;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public interface IDeleteProvider
    {
        DeleteByQueryResponse DeleteByQuery<T>(Expression<Func<T, bool>> expression, string index = "") where T : class, new();
    }
}
