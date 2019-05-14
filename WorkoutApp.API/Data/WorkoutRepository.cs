using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly DataContext context;


        public WorkoutRepository(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<List<WorkoutPlan>> GetWorkoutPlansForUser(int userId)
        {
            return await context.WorkoutPlans.Include(p => p.User).Include(p => p.Workouts)
                .ThenInclude(w => w.ExerciseGroups)
                .ThenInclude(eg => eg.Exercise)
                .ThenInclude(e => e.PrimaryMuscle)
                .Where(p => p.User.Id == userId).ToListAsync();
        }

        public async Task<PagedList<ExerciseForReturnDto>> GetExercises(ExerciseQueryParams exParams)
        {
            IQueryable<ExerciseForReturnDto> exercises = context.Exercises.Select(e => new ExerciseForReturnDto { Id = e.Id, Name = e.Name });

            return await PagedList<ExerciseForReturnDto>.CreateAsync(exercises, exParams.PageNumber, exParams.PageSize);
        }

        public async Task<ExerciseForReturnDto> GetExercise(int exerciseId)
        {
            return await context.Exercises.Select(e => new ExerciseForReturnDto { Id = e.Id, Name = e.Name }).FirstOrDefaultAsync(e => e.Id == exerciseId);
        }

        public async Task<PagedList<ExerciseForReturnDetailedDto>> GetExercisesDetailed(ExerciseQueryParams exParams)
        {
            IQueryable<Exercise> exerciseQuery = context.Exercises;

            if (exParams.ExerciseCategoryId.Count > 0)
            {
                exerciseQuery = exerciseQuery.Where(ex => ex.ExerciseCategorys.Any(ec => exParams.ExerciseCategoryId.Contains(ec.ExerciseCategoryId)));
            }

            if (exParams.EquipmentId.Count > 0)
            {
                exerciseQuery = exerciseQuery.Where(ex => ex.Equipment.Any(eq => exParams.EquipmentId.Contains(eq.EquipmentId)));
            }

            IQueryable<ExerciseForReturnDetailedDto> exercises = exerciseQuery
                .Select( e => new ExerciseForReturnDetailedDto { 
                    Id = e.Id,
                    Name = e.Name,
                    PrimaryMuscle = e.PrimaryMuscle,
                    SecondaryMuscle = e.SecondaryMuscle,
                    ExerciseSteps = e.ExerciseSteps.Select(es => new ExerciseStepForReturnDto{
                        ExerciseStepNumber = es.ExerciseStepNumber,
                        Description = es.Description
                    }).ToList(),
                    Equipment = e.Equipment.Select(eq => new EquipmentForReturnDto { 
                        Id = eq.EquipmentId, 
                        Name = eq.Equipment.Name
                    }).ToList(),
                    ExerciseCategorys = e.ExerciseCategorys.Select(ec => new ExerciseCategoryForReturnDto { 
                        Id = ec.ExerciseCategoryId,
                        Name = ec.ExerciseCategory.Name
                    }).ToList()
                });

            return await PagedList<ExerciseForReturnDetailedDto>.CreateAsync(exercises, exParams.PageNumber, exParams.PageSize);
        }

        public async Task<ExerciseForReturnDetailedDto> GetExerciseDetailed(int exerciseId)
        {
            ExerciseForReturnDetailedDto exercise = await context.Exercises
                .Select( e => new ExerciseForReturnDetailedDto { 
                    Id = e.Id,
                    Name = e.Name,
                    PrimaryMuscle = e.PrimaryMuscle,
                    SecondaryMuscle = e.SecondaryMuscle,
                    ExerciseSteps = e.ExerciseSteps.Select(es => new ExerciseStepForReturnDto{
                        ExerciseStepNumber = es.ExerciseStepNumber,
                        Description = es.Description
                    }).ToList(),
                    Equipment = e.Equipment.Select(eq => new EquipmentForReturnDto { 
                        Id = eq.EquipmentId, 
                        Name = eq.Equipment.Name
                    }).ToList(),
                    ExerciseCategorys = e.ExerciseCategorys.Select(ec => new ExerciseCategoryForReturnDto { 
                        Id = ec.ExerciseCategoryId,
                        Name = ec.ExerciseCategory.Name
                    }).ToList()
                }).FirstOrDefaultAsync(e => e.Id == exerciseId);

            return exercise;
        }

        public async Task<List<EquipmentForReturnDto>> GetEquipmentForExercise(int exerciseId)
        {
            Exercise exercise = await context.Exercises.Include(e => e.Equipment).FirstOrDefaultAsync(e => e.Id == exerciseId);

            IEnumerable<int> equipmentIds = exercise.Equipment.Select(i => i.EquipmentId);

            return await context.Equipment.Where(eq => equipmentIds.Contains(eq.Id)).Select(eq => new EquipmentForReturnDto { Id = eq.Id, Name = eq.Name } ).ToListAsync();
        }

        public async Task<List<ExerciseCategoryForReturnDto>> GetExerciseCategories()
        {
            return await context.ExerciseCategorys.Select(ec => new ExerciseCategoryForReturnDto { Id = ec.Id, Name = ec.Name }).ToListAsync();
        }

        public async Task<PagedList<EquipmentForReturnDto>> GetExerciseEquipment(PaginationParams pgParams)
        {
            IQueryable<EquipmentForReturnDto> equipment = context.Equipment.Select(eq => new EquipmentForReturnDto {
                Id = eq.Id,
                Name = eq.Name
            });

            return await PagedList<EquipmentForReturnDto>.CreateAsync(equipment, pgParams.PageNumber, pgParams.PageSize);
        }

        public async Task<EquipmentForReturnDto> GetSingleExerciseEquipment(int id)
        {
            return await context.Equipment.Select(eq => new EquipmentForReturnDto {
                Id = eq.Id,
                Name = eq.Name
            }).FirstOrDefaultAsync(eq => eq.Id == id);
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}