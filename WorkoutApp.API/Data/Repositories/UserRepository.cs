using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class UserRepository : Repository<User, PaginationParams>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;


        public UserRepository(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public Task<IdentityResult> CreateUserWithPasswordAsync(User user, string password)
        {
            return userManager.CreateAsync(user, password);
        }

        public Task<User> GetByUsernameDetailedAsync(string username)
        {
            IQueryable<User> query = context.Users;
            query = AddDetailedIncludes(query);

            return query.FirstOrDefaultAsync(e => e.UserName == username);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

            return result.Succeeded;
        }

        public Task<PagedList<Role>> GetRolesAsync(PaginationParams searchParams)
        {
            IQueryable<Role> query = context.Roles;

            return PagedList<Role>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public Task<List<Role>> GetRolesAsync()
        {
            return context.Roles.ToListAsync();
        }

        public Task<PagedList<ScheduledWorkout>> GetScheduledWorkoutsForUserAsync(int userId, ScheduledWorkoutSearchParams searchParams)
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
                
            return PagedList<ScheduledWorkout>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public Task<PagedList<WorkoutCompletionRecord>> GetWorkoutCompletionRecordsForUserAsync(int userId, CompletionRecordSearchParams searchParams)
        {
            IQueryable<WorkoutCompletionRecord> query = context.WorkoutCompletionRecords
                .Where(record => record.UserId == userId);
            
            return PagedList<WorkoutCompletionRecord>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        protected override IQueryable<User> AddDetailedIncludes(IQueryable<User> query)
        {
            return query.Include(u => u.RefreshTokens)
                .Include(u => u.UserRoles).ThenInclude(r => r.Role);
        }
    }
}