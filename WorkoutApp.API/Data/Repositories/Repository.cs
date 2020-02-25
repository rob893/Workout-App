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
        where RSearchParams : PaginationParams
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

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> GetByIdDetailedAsync(int id)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddDetailedIncludes(query);

            return await query.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<TEntity>> SearchAsync(RSearchParams searchParams)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddWhereClauses(query, searchParams);

            return await PagedList<TEntity>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<TEntity>> SearchAsync(RSearchParams searchParams, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            query = AddWhereClauses(query, searchParams);

            return await PagedList<TEntity>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<TEntity>> SearchDetailedAsync(RSearchParams searchParams)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = AddDetailedIncludes(query);
            query = AddWhereClauses(query, searchParams);

            return await PagedList<TEntity>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        protected virtual IQueryable<TEntity> AddWhereClauses(IQueryable<TEntity> query, RSearchParams searchParams)
        {
            return query;
        }

        protected virtual void BeforeDelete(TEntity entity) { }

        protected abstract IQueryable<TEntity> AddDetailedIncludes(IQueryable<TEntity> query);
    }
}