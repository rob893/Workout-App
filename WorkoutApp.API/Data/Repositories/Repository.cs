using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkoutApp.API.Models.QueryParams;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Data.Repositories
{
    public abstract class Repository<TEntity, RSearchParams>
        where TEntity : class, IIdentifiable
        where RSearchParams : CursorPaginationParams
    {
        protected readonly DataContext context;


        public Repository(DataContext context)
        {
            this.context = context;
        }

        public EntityEntry<TEntity> Entry(TEntity entity)
        {
            return context.Entry(entity);
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            BeforeDelete(entity);
            context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddIncludes(query);

            return query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddIncludes(query);
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<TEntity> GetByIdDetailedAsync(int id)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddDetailedIncludes(query);

            return query.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<CursorPagedList<TEntity>> SearchAsync(RSearchParams searchParams)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddIncludes(query);
            query = AddWhereClauses(query, searchParams);

            return CursorPagedList<TEntity>.CreateAsync(query, searchParams);
        }

        public Task<CursorPagedList<TEntity>> SearchAsync(IQueryable<TEntity> query, RSearchParams searchParams)
        {
            query = AddIncludes(query);
            query = AddWhereClauses(query, searchParams);

            return CursorPagedList<TEntity>.CreateAsync(query, searchParams);
        }

        public Task<CursorPagedList<TEntity>> SearchAsync(RSearchParams searchParams, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddIncludes(query);
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            query = AddWhereClauses(query, searchParams);

            return CursorPagedList<TEntity>.CreateAsync(query, searchParams);
        }

        public Task<CursorPagedList<TEntity>> SearchDetailedAsync(RSearchParams searchParams)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddDetailedIncludes(query);
            query = AddWhereClauses(query, searchParams);

            return CursorPagedList<TEntity>.CreateAsync(query, searchParams);
        }

        public Task<CursorPagedList<TEntity>> SearchDetailedAsync(IQueryable<TEntity> query, RSearchParams searchParams)
        {
            query = AddDetailedIncludes(query);
            query = AddWhereClauses(query, searchParams);

            return CursorPagedList<TEntity>.CreateAsync(query, searchParams);
        }

        protected virtual IQueryable<TEntity> AddWhereClauses(IQueryable<TEntity> query, RSearchParams searchParams)
        {
            return query;
        }

        protected virtual void BeforeDelete(TEntity entity) { }

        protected virtual IQueryable<TEntity> AddIncludes(IQueryable<TEntity> query)
        {
            return query;
        }

        protected abstract IQueryable<TEntity> AddDetailedIncludes(IQueryable<TEntity> query);
    }
}