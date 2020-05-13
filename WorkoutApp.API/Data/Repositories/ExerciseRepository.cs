using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ExerciseRepository : Repository<Exercise, ExerciseSearchParams>
    {
        public ExerciseRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Exercise>> GetRandomExercisesDetailedAsync(RandomExerciseSearchParams searchParams)
        {
            IQueryable<Exercise> query = context.Exercises;

            query = AddDetailedIncludes(query);
            query = AddWhereClauses(query, searchParams);

            if (searchParams.ExerciseCategory != null)
            {
                query = query.Where(e => e.ExerciseCategorys.Any(ec => ec.ExerciseCategory.Name == searchParams.ExerciseCategory));
            }

            var exercises = await query.ToListAsync();

            return exercises.Shuffle().Take(searchParams.NumExercises.Value);
        }

        protected override IQueryable<Exercise> AddIncludes(IQueryable<Exercise> query)
        {
            return query.Include(e => e.ExerciseSteps);
        }

        protected override IQueryable<Exercise> AddDetailedIncludes(IQueryable<Exercise> query)
        {
            return query
                .Include(e => e.PrimaryMuscle)
                .Include(e => e.SecondaryMuscle)
                .Include(e => e.ExerciseSteps)
                .Include(e => e.Equipment).ThenInclude(e => e.Equipment)
                .Include(e => e.ExerciseCategorys).ThenInclude(e => e.ExerciseCategory);
        }

        protected override IQueryable<Exercise> AddWhereClauses(IQueryable<Exercise> query, ExerciseSearchParams searchParams)
        {
            if (searchParams.EquipmentId != null && searchParams.EquipmentId.Count > 0)
            {
                query = query.Where(e => e.Equipment.Any(eq => searchParams.EquipmentId.Contains(eq.EquipmentId)));
            }

            if (searchParams.ExerciseCategoryId != null && searchParams.ExerciseCategoryId.Count > 0)
            {
                query = query.Where(e => e.ExerciseCategorys.Any(ec => searchParams.ExerciseCategoryId.Contains(ec.ExerciseCategoryId)));
            }

            if (searchParams.PrimaryMuscleId != null && searchParams.PrimaryMuscleId.Count > 0)
            {
                query = query.Where(e => e.PrimaryMuscle != null && searchParams.PrimaryMuscleId.Contains(e.PrimaryMuscle.Id));
            }

            if (searchParams.SecondaryMuscleId != null && searchParams.SecondaryMuscleId.Count > 0)
            {
                query = query.Where(e => e.SecondaryMuscle != null && searchParams.SecondaryMuscleId.Contains(e.SecondaryMuscle.Id));
            }

            return query;
        }
    }
}