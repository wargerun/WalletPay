﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using WalletPay.Data.Repositories;

namespace WalletPay.Core.Tests.Repositories
{
    public abstract class CollectionRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly List<TEntity> _entities;

        public CollectionRepositoryBase(IEnumerable<TEntity> entities)
        {
            _entities = entities.ToList();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> match)
        {
            Func<TEntity, bool> func = match.Compile();
            var result = _entities.Any(func);
            return Task.FromResult(result);
        }

        public Task<int> Count(Expression<Func<TEntity, bool>> match)
        {
            Func<TEntity, bool> func = match.Compile();
            return Task.FromResult(_entities.Count(func));
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public Task<List<TEntity>> FindAllByWhereOrderedAscendingAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy)
        {
            Func<TEntity, bool> func = match.Compile();
            Func<TEntity, object> funcOrderBy = orderBy.Compile();
            List<TEntity> entities = _entities.Where(func).OrderBy(funcOrderBy).ToList();
            return Task.FromResult(entities);
        }

        public Task<List<TEntity>> FindAllByWhereOrderedDescendingAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy)
        {
            Func<TEntity, bool> func = match.Compile();
            Func<TEntity, object> funcOrderBy = orderBy.Compile();
            List<TEntity> entities = _entities.Where(func).OrderByDescending(funcOrderBy).ToList();
            return Task.FromResult(entities);
        }

        public Task<TEntity> GetFirstWhereAsync(Expression<Func<TEntity, bool>> match)
        {
            Func<TEntity, bool> func = match.Compile();
            TEntity entity = _entities.FirstOrDefault(func);
            return Task.FromResult(entity);
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            _entities.Add(entity);
            return Task.FromResult(entity);
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
