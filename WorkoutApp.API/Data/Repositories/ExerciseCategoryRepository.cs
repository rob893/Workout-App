using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ExerciseCategoryRepository : Repository<ExerciseCategory, ExerciseCategorySearchParams>
    {
        public ExerciseCategoryRepository(DataContext context) : base(context) { }

        protected override IQueryable<ExerciseCategory> AddDetailedIncludes(IQueryable<ExerciseCategory> query)
        {
            return query.Include(e => e.Exercises).ThenInclude(e => e.Exercise);
        }

        protected override IQueryable<ExerciseCategory> AddWhereClauses(IQueryable<ExerciseCategory> query, ExerciseCategorySearchParams searchParams)
        {
            if (searchParams.ExerciseId != null && searchParams.ExerciseId.Count > 0)
            {
                query = query.Where(c => c.Exercises.Any(e => searchParams.ExerciseId.Contains(e.ExerciseId)));
            }

            return query;
        }
    }
}