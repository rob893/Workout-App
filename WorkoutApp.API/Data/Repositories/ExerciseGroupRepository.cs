using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ExerciseGroupRepository : Repository<ExerciseGroup, ExerciseGroupSearchParams>
    {
        public ExerciseGroupRepository(DataContext context) : base(context) { }

        protected override IQueryable<ExerciseGroup> AddDetailedIncludes(IQueryable<ExerciseGroup> query)
        {
            return query
                .Include(e => e.Exercise)
                .Include(e => e.Workout)
                .Include(e => e.ScheduledWorkout);
        }

        protected override IQueryable<ExerciseGroup> AddWhereClauses(IQueryable<ExerciseGroup> query, ExerciseGroupSearchParams searchParams)
        {
            if (searchParams.WorkoutId != null && searchParams.WorkoutId.Count > 0)
            {
                query = query.Where(group => group.Workout != null && searchParams.WorkoutId.Contains(group.Workout.Id));
            }

            if (searchParams.ScheduledWorkoutId != null && searchParams.ScheduledWorkoutId.Count > 0)
            {
                query = query.Where(group => group.ScheduledWorkout != null && searchParams.ScheduledWorkoutId.Contains(group.ScheduledWorkout.Id));
            }

            return query;
        }
    }
}