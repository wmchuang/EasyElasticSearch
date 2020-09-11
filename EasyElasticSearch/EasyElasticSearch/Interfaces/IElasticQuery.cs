using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyElasticSearch
{
    public interface IElasticQuery<T> : IQueryable<T>
    {
    }
}
