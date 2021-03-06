using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class EquipmentRepository : Repository<Equipment, EquipmentSearchParams>
    {
        public EquipmentRepository(DataContext context) : base(context) { }

        protected override IQueryable<Equipment> AddDetailedIncludes(IQueryable<Equipment> query)
        {
            return query.Include(e => e.Exercises).ThenInclude(e => e.Exercise);
        }

        protected override IQueryable<Equipment> AddWhereClauses(IQueryable<Equipment> query, EquipmentSearchParams searchParams)
        {
            if (searchParams != null && searchParams.ExerciseIds != null && searchParams.ExerciseIds.Count > 0)
            {
                query = query.Where(equipment => equipment.Exercises.Any(exercise => searchParams.ExerciseIds.Contains(exercise.ExerciseId)));
            }

            return query;
        }
    }
}