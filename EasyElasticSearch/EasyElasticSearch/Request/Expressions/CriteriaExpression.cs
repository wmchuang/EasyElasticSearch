using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyElasticSearch
{
    /// <summary>
    /// An expression tree node that represents criteria.
    /// </summary>
    internal class CriteriaExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaExpression"/> class.
        /// </summary>
        /// <param name="criteria"><see cref="ICriteria" /> to represent with this expression.</param>
        public CriteriaExpression(ICriteria criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// <see cref="ICriteria" /> that is represented by this expression.
        /// </summary>
        public ICriteria Criteria { get; }

        /// <inheritdoc/>
        public override ExpressionType NodeType => (ExpressionType)10000;

        /// <inheritdoc/>
        public override Type Type => typeof(bool);

        /// <inheritdoc/>
        public override bool CanReduce => false;

        /// <inheritdoc/>
        public override string ToString()
        {
            return Criteria.ToString();
        }
    }
}
