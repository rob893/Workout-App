using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ExerciseGroupRepository : Repository<ExerciseGroup, PaginationParams>
    {
        public ExerciseGroupRepository(DataContext context) : base(context) { }

        protected override IQueryable<ExerciseGroup> AddDetailedIncludes(IQueryable<ExerciseGroup> query)
        {
            return query
                .Include(e => e.Exercise)
                .Include(e => e.Workout)
                .Include(e => e.ScheduledWorkout);
        }
    }
}