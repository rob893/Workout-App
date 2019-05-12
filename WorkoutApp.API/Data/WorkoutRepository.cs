using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                .ThenInclude(e => e.Muscle)
                .ThenInclude(m => m.MuscleGroup)
                .Where(p => p.User.Id == userId).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}