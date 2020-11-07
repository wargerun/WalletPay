using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using WalletPay.Data.Context;

namespace WalletPay.Data.Repositories
{
    public abstract class RepositoryDbContextBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly WalletPayDbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        protected RepositoryDbContextBase(WalletPayDbContext context)
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }

        public Task<int> Count(Expression<Func<TEntity, bool>> match)
        {
            return Context.Set<TEntity>().CountAsync(match);
        }

        public async Task<List<TEntity>> FindAllByWhereOrderedAscendingAsync(
            Expression<Func<TEntity, bool>> match,
            Expression<Func<TEntity, object>> orderBy)
        {
            return await Context.Set<TEntity>().Where(match).OrderBy(orderBy).ToListAsync();
        }

        public async Task<List<TEntity>> FindAllByWhereOrderedDescendingAsync(
            Expression<Func<TEntity, bool>> match,
            Expression<Func<TEntity, object>> orderBy)
        {
            return await Context.Set<TEntity>().Where(match).OrderByDescending(orderBy).ToListAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> match)
        {
            return Context.Set<TEntity>().AnyAsync(match);
        }

        public virtual Task<TEntity> GetFirstWhereAsync(Expression<Func<TEntity, bool>> match)
        {
            return Context.Set<TEntity>().FirstOrDefaultAsync(match);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            if (Context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);
            }

            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            await Context.SaveChangesAsync();
            return entityToUpdate;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
    }
}
