using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly DataContext context;


        public WorkoutRepository(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            context.RemoveRange(entities);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<PagedList<Exercise>> GetExercisesAsync(ExerciseSearchParams exParams)
        {
            IQueryable<Exercise> exercises = context.Exercises
                .Include(ex => ex.PrimaryMuscle)
                .Include(ex => ex.SecondaryMuscle)
                .Include(ex => ex.ExerciseSteps)
                .Include(ex => ex.Equipment).ThenInclude(eq => eq.Equipment)
                .Include(ex => ex.ExerciseCategorys).ThenInclude(eq => eq.ExerciseCategory)
                .OrderBy(ex => ex.Id);

            if (exParams.ExerciseCategoryId.Count > 0)
            {
                exercises = exercises.Where(ex => ex.ExerciseCategorys.Any(ec => exParams.ExerciseCategoryId.Contains(ec.ExerciseCategoryId)));
            }

            if (exParams.EquipmentId.Count > 0)
            {
                exercises = exercises.Where(ex => ex.Equipment.Any(eq => exParams.EquipmentId.Contains(eq.EquipmentId)));
            }

            return await PagedList<Exercise>.CreateAsync(exercises, exParams.PageNumber, exParams.PageSize);
        }

        public async Task<Exercise> GetExerciseAsync(int exerciseId)
        {
            return await context.Exercises
                .Include(ex => ex.PrimaryMuscle)
                .Include(ex => ex.SecondaryMuscle)
                .Include(ex => ex.ExerciseSteps)
                .Include(ex => ex.Equipment).ThenInclude(eq => eq.Equipment)
                .Include(ex => ex.ExerciseCategorys).ThenInclude(eq => eq.ExerciseCategory)
                .FirstOrDefaultAsync(e => e.Id == exerciseId);
        }

        public async Task<List<ExerciseCategory>> GetExerciseCategories()
        {
            return await context.ExerciseCategories.ToListAsync();
        }

        public async Task<Workout> GetWorkoutAsync(int id)
        {
            return await context.Workouts
                .Include(wo => wo.ExerciseGroups)
                .ThenInclude(eg => eg.Exercise)
                .FirstOrDefaultAsync(wo => wo.Id == id);
        }

        public async Task<PagedList<Workout>> GetWorkoutsAsync(WorkoutParams woParams)
        {
            IQueryable<Workout> workouts = context.Workouts.OrderBy(wo => wo.Id)
                .Include(wo => wo.ExerciseGroups)
                .ThenInclude(eg => eg.Exercise);

            if (woParams.UserId != null)
            {
                workouts = workouts.Where(wo => wo.CreatedByUserId == woParams.UserId.Value || wo.CreatedByUserId == 1);
            }

            return await PagedList<Workout>.CreateAsync(workouts, woParams.PageNumber, woParams.PageSize);
        }

        public async Task<WorkoutInvitation> GetWorkoutInvitationAsync(int id)
        {
            return await context.WorkoutInvitations.FirstOrDefaultAsync(woInv => woInv.Id == id);
        }

        public Task<List<User>> GetUsersAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScheduledWorkout> GetScheduledUserWorkoutAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<ScheduledWorkout>> GetScheduledUserWorkoutsAsync(ScheduledWorkoutSearchParams woParams)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Equipment>> GetExerciseEquipmentAsync(EquipmentSearchParams eqParams)
        {
            throw new System.NotImplementedException();
        }

        public Task<Equipment> GetSingleExerciseEquipmentAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}