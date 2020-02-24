using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkoutApp.API.Data.Repositories
{
    public abstract class Repository<T> where T : class
    {
        protected readonly DataContext context;


        public Repository(DataContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            context.AddRange(entities);
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            context.RemoveRange(entities);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}