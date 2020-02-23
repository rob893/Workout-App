using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data.Repositories;
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
        private readonly ExerciseRepository exerciseRepository;
        private readonly IMapper mapper;


        public ExercisesController(ExerciseRepository exerciseRepository, IMapper mapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDto>>> GetExercisesAsync([FromQuery] ExerciseSearchParams searchParams)
        {
            var exercises = await exerciseRepository.GetExercisesAsync(searchParams);
            Response.AddPagination(exercises);
            var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDto>>(exercises);

            return Ok(exercisesToReturn);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> GetExercisesDetailedAsync([FromQuery] ExerciseSearchParams searchParams)
        {
            var exercises = await exerciseRepository.GetExercisesDetailedAsync(searchParams);
            Response.AddPagination(exercises);
            var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(exercises);

            return Ok(exercisesToReturn);
        }

        // [HttpGet("detailed/random")]
        // public async Task<ActionResult<ExerciseForReturnDetailedDto>> GetRandomExercisesDetailedAsync([FromQuery] RandomExerciseSearchParams searchParams)
        // {
        //     var exercises = await exerciseRepository.GetExercisesDetailedAsync(searchParams);
        //     exercises = exercises.Shuffle().Take(searchParams.NumExercises);
        //     Response.AddPagination(exercises);
        //     var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(exercises);

        //     return Ok(exercisesToReturn);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseForReturnDto>> GetExerciseAsync(int id)
        {
            var exercise = await exerciseRepository.GetExerciseAsync(id);
            var exerciseToReturn = mapper.Map<ExerciseForReturnDto>(exercise);

            return Ok(exerciseToReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<ExerciseForReturnDetailedDto>> GetExerciseDetailedAsync(int id)
        {
            var exercise = await exerciseRepository.GetExerciseDetailedAsync(id);
            var exerciseToReturn = mapper.Map<ExerciseForReturnDetailedDto>(exercise);

            return Ok(exerciseToReturn);
        }

        // [HttpPost]
        // public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreationDto exercise)
        // {
        //     Exercise newExercise = mapper.Map<Exercise>(exercise);

        //     if (exercise.PrimaryMuscleId != null)
        //     {
        //         newExercise.PrimaryMuscle = new Muscle
        //         {
        //             Id = exercise.PrimaryMuscleId.Value
        //         };
        //     }

        //     if (exercise.SecondaryMuscleId != null)
        //     {
        //         newExercise.SecondaryMuscle = new Muscle
        //         {
        //             Id = exercise.SecondaryMuscleId.Value
        //         };
        //     }

        //     if (exercise.EquipmentIds != null)
        //     {
        //         newExercise.Equipment = new List<EquipmentExercise>();
        //         exercise.EquipmentIds.ForEach(id =>
        //             newExercise.Equipment.Add(new EquipmentExercise
        //             {
        //                 EquipmentId = id
        //             })
        //         );
        //     }

        //     if (exercise.ExerciseCategoryIds != null)
        //     {
        //         newExercise.ExerciseCategorys = new List<ExerciseCategoryExercise>();
        //         exercise.ExerciseCategoryIds.ForEach(id =>
        //             newExercise.ExerciseCategorys.Add(new ExerciseCategoryExercise
        //             {
        //                 ExerciseCategoryId = id
        //             })
        //         );
        //     }

        //     repo.Add<Exercise>(newExercise);

        //     if (await repo.SaveAllAsync())
        //     {
        //         return CreatedAtAction(nameof(GetExercise), new { id = newExercise.Id }, newExercise);
        //     }

        //     return BadRequest("Could not create exercise.");
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteExercise(int id)
        // {
        //     Exercise exercise = await repo.GetExerciseAsync(id);

        //     if (exercise == null)
        //     {
        //         return NotFound();
        //     }

        //     repo.Delete<Exercise>(exercise);

        //     if (await repo.SaveAllAsync())
        //     {
        //         return Ok();
        //     }

        //     return BadRequest("Could not delete exercise.");
        // }
    }
}