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
    public class ScheduledWorkoutRepository : Repository<ScheduledWorkout>
    {
        public ScheduledWorkoutRepository(DataContext context) : base(context) { }

        public async Task<ScheduledWorkout> GetScheduledWorkoutAsync(int id)
        {
            return await context.ScheduledWorkouts.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<ScheduledWorkout> GetScheduledWorkoutAsync(int id, params Expression<Func<ScheduledWorkout, object>>[] includes)
        {
            IQueryable<ScheduledWorkout> query = context.ScheduledWorkouts;
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<ScheduledWorkout> GetScheduledWorkoutDetailedAsync(int id)
        {
            IQueryable<ScheduledWorkout> query = context.ScheduledWorkouts;

            query = AddDetailedIncludes(query);

            return await query.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<PagedList<ScheduledWorkout>> GetScheduledWorkoutsAsync(ScheduledWorkoutSearchParams searchParams)
        {
            IQueryable<ScheduledWorkout> query = context.ScheduledWorkouts;

            query = AddWhereClauses(query, searchParams);

            return await PagedList<ScheduledWorkout>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<ScheduledWorkout>> GetScheduledWorkoutsDetailedAsync(ScheduledWorkoutSearchParams searchParams)
        {
            IQueryable<ScheduledWorkout> query = context.ScheduledWorkouts;

            query = AddWhereClauses(query, searchParams);
            query = AddDetailedIncludes(query);

            return await PagedList<ScheduledWorkout>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        private IQueryable<ScheduledWorkout> AddDetailedIncludes(IQueryable<ScheduledWorkout> query)
        {
            return query
                .Include(w => w.ScheduledByUser)
                .Include(w => w.Attendees).ThenInclude(a => a.User)
                .Include(w => w.Workout).ThenInclude(w => w.ExerciseGroups).ThenInclude(eg => eg.Exercise)
                .Include(w => w.AdHocExercises).ThenInclude(eg => eg.Exercise);
        }

        private IQueryable<ScheduledWorkout> AddWhereClauses(IQueryable<ScheduledWorkout> query, ScheduledWorkoutSearchParams searchParams)
        {
            if (searchParams.ScheduledByUserId != null)
            {
                query = query.Where(wo => wo.ScheduledByUserId == searchParams.ScheduledByUserId.Value);
            }

            if (searchParams.MinDate != null)
            {
                query = query.Where(wo => wo.ScheduledDateTime >= searchParams.MinDate.Value);
            }

            if (searchParams.MaxDate != null)
            {
                query = query.Where(wo => wo.ScheduledDateTime <= searchParams.MaxDate.Value);
            }

            return query;
        }
    }
}