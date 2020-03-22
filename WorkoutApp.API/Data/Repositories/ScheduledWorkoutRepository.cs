using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class ScheduledWorkoutRepository : Repository<ScheduledWorkout, ScheduledWorkoutSearchParams>
    {
        public ScheduledWorkoutRepository(DataContext context) : base(context) { }

        public Task<PagedList<User>> GetScheduledWorkoutAttendeesAsync(int scheduledWorkoutId, PaginationParams searchParams)
        {
            IQueryable<User> query = context.ScheduledWorkouts
                .Where(wo => wo.Id == scheduledWorkoutId)
                .SelectMany(swo => swo.Attendees.Select(attendee => attendee.User));
            
            return PagedList<User>.CreateAsync(query, searchParams);
        }

        protected override IQueryable<ScheduledWorkout> AddDetailedIncludes(IQueryable<ScheduledWorkout> query)
        {
            return query
                .Include(w => w.ScheduledByUser)
                .Include(w => w.Attendees).ThenInclude(a => a.User)
                .Include(w => w.Workout).ThenInclude(w => w.ExerciseGroups).ThenInclude(eg => eg.Exercise)
                .Include(w => w.AdHocExercises).ThenInclude(eg => eg.Exercise);
        }

        protected override IQueryable<ScheduledWorkout> AddWhereClauses(IQueryable<ScheduledWorkout> query, ScheduledWorkoutSearchParams searchParams)
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

        protected override void BeforeDelete(ScheduledWorkout entity)
        {
            if (entity.AdHocExercises == null)
            {
                throw new ArgumentNullException("AdHocExercises must be loaded before delete.");
            }

            context.ExerciseGroups.RemoveRange(entity.AdHocExercises);
        }
    }
}