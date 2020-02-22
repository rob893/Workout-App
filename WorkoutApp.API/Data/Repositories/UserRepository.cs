using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DataContext context) : base(context) { }


        public async Task<User> GetUserBasicAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserDetailedAsync(int id)
        {
            return await context.Users
                .Include(u => u.CreatedWorkouts)
                .Include(u => u.WorkoutCompletionRecords)
                .Include(u => u.ExerciseGroupCompletionRecords)
                .Include(u => u.ExerciseSetCompletionRecords)
                .Include(u => u.FavoriteExercises)
                .Include(u => u.ScheduledWorkouts)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}