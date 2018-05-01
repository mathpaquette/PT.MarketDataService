using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PT.Common.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class
    {
        
    }
}