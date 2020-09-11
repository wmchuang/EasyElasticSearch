using Nest;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public interface IEasyEsContext
    {
        IQueryable<T> Query<T>() where T : class;
    }
}
