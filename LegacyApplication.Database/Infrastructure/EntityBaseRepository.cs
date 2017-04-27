using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LegacyApplication.Database.Context;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Database.Infrastructure
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
    {
        #region Properties
        protected CoreContext Context { get; }

        public EntityBaseRepository(IUnitOfWork unitOfWork)
        {
            Context = unitOfWork as CoreContext;
        }
        #endregion

        public virtual IQueryable<T> All => Context.Set<T>();

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual int Count()
        {
            return Context.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }

        public T GetSingle(int id)
        {
            return Context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public async Task<T> GetSingleAsync(int id)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.Where(predicate).FirstOrDefault();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = Context.Entry<T>(entity);
            Context.Set<T>().Add(entity);
        }
        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = Context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = Context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                DbEntityEntry dbEntityEntry = Context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = Context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                Context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }
        public void Attach(T entity)
        {
            Context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Attach(entity);
            }
        }

        public void Detach(T entity)
        {
            Context.Entry<T>(entity).State = EntityState.Detached;
        }

        public void DetachRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Detach(entity);
            }
        }

        public virtual void ChangeStatus(T entity)
        {
            var dbEntityEntry = Context.Entry<T>(entity);
            dbEntityEntry.Property(x => x.Status).IsModified = true;
        }

    }
}