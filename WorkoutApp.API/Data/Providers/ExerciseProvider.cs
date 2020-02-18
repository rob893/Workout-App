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

        public async Task<IEnumerable<ExerciseForReturnDetailedDto>> GetRandomExercisesAsync(RandomExercisesParams exParams)
        {
            var exercises = await context.Exercises.AsNoTracking()
                .Include(e => e.ExerciseCategorys).ThenInclude(ec => ec.ExerciseCategory)
                .Include(e => e.Equipment).ThenInclude(eq => eq.Equipment)
                .Include(e => e.PrimaryMuscle)
                .Include(e => e.SecondaryMuscle)
                .Include(e => e.ExerciseSteps)
                .ToListAsync();
            
            var exerciseDict = new Dictionary<string, List<Exercise>>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var e in exercises)
            {
                foreach (var category in e.ExerciseCategorys.Select(ec => ec.ExerciseCategory.Name))
                {
                    if (exerciseDict.ContainsKey(category))
                    {
                        exerciseDict[category].Add(e);
                    }
                    else
                    {
                        exerciseDict[category] = new List<Exercise> { e };
                    }
                }
            }

            var randomExercises = new HashSet<Exercise>();
            var random = new Random();

            foreach (var category in exParams.ExerciseCategories)
            {
                if (exerciseDict.ContainsKey(category))
                {
                    var idList = exerciseDict[category];

                    for (int i = 0; i < exParams.NumExercisesPerCategory; i++)
                    {
                        if (idList.Count == 0)
                        {
                            break;
                        }

                        var randomIndex = random.Next(idList.Count);
                        randomExercises.Add(idList[randomIndex]);
                        idList.RemoveAt(randomIndex);
                    }
                }
            }

            var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(randomExercises);

            return exercisesToReturn;
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