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
using Microsoft.AspNetCore.JsonPatch;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly ExerciseRepository exerciseRepository;
        private readonly MuscleRepository muscleRepository;
        private readonly IMapper mapper;


        public ExercisesController(ExerciseRepository exerciseRepository, MuscleRepository muscleRepository, IMapper mapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.muscleRepository = muscleRepository;
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

        [HttpGet("detailed/random")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> GetRandomExercisesDetailedAsync([FromQuery] RandomExerciseSearchParams searchParams)
        {
            var exercises = await exerciseRepository.GetRandomExercisesDetailedAsync(searchParams);
            var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(exercises);

            return Ok(exercisesToReturn);
        }

        [HttpGet("{id}", Name = "GetExercise")]
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

        [HttpPost]
        public async Task<ActionResult<ExerciseForReturnDto>> CreateExercise([FromBody] ExerciseForCreationDto exercise)
        {
            var newExercise = mapper.Map<Exercise>(exercise);

            if (exercise.PrimaryMuscleId != null)
            {
                var primaryMuscle = await muscleRepository.GetMuscleAsync(exercise.PrimaryMuscleId.Value);
                newExercise.PrimaryMuscle = primaryMuscle;
            }

            if (exercise.SecondaryMuscleId != null)
            {
                var secondaryMuscle = await muscleRepository.GetMuscleAsync(exercise.SecondaryMuscleId.Value);
                newExercise.SecondaryMuscle = secondaryMuscle;
            }

            if (exercise.EquipmentIds != null)
            {
                newExercise.Equipment = exercise.EquipmentIds.Select(eqId => new EquipmentExercise
                {
                    EquipmentId = eqId
                }).ToList();
            }

            if (exercise.ExerciseCategoryIds != null)
            {
                newExercise.ExerciseCategorys = exercise.ExerciseCategoryIds.Select(ecId => new ExerciseCategoryExercise
                {
                    ExerciseCategoryId = ecId
                }).ToList();
            }

            exerciseRepository.Add(newExercise);
            var saveResult = await exerciseRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to create exercise.", 400, Request));
            }

            var exerciseToReturn = mapper.Map<ExerciseForReturnDto>(newExercise);

            return CreatedAtRoute("GetExercise", new { id = newExercise.Id }, exerciseToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExercise(int id)
        {
            var exercise = await exerciseRepository.GetExerciseAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            exerciseRepository.Delete(exercise);
            var saveResult = await exerciseRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to delete exercise.", 400, Request));
            }

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ExerciseForReturnDetailedDto>> UpdateExerciseAsync(int id, [FromBody] JsonPatchDocument<Exercise> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var exercise = await exerciseRepository.GetExerciseDetailedAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(exercise);

            var saveResult = await exerciseRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
            }

            var exerciseToReturn = mapper.Map<ExerciseForReturnDetailedDto>(exercise);

            return Ok(exerciseToReturn);
        }
    }
}