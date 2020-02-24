using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class MuscleRepository : Repository<Muscle, PaginationParams>
    {
        public MuscleRepository(DataContext context) : base(context) { }

        protected override IQueryable<Muscle> EntitySet => context.Muscles;

        protected override IQueryable<Muscle> AddDetailedIncludes(IQueryable<Muscle> query)
        {
            return query
                .Include(m => m.PrimaryExercises)
                .Include(m => m.SecondaryExercises);
        }
    }
}