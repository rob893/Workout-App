using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
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

        public async Task<User> GetUser(int id)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<List<WorkoutPlan>> GetWorkoutPlansForUser(int userId)
        {
            return await context.WorkoutPlans.Include(p => p.User).Include(p => p.Workouts)
                .ThenInclude(w => w.ExerciseGroups)
                .ThenInclude(eg => eg.Exercise)
                .ThenInclude(e => e.PrimaryMuscle)
                .Where(p => p.User.Id == userId).ToListAsync();
        }

        public async Task<PagedList<Exercise>> GetExercises(ExerciseQueryParams exParams)
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

        public async Task<Exercise> GetExercise(int exerciseId)
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

        public async Task<PagedList<Equipment>> GetExerciseEquipment(EquipmentQueryParams eqParams)
        {
            IQueryable<Equipment> equipment = context.Equipment.OrderBy(eq => eq.Id);

            if (eqParams.ExerciseIds.Count > 0)
            {
                equipment = equipment.Where(eq => eq.Exercises.Any(ex => eqParams.ExerciseIds.Contains(ex.ExerciseId)));
            }

            return await PagedList<Equipment>.CreateAsync(equipment, eqParams.PageNumber, eqParams.PageSize);
        }

        public async Task<Equipment> GetSingleExerciseEquipment(int id)
        {
            return await context.Equipment.FirstOrDefaultAsync(eq => eq.Id == id);
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}