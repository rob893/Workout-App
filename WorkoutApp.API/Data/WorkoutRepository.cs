using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data
{
    public class WorkoutRepositoryOLD : IWorkoutRepository
    {
        private readonly DataContext context;


        public WorkoutRepositoryOLD(DataContext context)
        {
            this.context = context;
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

        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void DeleteRange<T>(IEnumerable<T> entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Workout> GetWorkoutAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Workout>> GetWorkoutsAsync(WorkoutSearchParams woParams)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Exercise>> GetExercisesAsync(ExerciseSearchParams exParams)
        {
            throw new System.NotImplementedException();
        }

        public Task<Exercise> GetExerciseAsync(int exerciseId)
        {
            throw new System.NotImplementedException();
        }
    }
}