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
        private readonly WalletPayDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected RepositoryDbContextBase(WalletPayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(ICollection<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public Task<int> Count()
        {
            return _context.Set<TEntity>().CountAsync();
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> FindAllByWhereAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().Where(match).ToListAsync();
        }

        public async Task<List<TEntity>> FindAllByWhereOrderedAscendingAsync(
            Expression<Func<TEntity, bool>> match,
            Expression<Func<TEntity, object>> orderBy)
        {
            return await _context.Set<TEntity>().Where(match).OrderBy(orderBy).ToListAsync();
        }

        public async Task<List<TEntity>> FindAllByWhereOrderedDescendingAsync(
            Expression<Func<TEntity, bool>> match,
            Expression<Func<TEntity, object>> orderBy)
        {
            return await _context.Set<TEntity>().Where(match).OrderByDescending(orderBy).ToListAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().AnyAsync(match);
        }

        public virtual Task<TEntity> GetFirstWhereAsync(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(match);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);
            }

            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            await _context.SaveChangesAsync();
            return entityToUpdate;
        }

        public virtual async Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities)
        {
            var detachedEntities = new List<TEntity>();
            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            foreach (var entity in entities)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    detachedEntities.Add(entity);
                }

                _context.Entry(entity).State = EntityState.Modified;
            }

            _dbSet.AttachRange(detachedEntities);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.AutoDetectChangesEnabled = true;

            return entities;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IList<TEntity>> InsertRangeAsync(IList<TEntity> entities, bool saveChanges = true)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);

            if (saveChanges)
            {
                await _context.SaveChangesAsync();
            }

            return entities;
        }
    }
}
