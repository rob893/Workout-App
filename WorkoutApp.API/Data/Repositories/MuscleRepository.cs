using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class MuscleRepository : Repository<Muscle>
    {
        public MuscleRepository(DataContext context) : base(context) { }

        public async Task<Muscle> GetMuscleAsync(int id)
        {
            return await context.Muscles.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Muscle> GetMuscleDetailedAsync(int id)
        {
            return await context.Muscles
                .Include(m => m.PrimaryExercises)
                .Include(m => m.SecondaryExercises)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Muscle>> GetMusclesAsync(PaginationParams searchParams)
        {
            IQueryable<Muscle> query = context.Muscles;

            return await PagedList<Muscle>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public async Task<PagedList<Muscle>> GetMusclesDetailedAsync(PaginationParams searchParams)
        {
            IQueryable<Muscle> query = context.Muscles
                .Include(m => m.PrimaryExercises)
                .Include(m => m.SecondaryExercises);

            return await PagedList<Muscle>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }
    }
}