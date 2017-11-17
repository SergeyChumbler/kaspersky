using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaspersky.Data.Common;
using Kaspersky.Data.Domain;
using Kaspersky.Data.Specification;
using Microsoft.EntityFrameworkCore.Query;

namespace Kaspersky.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<ICollection<TEntity>> GetManyAsync(
            ISpecification<TEntity> specification = null,
            Range range = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        Task<TEntity> GetSingleAsync(
            ISpecification<TEntity> specification,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }

}
