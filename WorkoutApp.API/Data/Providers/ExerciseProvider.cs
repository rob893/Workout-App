using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data.Providers
{
    public class ExerciseProvider
    {
        private readonly DataContext context;
        private readonly IMapper mapper;


        public ExerciseProvider(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ExerciseForReturnDto> GetExercise(int id)
        {
            return await context.Exercises
                .AsNoTracking()
                .Select(ex => new ExerciseForReturnDto {
                    Id = ex.Id,
                    Name = ex.Name
                })
                .Where(ex => ex.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ExerciseForReturnDto>> GetExercises()
        {
            return await context.Exercises
                .AsNoTracking()
                .Select(ex => new ExerciseForReturnDto {
                    Id = ex.Id,
                    Name = ex.Name
                })
                .ToListAsync();
        }

        public async Task<List<ExerciseForReturnDetailedDto>> GetExercisesDetailed()
        {
            return await context.Exercises
                .AsNoTracking()
                .Include(ex => ex.PrimaryMuscle)
                .Include(ex => ex.SecondaryMuscle)
                .Include(ex => ex.ExerciseSteps)
                .Include(ex => ex.Equipment).ThenInclude(eq => eq.Equipment)
                .Include(ex => ex.ExerciseCategorys).ThenInclude(ec => ec.ExerciseCategory)
                .Select(ex => mapper.Map<ExerciseForReturnDetailedDto>(ex))
                .ToListAsync();
        }
    }
}