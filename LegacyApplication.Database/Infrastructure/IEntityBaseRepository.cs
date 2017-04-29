using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Database.Infrastructure
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        int Count();
        Task<int> CountAsync();
        T GetSingle(int id);
        Task<T> GetSingleAsync(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        void Detach(T entity);
        void DetachRange(IEnumerable<T> entities);
        void AttachAsModified(T entity);
        void ChangeStatus(T entity);
    }
}