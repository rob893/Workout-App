using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class WorkoutRepository : Repository<Workout>
    {
        public WorkoutRepository(DataContext context) : base(context) { }

        public async Task<Workout> GetWorkoutAsync(int id)
        {
            return await context.Workouts.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Workout> GetWorkoutDetailedAsync(int id)
        {
            return await context.Workouts
                .Include(w => w.CreatedByUser)
                .Include(w => w.WorkoutCopiedFrom)
                .Include(w => w.ExerciseGroups).ThenInclude(eg => eg.Exercise)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Workout>> GetWorkoutsAsync(WorkoutSearchParams searchParams)
        {
            IQueryable<Workout> query = context.Workouts;

            if (searchParams.UserId != null)
            {
                query = query.Where(w => w.CreatedByUserId == searchParams.UserId.Value);
            }

            return await PagedList<Workout>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<Workout>> GetWorkoutsDetailedAsync(WorkoutSearchParams searchParams)
        {
            IQueryable<Workout> query = context.Workouts
                .Include(w => w.CreatedByUser)
                .Include(w => w.WorkoutCopiedFrom)
                .Include(w => w.ExerciseGroups).ThenInclude(eg => eg.Exercise);

            if (searchParams.UserId != null)
            {
                query = query.Where(w => w.CreatedByUserId == searchParams.UserId.Value);
            }

            return await PagedList<Workout>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }
    }
}