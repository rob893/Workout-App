using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Helpers.QueryParams;
using WorkoutApp.API.Models;

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

        public async Task<User> GetUserAsync(int id)
        {
            User user = await context.Users.Include(u => u.CreatedWorkouts)
                .Include(u => u.FavoriteExercises)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<PagedList<Exercise>> GetExercisesAsync(ExerciseParams exParams)
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
            return await context.ExerciseCategorys.ToListAsync();
        }

        public async Task<PagedList<Equipment>> GetExerciseEquipmentAsync(EquipmentParams eqParams)
        {
            IQueryable<Equipment> equipment = context.Equipment.OrderBy(eq => eq.Id);
            
            if (eqParams.ExerciseIds.Count > 0)
            {
                equipment = equipment.Where(eq => eq.Exercises.Any(ex => eqParams.ExerciseIds.Contains(ex.ExerciseId)));
            }

            return await PagedList<Equipment>.CreateAsync(equipment, eqParams.PageNumber, eqParams.PageSize);
        }

        public async Task<Equipment> GetSingleExerciseEquipmentAsync(int id)
        {
            return await context.Equipment.FirstOrDefaultAsync(eq => eq.Id == id);
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

        public async Task<ScheduledWorkout> GetScheduledUserWorkoutAsync(int id)
        {
            return await context.ScheduledWorkouts
                .Include(wo => wo.Workout).ThenInclude(wo => wo.CreatedByUser)
                .Include(wo => wo.Workout).ThenInclude(wo => wo.ExerciseGroups).ThenInclude(eg => eg.Exercise)
                .Include(wo => wo.AdHocExercises)
                .Include(wo => wo.Attendees)
                .Include(wo => wo.ScheduledByUser)
                .FirstOrDefaultAsync(wo => wo.Id == id);
        }

        public async Task<PagedList<ScheduledWorkout>> GetScheduledUserWorkoutsAsync(SchUsrWoParams woParams)
        {
            IQueryable<ScheduledWorkout> workouts = context.ScheduledWorkouts.OrderBy(wo => wo.ScheduledDateTime)
                .Include(wo => wo.Workout).ThenInclude(wo => wo.CreatedByUser)
                .Include(wo => wo.Workout).ThenInclude(wo => wo.ExerciseGroups).ThenInclude(eg => eg.Exercise)
                .Include(wo => wo.AdHocExercises)
                .Include(wo => wo.Attendees)
                .Include(wo => wo.ScheduledByUser);

            // if (woParams.UserId != null)
            // {
            //     workouts = workouts.Where(wo => wo.UserId == woParams.UserId.Value ||
            //         wo.ExtraSchUsrWoAttendees.Select(a => a.UserId).Contains(woParams.UserId.Value));
            // }

            if (woParams.MinDate != null)
            {
                workouts = workouts.Where(wo => wo.ScheduledDateTime >= woParams.MinDate);
            }

            if (woParams.MaxDate != null)
            {
                workouts = workouts.Where(wo => wo.ScheduledDateTime <= woParams.MaxDate.Value.AddHours(23).AddMinutes(59));
            }

            return await PagedList<ScheduledWorkout>.CreateAsync(workouts, woParams.PageNumber, woParams.PageSize);
        }

        public async Task<WorkoutInvitation> GetWorkoutInvitationAsync(int id)
        {
            return await context.WorkoutInvitations.FirstOrDefaultAsync(woInv => woInv.Id == id);
        }

        public async Task<Muscle> GetMuscleAsync(int id)
        {
            return await context.Muscles.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Muscle>> GetMusclesAsync()
        {
            return await context.Muscles.ToListAsync();
        }
    }
}