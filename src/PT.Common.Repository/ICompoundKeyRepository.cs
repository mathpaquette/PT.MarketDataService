using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PT.Common.Repository
{
    public interface ICompoundKeyRepository<TEntity, TKey, TKey2> where TEntity : class
    {
        TEntity Get(TKey key, TKey2 key2);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }

    public interface ICompoundKeyRepository<TEntity, TKey, TKey2, TKey3> where TEntity : class
    {
        TEntity Get(TKey key, TKey2 key2, TKey3 key3);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}