using System;
using System.Linq.Expressions;
using Kaspersky.Data.Domain;

namespace Kaspersky.Data.Specification
{
    public interface ISpecification<T> where T : class, IEntity
    {
        Expression<Func<T, bool>> IsSatisifiedBy();
    }
}
