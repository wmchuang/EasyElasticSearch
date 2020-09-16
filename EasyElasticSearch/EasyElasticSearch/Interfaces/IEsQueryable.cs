using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nest;

namespace EasyElasticSearch
{
    public interface IEsQueryable<T>
    {
        public QueryContainer QueryContainer { get; set; }

        IEsQueryable<T> Where(Expression<Func<T, bool>> expression);

        List<T> ToList();

        Task<List<T>> ToListAsync();
        List<T> ToPageList(int pageIndex, int pageSize);
        List<T> ToPageList(int pageIndex, int pageSize, ref long totalNumber);
    }
}