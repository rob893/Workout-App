using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class UserRepository : Repository<User, PaginationParams>
    {
        // Perhaps use UserManager<User> in future?
        public UserRepository(DataContext context) : base(context) { }

        protected override IQueryable<User> EntitySet => context.Users;

        protected override IQueryable<User> AddDetailedIncludes(IQueryable<User> query)
        {
            return query.Include(u => u.UserRoles).ThenInclude(r => r.Role);
        }

        public async Task<PagedList<Role>> GetRolesAsync(PaginationParams searchParams)
        {
            IQueryable<Role> query = context.Roles;

            return await PagedList<Role>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await context.Roles.ToListAsync();
        }

        public async Task<PagedList<ScheduledWorkout>> GetScheduledWorkoutsForUserAsync(int userId, ScheduledWorkoutSearchParams searchParams)
        {
            IQueryable<ScheduledWorkout> query = context.ScheduledWorkouts
                .Include(wo => wo.ScheduledByUser)
                .Include(wo => wo.Attendees).ThenInclude(attendee => attendee.User)
                .Include(wo => wo.Workout).ThenInclude(wo => wo.ExerciseGroups).ThenInclude(eg => eg.Exercise)
                .Include(wo => wo.AdHocExercises).ThenInclude(ex => ex.Exercise)
                .Where(wo => wo.Attendees.Any(attendee => attendee.UserId == userId));
            
            if (searchParams.MinDate != null)
            {
                query = query.Where(wo => wo.ScheduledDateTime >= searchParams.MinDate.Value);
            }

            if (searchParams.MaxDate != null)
            {
                query = query.Where(wo => wo.ScheduledDateTime <= searchParams.MaxDate.Value);
            }
                
            return await PagedList<ScheduledWorkout>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<WorkoutCompletionRecord>> GetWorkoutCompletionRecordsForUserAsync(int userId, CompletionRecordSearchParams searchParams)
        {
            IQueryable<WorkoutCompletionRecord> query = context.WorkoutCompletionRecords
                .Where(record => record.UserId == userId);
            
            return await PagedList<WorkoutCompletionRecord>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }
    }
}