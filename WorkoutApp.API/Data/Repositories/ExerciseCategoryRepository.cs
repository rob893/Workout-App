using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ExerciseCategoryRepository : Repository<ExerciseCategory>
    {
        public ExerciseCategoryRepository(DataContext context) : base(context) { }

        public async Task<ExerciseCategory> GetExerciseCategoryAsync(int id)
        {
            return await context.ExerciseCategories.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ExerciseCategory> GetExerciseCategoryDetailedAsync(int id)
        {
            return await context.ExerciseCategories
                .Include(e => e.Exercises).ThenInclude(e => e.Exercise)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PagedList<ExerciseCategory>> GetExerciseCategoriesAsync(PaginationParams searchParams)
        {
            IQueryable<ExerciseCategory> query = context.ExerciseCategories;

            return await PagedList<ExerciseCategory>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<ExerciseCategory>> GetExerciseCategoriesDetailedAsync(PaginationParams searchParams)
        {
            IQueryable<ExerciseCategory> query = context.ExerciseCategories
                .Include(e => e.Exercises).ThenInclude(e => e.Exercise);

            return await PagedList<ExerciseCategory>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }
    }
}