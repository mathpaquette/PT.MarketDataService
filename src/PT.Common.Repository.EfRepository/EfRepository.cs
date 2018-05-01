using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PT.Common.Repository.EfRepository
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected DbContext Context;
        protected DbSet DbSet;

        public EfRepository(DbContext context)
        {
            Context = context;
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(TKey id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }

    public class EfRepository<TEntity> : EfRepository<TEntity, int>, IRepository<TEntity> where TEntity : class
    {
        public EfRepository(DbContext context) : base(context)
        {
        }
    }
}