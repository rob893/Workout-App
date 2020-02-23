using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data
{
    public interface IWorkoutRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        void DeleteRange<T>(IEnumerable<T> entity) where T: class;
        Task<bool> SaveAllAsync();

        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);

        Task<Workout> GetWorkoutAsync(int id);
        Task<PagedList<Workout>> GetWorkoutsAsync(WorkoutParams woParams);

        Task<ScheduledWorkout> GetScheduledUserWorkoutAsync(int id);
        Task<PagedList<ScheduledWorkout>> GetScheduledUserWorkoutsAsync(ScheduledWorkoutSearchParams woParams);

        Task<WorkoutInvitation> GetWorkoutInvitationAsync(int id);

        Task<PagedList<Exercise>> GetExercisesAsync(ExerciseSearchParams exParams);
        Task<Exercise> GetExerciseAsync(int exerciseId);

        Task<PagedList<Equipment>> GetExerciseEquipmentAsync(EquipmentSearchParams eqParams);
        Task<Equipment> GetSingleExerciseEquipmentAsync(int id);
    }
}