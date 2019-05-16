using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Helpers.QueryParams;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data
{
    public interface IWorkoutRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();

        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);

        //Task<PagedList<WorkoutPlan>> GetWorkoutPlans();
        Task<WorkoutPlan> GetWorkoutPlan(int id);
        Task<List<WorkoutPlan>> GetWorkoutPlansForUser(int userId);

        Task<PagedList<Workout>> GetWorkouts(WorkoutParams woParams);

        Task<PagedList<Exercise>> GetExercises(ExerciseParams exParams);
        Task<Exercise> GetExercise(int exerciseId);

        Task<PagedList<Equipment>> GetExerciseEquipment(EquipmentParams eqParams);
        Task<Equipment> GetSingleExerciseEquipment(int id);
    }
}