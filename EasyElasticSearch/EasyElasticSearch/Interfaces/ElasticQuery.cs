using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class ElasticQuery<T> : IElasticQuery<T> 
    {
        private readonly ElasticQueryProvider provider;

        public Expression Expression { get; }

        public Type ElementType => typeof(T);

        public IQueryProvider Provider => provider;

        public ElasticQuery(ElasticQueryProvider provider)
        {
            this.provider = provider;
            Expression = Expression.Constant(this);
        }

        public ElasticQuery(ElasticQueryProvider provider, Expression expression)
        {
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
                throw new ArgumentOutOfRangeException(nameof(expression));

            this.provider = provider;
            Expression = expression;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)provider.Execute(Expression)).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)provider.Execute<T>(Expression)).GetEnumerator();
        }
    }
}
