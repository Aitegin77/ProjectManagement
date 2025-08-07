using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Asynchronously returns entity by their identifier code.
        /// </summary>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Returns queryable set of entities by filter.
        /// </summary>
        IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Asynchronously returns all entities from context.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Asynchronously adds a new entity to the context.
        /// </summary>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates existing entity in context.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes entity from context.
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// Returns queryable set of entities.
        /// </summary>
        IQueryable<TEntity> GetSet();

        /// <summary>
        /// Asynchronously checks if any entities match the filter.
        /// </summary>
        Task<bool> IsAnyAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Asynchronously returns first entity by filter
        /// </summary>
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Asynchronously saves all changes in the context;
        /// </summary>
        Task SaveChangesAsync();
    }
}
