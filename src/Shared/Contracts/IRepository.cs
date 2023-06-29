using Shared.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Contracts
{
    public interface IRepository
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        void SetEntityStateUnchanged<TEntity>(TEntity entity);

        IQueryable<T> Query<T>(Func<IIncludable<T>, IIncludable> includes = null) where T : class;

        T Get<T>(params object[] id) where T : class;

        T Get<T>(Expression<Func<T, bool>> selector, Func<IIncludable<T>, IIncludable> includes = null) where T : class;

        T GetById<T>(params object[] id) where T : class;

        IQueryable<T> GetByIds<T>(params object[] ids) where T : class;

        T UntrackFirst<T>(Expression<Func<T, bool>> predicate = null,
                          Func<IIncludable<T>, IIncludable> includes = null) where T : class;

        Task<T> UntrackFirstAsync<T>(Expression<Func<T, bool>> predicate = null,
                          Func<IIncludable<T>, IIncludable> includes = null) where T : class;

        T First<T>(Expression<Func<T, bool>> predicate = null, Func<IIncludable<T>, IIncludable> includes = null) where T : class;

        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IIncludable<T>, IIncludable> includes = null) where T : class;

        int Count<T>(Expression<Func<T, bool>> expression = null) where T : class;

        IQueryable<T> Fetch<T>(Expression<Func<T, bool>> expression, Func<IIncludable<T>, IIncludable> includes = null) where T : class;
        IQueryable<T> Fetch<T>(Func<IIncludable<T>, IIncludable> includes = null) where T : class;

        bool Any<T>(Expression<Func<T, bool>> expression) where T : class;

        bool All<T>(Expression<Func<T, bool>> expression) where T : class;

        void Insert<T>(T entity) where T : class;

        void Insert<T>(params T[] entities) where T : class;

        void Insert<T>(IEnumerable<T> entities) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        void HardDelete<T>(T entity) where T : class;
        void Save<T>(T entity) where T : class, BaseEntityId;
        void Save<T>(IEnumerable<T> entities) where T : class, BaseEntityId;
    }
}