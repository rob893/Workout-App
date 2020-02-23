using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data;
using WorkoutApp.API.Data.Providers;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly IMapper mapper;
        private readonly ExerciseProvider exerciseProvider;


        public ExercisesController(IWorkoutRepository repo, IMapper mapper, ExerciseProvider exerciseProvider)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.exerciseProvider = exerciseProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetExercises([FromQuery] ExerciseParams exParams)
        {
            List<ExerciseForReturnDto> exercisesToReturn = await exerciseProvider.GetExercises();

            return Ok(exercisesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExercise(int id)
        {
            ExerciseForReturnDto exToReturn = await exerciseProvider.GetExercise(id);

            return Ok(exToReturn);
        }

        [HttpGet("{exerciseId}/detailed")]
        public async Task<IActionResult> GetExerciseDetailed(int exerciseId)
        {
            Exercise exercise = await repo.GetExerciseAsync(exerciseId);

            ExerciseForReturnDetailedDto exToReturn = mapper.Map<ExerciseForReturnDetailedDto>(exercise);

            return Ok(exToReturn);
        }

        [HttpGet("detailed")]
        public async Task<IActionResult> GetExercisesDetailed([FromQuery] ExerciseParams exParams)
        {
            PagedList<ExerciseForReturnDetailedDto> exercises = await exerciseProvider.GetExercisesDetailed(exParams);

            Response.AddPagination(exercises.PageNumber, exercises.PageSize, exercises.TotalItems, exercises.TotalPages);

            return Ok(exercises);
        }

        [HttpGet("detailed/random")]
        public async Task<IActionResult> GetRandomExercisesDetailed([FromQuery] RandomExercisesParams exParams)
        {
            IEnumerable<Exercise> exercises;

            if (exParams.Favorites)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                exercises = await exerciseProvider.GetRandomFavoriteExercisesForUserAsync(exParams.NumExercises.Value, userId, exParams.ExerciseCategory);
            }
            else
            {
                exercises = await exerciseProvider.GetRandomExercisesAsync(exParams.NumExercises.Value, exParams.ExerciseCategory);
            }

            var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(exercises);

            return Ok(exercisesToReturn);
        }

        [HttpGet("{exerciseId}/equipment")]
        public async Task<IActionResult> GetEquipmentForExercise(int exerciseId, [FromQuery] EquipmentParams eqParams)
        {
            eqParams.ExerciseIds.Add(exerciseId);

            PagedList<Equipment> equipment = await repo.GetExerciseEquipmentAsync(eqParams);

            Response.AddPagination(equipment.PageNumber, equipment.PageSize, equipment.TotalItems, equipment.TotalPages);

            IEnumerable<EquipmentForReturnDto> equipmentForReturn = mapper.Map<IEnumerable<EquipmentForReturnDto>>(equipment);

            return Ok(equipmentForReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreationDto exercise)
        {
            Exercise newExercise = mapper.Map<Exercise>(exercise);

            if (exercise.PrimaryMuscleId != null)
            {
                newExercise.PrimaryMuscle = new Muscle
                {
                    Id = exercise.PrimaryMuscleId.Value
                };
            }

            if (exercise.SecondaryMuscleId != null)
            {
                newExercise.SecondaryMuscle = new Muscle
                {
                    Id = exercise.SecondaryMuscleId.Value
                };
            }

            if (exercise.EquipmentIds != null)
            {
                newExercise.Equipment = new List<EquipmentExercise>();
                exercise.EquipmentIds.ForEach(id =>
                    newExercise.Equipment.Add(new EquipmentExercise
                    {
                        EquipmentId = id
                    })
                );
            }

            if (exercise.ExerciseCategoryIds != null)
            {
                newExercise.ExerciseCategorys = new List<ExerciseCategoryExercise>();
                exercise.ExerciseCategoryIds.ForEach(id =>
                    newExercise.ExerciseCategorys.Add(new ExerciseCategoryExercise
                    {
                        ExerciseCategoryId = id
                    })
                );
            }

            repo.Add<Exercise>(newExercise);

            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetExercise), new { id = newExercise.Id }, newExercise);
            }

            return BadRequest("Could not create exercise.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            Exercise exercise = await repo.GetExerciseAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            repo.Delete<Exercise>(exercise);

            if (await repo.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest("Could not delete exercise.");
        }
    }
}