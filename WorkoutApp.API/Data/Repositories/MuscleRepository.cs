using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class MuscleRepository : Repository<Muscle, PaginationParams>
    {
        private readonly ExerciseRepository exerciseRepository;


        public MuscleRepository(DataContext context, ExerciseRepository exerciseRepository) : base(context) 
        {
            this.exerciseRepository = exerciseRepository;
        }

        public async Task<PagedList<Exercise>> GetPrimaryExercisesForMuscleAsync(int muscleId, ExerciseSearchParams searchParams)
        {
            IQueryable<Exercise> query = context.Muscles
                .Where(m => m.Id == muscleId)
                .SelectMany(m => m.PrimaryExercises);
            
            return await exerciseRepository.SearchAsync(query, searchParams);
        }

        public async Task<PagedList<Exercise>> GetSecondaryExercisesForMuscleAsync(int muscleId, ExerciseSearchParams searchParams)
        {
            IQueryable<Exercise> query = context.Muscles
                .Where(m => m.Id == muscleId)
                .SelectMany(m => m.SecondaryExercises);
            
            return await exerciseRepository.SearchAsync(query, searchParams);
        }

        protected override IQueryable<Muscle> AddDetailedIncludes(IQueryable<Muscle> query)
        {
            return query
                .Include(m => m.PrimaryExercises)
                .Include(m => m.SecondaryExercises);
        }
    }
}