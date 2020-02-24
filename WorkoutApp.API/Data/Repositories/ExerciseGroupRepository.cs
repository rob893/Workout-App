using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ExerciseGroupRepository : Repository<ExerciseGroup>
    {
        public ExerciseGroupRepository(DataContext context) : base(context) { }

        public async Task<ExerciseGroup> GetExerciseGroupAsync(int id)
        {
            return await context.ExerciseGroups.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ExerciseGroup> GetExerciseGroupAsync(int id, params Expression<Func<ExerciseGroup, object>>[] includes)
        {
            IQueryable<ExerciseGroup> query = context.ExerciseGroups;
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ExerciseGroup> GetMuscleDetailedAsync(int id)
        {
            return await context.Muscles
                .Include(m => m.PrimaryExercises)
                .Include(m => m.SecondaryExercises)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Muscle>> GetMusclesAsync(PaginationParams searchParams)
        {
            IQueryable<Muscle> query = context.Muscles;

            return await PagedList<Muscle>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<Muscle>> GetMusclesDetailedAsync(PaginationParams searchParams)
        {
            IQueryable<Muscle> query = context.Muscles
                .Include(m => m.PrimaryExercises)
                .Include(m => m.SecondaryExercises);

            return await PagedList<Muscle>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }
    }
}