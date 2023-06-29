using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Domain.Common;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Persistence.Repository
{
    public class Repository<TContext> : IRepository
        where TContext : DbContext
    {
        protected readonly TContext _dbContext;

        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetEntityStateUnchanged<TEntity>(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;
        }

        public virtual int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #region Gets

        public virtual IQueryable<T> Query<T>(Func<IIncludable<T>, IIncludable> includes = null) where T : class
        {
            var dbSet = _dbContext.Set<T>() as IQueryable<T>;

            if (includes != null)
            {
                dbSet = dbSet.IncludeMultiple(includes);
            }

            return dbSet;
        }

        public T Get<T>(params object[] id) where T : class
        {
            return GetById<T>(id);
        }

        public T Get<T>(Expression<Func<T, bool>> selector, Func<IIncludable<T>, IIncludable> includes = null) where T : class
        {
            return First(selector, includes);
        }

        public virtual T GetById<T>(params object[] id) where T : class
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetByIds<T>(params object[] ids) where T : class
        {
            return ids.Select(id => GetById<T>(id))
                      .Where(x => x != null)
                      .AsQueryable();
        }

        public virtual T First<T>(Expression<Func<T, bool>> predicate = null,
                                  Func<IIncludable<T>, IIncludable> includes = null) where T : class
        {
            var dbSet = _dbContext.Set<T>() as IQueryable<T>;

            if (includes != null)
            {
                dbSet = dbSet.IncludeMultiple(includes);
            }

            return predicate == null
                       ? dbSet.FirstOrDefault()
                       : dbSet.FirstOrDefault(predicate);
        }

        public virtual Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate = null,
                                  Func<IIncludable<T>, IIncludable> includes = null) where T : class
        {
            var dbSet = _dbContext.Set<T>() as IQueryable<T>;

            if (includes != null)
            {
                dbSet = dbSet.IncludeMultiple(includes);
            }

            return predicate == null
                       ? dbSet.FirstOrDefaultAsync()
                       : dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual T UntrackFirst<T>(Expression<Func<T, bool>> predicate = null,
                                  Func<IIncludable<T>, IIncludable> includes = null) where T : class
        {
            var dbSet = _dbContext.Set<T>() as IQueryable<T>;

            if (includes != null)
            {
                dbSet = dbSet.IncludeMultiple(includes);
            }

            return predicate == null
                       ? dbSet.AsNoTracking().FirstOrDefault()
                       : dbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public virtual Task<T> UntrackFirstAsync<T>(Expression<Func<T, bool>> predicate = null,
                                  Func<IIncludable<T>, IIncludable> includes = null) where T : class
        {
            var dbSet = _dbContext.Set<T>() as IQueryable<T>;

            if (includes != null)
            {
                dbSet = dbSet.IncludeMultiple(includes);
            }

            return predicate == null
                       ? dbSet.AsNoTracking().FirstOrDefaultAsync()
                       : dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual int Count<T>(Expression<Func<T, bool>> expression = null) where T : class
        {
            return expression == null
                       ? _dbContext.Set<T>().Count()
                       : _dbContext.Set<T>().Count(expression);
        }

        public virtual IQueryable<T> Fetch<T>(Expression<Func<T, bool>> expression,
                                                  Func<IIncludable<T>, IIncludable> includes = null) where T : class
        {
            var dbSet = _dbContext.Set<T>().AsNoTracking().Where(expression);

            if (includes != null)
            {
                return dbSet
                    .IncludeMultiple(includes);
            }

            return dbSet;
        }

        public virtual IQueryable<T> Fetch<T>(Func<IIncludable<T>, IIncludable> includes = null) where T : class
        {
            var dbSet = _dbContext.Set<T>().AsNoTracking();

            if (includes != null)
            {
                return dbSet
                    .IncludeMultiple(includes);
            }

            return dbSet;
        }

        public virtual bool Any<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _dbContext.Set<T>().Any(expression);
        }

        public virtual bool All<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _dbContext.Set<T>().All(expression);
        }

        #endregion Gets

        #region Insert

        public virtual void Insert<T>(T entity) where T : class
        {
            var entityEntry = _dbContext.Entry(entity);

            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                _dbContext.Set<T>().Add(entity);
            }
        }

        public void Insert<T>(params T[] entities) where T : class
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public void Insert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        #endregion Insert

        #region Update

        public virtual void Update<T>(T entity) where T : class
        {
            var entityEntry = _dbContext.Entry(entity);

            if (entityEntry.State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entity);
            }

            entityEntry.State = EntityState.Modified;
        }

        public virtual void Save<T>(T entity) where T : class, BaseEntityId
        {

            if (entity.ID > 0)
            {
                var entityEntry = _dbContext.Entry(entity);

                if (entityEntry.State == EntityState.Detached)
                {
                    _dbContext.Set<T>().Attach(entity);
                }

                entityEntry.State = EntityState.Modified;
            }
            else
            {
                var entityEntry = _dbContext.Entry(entity);

                if (entityEntry.State != EntityState.Detached)
                {
                    entityEntry.State = EntityState.Added;
                }
                else
                {
                    _dbContext.Set<T>().Add(entity);
                }
            }
        }

        public virtual void Save<T>(IEnumerable<T> entities) where T : class, BaseEntityId
        {

            foreach (var entity in entities)
            {
                if (entity.ID > 0)
                {
                    var entityEntry = _dbContext.Entry(entity);

                    if (entityEntry.State == EntityState.Detached)
                    {
                        _dbContext.Set<T>().Attach(entity);
                    }

                    entityEntry.State = EntityState.Modified;
                }
                else
                {
                    var entityEntry = _dbContext.Entry(entity);

                    if (entityEntry.State != EntityState.Detached)
                    {
                        entityEntry.State = EntityState.Added;
                    }
                    else
                    {
                        _dbContext.Set<T>().Add(entity);
                    }
                }
            }
        }

        #endregion Update

        #region Delete

        public virtual void Delete<T>(T entity) where T : class
        {
            if (entity is ISoftDeletableEntity softDeletable)
            {
                softDeletable.IsDeleted = true;
                Update(entity);
            }
            else
            {
                _dbContext.Set<T>().Remove(entity);
            }
        }

        public void HardDelete<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
        }
        #endregion Delete
    }
}