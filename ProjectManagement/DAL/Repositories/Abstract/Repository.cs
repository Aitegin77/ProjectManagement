using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstract
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected AppDbContext AppDbContext { get; init; }

        public Repository(AppDbContext libraryDbContext) =>
            AppDbContext = libraryDbContext;

        protected DbSet<TEntity> Set => AppDbContext.Set<TEntity>();

        public IQueryable<TEntity> GetSet() => Set;

        public void Update(TEntity entity) => Set.Update(entity);

        public void Delete(TEntity entity) => Set.Remove(entity);

        public async Task AddAsync(TEntity entity) => await Set.AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAll() => await Set.ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id) => await Set.FindAsync(id);

        public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter) =>
            Set.Where(filter).AsNoTracking();

        public async Task SaveChangesAsync() => await AppDbContext.SaveChangesAsync();

        public async Task<bool> IsAnyAsync(Expression<Func<TEntity, bool>> filter) =>
            await Set.AnyAsync(filter);

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter) =>
            await Set.FirstOrDefaultAsync(filter);
    }
}
