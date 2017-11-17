using System;
using System.Linq.Expressions;
using Kaspersky.Data.Domain;

namespace Kaspersky.Data.Specification.Impl
{
    public class AdHocSpecification<T> : ISpecification<T> where T : class, IEntity
    {
        private readonly Expression<Func<T, bool>> _expression;

        public AdHocSpecification(Expression<Func<T, bool>> expression) => _expression = expression;

        public Expression<Func<T, bool>> IsSatisifiedBy() => _expression;
    }
}
