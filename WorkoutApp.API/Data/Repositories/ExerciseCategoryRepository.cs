using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ExerciseCategoryRepository : Repository<ExerciseCategory, PaginationParams>
    {
        public ExerciseCategoryRepository(DataContext context) : base(context) { }

        protected override IQueryable<ExerciseCategory> AddDetailedIncludes(IQueryable<ExerciseCategory> query)
        {
            return query.Include(e => e.Exercises).ThenInclude(e => e.Exercise);
        }
    }
}