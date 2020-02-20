using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Helpers.QueryParams;
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
                .Select(ex => new ExerciseForReturnDto
                {
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
                .Select(ex => new ExerciseForReturnDto
                {
                    Id = ex.Id,
                    Name = ex.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetRandomFavoriteExercisesForUserAsync(int numExercises, int userId, string exerciseCategory = null)
        {
            var exercisesQ = context.Users.AsNoTracking()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.FavoriteExercises.Select(fe => fe.Exercise))
                .Include(e => e.ExerciseCategorys).ThenInclude(ec => ec.ExerciseCategory)
                .Include(e => e.Equipment).ThenInclude(eq => eq.Equipment)
                .Include(e => e.PrimaryMuscle)
                .Include(e => e.SecondaryMuscle)
                .Include(e => e.ExerciseSteps);

            if (exerciseCategory != null)
            {
                exercisesQ.Where(e => e.ExerciseCategorys.Any(ec => ec.ExerciseCategory.Name == exerciseCategory));
            }

            var exercises = await exercisesQ.ToListAsync();

            return exercises.Shuffle().Take(numExercises);
        }

        public async Task<IEnumerable<Exercise>> GetRandomExercisesAsync(int numExercises, string exerciseCategory = null)
        {
            var exercisesQ = context.ExerciseCategorys.AsNoTracking()
               .SelectMany(ec => ec.Exercises.Select(ece => ece.Exercise))
               .Include(e => e.ExerciseCategorys).ThenInclude(ec => ec.ExerciseCategory)
               .Include(e => e.Equipment).ThenInclude(eq => eq.Equipment)
               .Include(e => e.PrimaryMuscle)
               .Include(e => e.SecondaryMuscle)
               .Include(e => e.ExerciseSteps);
            
            if (exerciseCategory != null)
            {
                exercisesQ.Where(e => e.ExerciseCategorys.Any(ec => ec.ExerciseCategory.Name == exerciseCategory));
            }

            var exercises = await exercisesQ.ToListAsync();

            return exercises.Shuffle().Take(numExercises);
        }

        public async Task<PagedList<ExerciseForReturnDetailedDto>> GetExercisesDetailed(ExerciseParams exParams)
        {
            IQueryable<Exercise> exercises = context.Exercises
                .AsNoTracking()
                .OrderBy(ex => ex.Id);

            if (exParams.ExerciseCategoryId.Count > 0)
            {
                exercises = exercises.Where(ex => ex.ExerciseCategorys.Any(ec => exParams.ExerciseCategoryId.Contains(ec.ExerciseCategoryId)));
            }

            if (exParams.EquipmentId.Count > 0)
            {
                exercises = exercises.Where(ex => ex.Equipment.Any(eq => exParams.EquipmentId.Contains(eq.EquipmentId)));
            }

            IQueryable<ExerciseForReturnDetailedDto> exercisesToReturn = exercises
                .Select(ex => new ExerciseForReturnDetailedDto
                {
                    Id = ex.Id,
                    Name = ex.Name,
                    PrimaryMuscle = ex.PrimaryMuscle,
                    SecondaryMuscle = ex.SecondaryMuscle,
                    ExerciseSteps = ex.ExerciseSteps.Select(es => new ExerciseStepForReturnDto
                    {
                        ExerciseStepNumber = es.ExerciseStepNumber,
                        Description = es.Description
                    }).ToList(),
                    Equipment = ex.Equipment.Select(eq => new EquipmentForReturnDto
                    {
                        Id = eq.EquipmentId,
                        Name = eq.Equipment.Name
                    }).ToList(),
                    ExerciseCategorys = ex.ExerciseCategorys.Select(ec => new ExerciseCategoryForReturnDto
                    {
                        Id = ec.ExerciseCategory.Id,
                        Name = ec.ExerciseCategory.Name
                    }).ToList()
                });

            return await PagedList<ExerciseForReturnDetailedDto>.CreateAsync(exercisesToReturn, exParams.PageNumber, exParams.PageSize);
        }
    }
}