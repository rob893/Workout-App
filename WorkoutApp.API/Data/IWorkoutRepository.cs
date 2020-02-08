using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Helpers.QueryParams;
using WorkoutApp.API.Models;

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

        Task<Muscle> GetMuscleAsync(int id);
        Task<IEnumerable<Muscle>> GetMusclesAsync();

        Task<ScheduledUserWorkout> GetScheduledUserWorkoutAsync(int id);
        Task<PagedList<ScheduledUserWorkout>> GetScheduledUserWorkoutsAsync(SchUsrWoParams woParams);

        Task<WorkoutInvitation> GetWorkoutInvitationAsync(int id);

        Task<PagedList<Exercise>> GetExercisesAsync(ExerciseParams exParams);
        Task<Exercise> GetExerciseAsync(int exerciseId);

        Task<PagedList<Equipment>> GetExerciseEquipmentAsync(EquipmentParams eqParams);
        Task<Equipment> GetSingleExerciseEquipmentAsync(int id);
    }
}