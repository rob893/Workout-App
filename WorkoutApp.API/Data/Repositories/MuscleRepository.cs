using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class MuscleRepository : Repository<Muscle, CursorPaginationParams>
    {
        public MuscleRepository(DataContext context) : base(context) { }

        protected override IQueryable<Muscle> AddDetailedIncludes(IQueryable<Muscle> query)
        {
            return query
                .Include(m => m.PrimaryExercises)
                .Include(m => m.SecondaryExercises);
        }
    }
}