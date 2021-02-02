using Nest;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyElasticSearch
{
    public interface IDeleteProvider
    {
        Task<DeleteByQueryResponse> DeleteByQuery<T>(Expression<Func<T, bool>> expression, string index = "") where T : class, new();
    }
}
