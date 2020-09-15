using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest;

namespace EasyElasticSearch
{
    public interface IEsQueryable<T>
    {
        public QueryContainer QueryContainer { get; set; }

        IEsQueryable<T> Where(Expression<Func<T, bool>> expression);

        List<T> ToList();
    }
}