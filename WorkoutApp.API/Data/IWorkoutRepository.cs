using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}