using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ExerciseRepository : Repository<Exercise>
    {
        public ExerciseRepository(DataContext context) : base(context) { }

        public async Task<Exercise> GetExerciseAsync(int id)
        {
            return await context.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Exercise> GetExerciseDetailedAsync(int id)
        {
            IQueryable<Exercise> query = context.Exercises;

            query = AddDetailedIncludes(query);
            
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PagedList<Exercise>> GetExercisesAsync(ExerciseSearchParams searchParams)
        {
            IQueryable<Exercise> query = context.Exercises;

            query = AddWhereClauses(query, searchParams);

            return await PagedList<Exercise>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<Exercise>> GetExercisesDetailedAsync(ExerciseSearchParams searchParams)
        {
            IQueryable<Exercise> query = context.Exercises;

            query = AddDetailedIncludes(query);
            query = AddWhereClauses(query, searchParams);

            return await PagedList<Exercise>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        private IQueryable<Exercise> AddDetailedIncludes(IQueryable<Exercise> query)
        {
            return query
                .Include(e => e.PrimaryMuscle)
                .Include(e => e.SecondaryMuscle)
                .Include(e => e.ExerciseSteps)
                .Include(e => e.Equipment).ThenInclude(e => e.Equipment)
                .Include(e => e.ExerciseCategorys).ThenInclude(e => e.ExerciseCategory);
        }

        private IQueryable<Exercise> AddWhereClauses(IQueryable<Exercise> query, ExerciseSearchParams searchParams)
        {
            if (searchParams.EquipmentId != null && searchParams.EquipmentId.Count > 0)
            {
                query = query.Where(e => e.Equipment.Any(eq => searchParams.EquipmentId.Contains(eq.EquipmentId)));
            }

            if (searchParams.ExerciseCategoryId != null && searchParams.ExerciseCategoryId.Count > 0)
            {
                query = query.Where(e => e.ExerciseCategorys.Any(ec => searchParams.ExerciseCategoryId.Contains(ec.ExerciseCategoryId)));
            }

            return query;
        }
    }
}