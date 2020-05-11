using System.Collections.Generic;
using System.Linq;
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
        private readonly ExerciseRepository exerciseRepository;
        private readonly ScheduledWorkoutRepository scheduledWorkoutRepository;


        public UserRepository(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager,
            ExerciseRepository exerciseRepository, ScheduledWorkoutRepository scheduledWorkoutRepository
            ) : base(context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.exerciseRepository = exerciseRepository;
            this.scheduledWorkoutRepository = scheduledWorkoutRepository;
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

        public Task<OffsetPagedList<Role>> GetRolesAsync(PaginationParams searchParams)
        {
            IQueryable<Role> query = context.Roles;

            return OffsetPagedList<Role>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public Task<List<Role>> GetRolesAsync()
        {
            return context.Roles.ToListAsync();
        }

        public Task<OffsetPagedList<Exercise>> GetFavoriteExercisesForUserAsync(int userId, ExerciseSearchParams searchParams)
        {
            IQueryable<Exercise> query = context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.FavoriteExercises.Select(fe => fe.Exercise));

            return exerciseRepository.SearchAsync(query, searchParams);
        }

        public Task<OffsetPagedList<Exercise>> GetFavoriteExercisesForUserDetailedAsync(int userId, ExerciseSearchParams searchParams)
        {
            IQueryable<Exercise> query = context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.FavoriteExercises.Select(fe => fe.Exercise));

            return exerciseRepository.SearchDetailedAsync(query, searchParams);
        }

        public Task<OffsetPagedList<ScheduledWorkout>> GetScheduledWorkoutsForUserAsync(int userId, ScheduledWorkoutSearchParams searchParams)
        {
            IQueryable<ScheduledWorkout> query = context.ScheduledWorkouts
                .Where(wo => wo.Attendees.Any(attendee => attendee.UserId == userId));

            return scheduledWorkoutRepository.SearchAsync(query, searchParams);
        }

        public Task<OffsetPagedList<ScheduledWorkout>> GetScheduledWorkoutsForUserDetailedAsync(int userId, ScheduledWorkoutSearchParams searchParams)
        {
            IQueryable<ScheduledWorkout> query = context.ScheduledWorkouts
                .Where(wo => wo.Attendees.Any(attendee => attendee.UserId == userId));

            return scheduledWorkoutRepository.SearchDetailedAsync(query, searchParams);
        }

        public Task<OffsetPagedList<WorkoutCompletionRecord>> GetWorkoutCompletionRecordsForUserAsync(int userId, CompletionRecordSearchParams searchParams)
        {
            IQueryable<WorkoutCompletionRecord> query = context.WorkoutCompletionRecords
                .Where(record => record.UserId == userId);

            return OffsetPagedList<WorkoutCompletionRecord>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        protected override IQueryable<User> AddDetailedIncludes(IQueryable<User> query)
        {
            return query.Include(u => u.RefreshTokens)
                .Include(u => u.UserRoles).ThenInclude(r => r.Role);
        }
    }
}