using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
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
         Task<List<WorkoutPlan>> GetWorkoutPlansForUser(int userId);
         Task<List<ExerciseForReturnDto>> GetExercises(ExerciseQueryParams exParams);
         Task<ExerciseForReturnDto> GetExercise(int exerciseId);
         Task<List<ExerciseForReturnDetailedDto>> GetExercisesDetailed(ExerciseQueryParams exParams);
         Task<ExerciseForReturnDetailedDto> GetExerciseDetailed(int exerciseId);
         Task<List<EquipmentForReturnDto>> GetEquipmentForExercise(int exerciseId);
    }
}