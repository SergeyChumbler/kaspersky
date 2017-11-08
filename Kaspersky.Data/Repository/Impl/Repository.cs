using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaspersky.Data.Common;
using Kaspersky.Data.Domain;
using Kaspersky.Data.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Kaspersky.Data.Repository.Impl
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly KasperskyContext DbContext;

        public Repository(KasperskyContext dbContext)
            => DbContext = dbContext;
        
        public async Task<TEntity> GetSingleAsync(
            ISpecification<TEntity> specification,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            var query = DbContext.Set<TEntity>().Where(specification.IsSatisifiedBy());

            if (include != null)
                query = include(query);

            if (disableTracking)
                query = query.AsNoTracking();


            return await query.FirstOrDefaultAsync(specification.IsSatisifiedBy());
        }

        public async Task<ICollection<TEntity>> GetManyAsync(
            ISpecification<TEntity> specification = null,
            Range range = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {

            var query = DbContext.Set<TEntity>().AsQueryable();

            if (include != null)
                query = include(query);

            if (specification != null)
                query = query.Where(specification.IsSatisifiedBy());

            if (orderBy != null)
                query = orderBy(query);

            if (!Range.IsNullOrEmpty(range))
                query = query.Skip(range.Offset).Take(range.Limit);

            if (disableTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
