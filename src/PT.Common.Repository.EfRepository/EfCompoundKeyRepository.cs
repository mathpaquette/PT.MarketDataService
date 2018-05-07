using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PT.Common.Repository.EfRepository
{
    public class EfCompoundKeyRepository<TEntity, TKey, TKey2> : ICompoundKeyRepository<TEntity, TKey, TKey2> where TEntity : class
    {
        protected DbContext Context;
        protected IDbSet<TEntity> DbSet;

        public EfCompoundKeyRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual TEntity Get(TKey key, TKey2 key2)
        {
            return DbSet.Find(key, key2);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }

    public class EfCompoundKeyRepository<TEntity, TKey, TKey2, TKey3> : ICompoundKeyRepository<TEntity, TKey, TKey2, TKey3> where TEntity : class
    {
        protected DbContext Context;
        protected IDbSet<TEntity> DbSet;

        public EfCompoundKeyRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual TEntity Get(TKey key, TKey2 key2, TKey3 key3)
        {
            return DbSet.Find(key, key2, key3);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}