using System.Linq;
using System.Collections.Generic;
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
        private readonly EquipmentRepository equipmentRepository;
        private readonly ExerciseCategoryRepository exerciseCategoryRepository;
        private readonly IMapper mapper;


        public ExercisesController(ExerciseRepository exerciseRepository, MuscleRepository muscleRepository, 
            EquipmentRepository equipmentRepository, ExerciseCategoryRepository exerciseCategoryRepository, IMapper mapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.muscleRepository = muscleRepository;
            this.equipmentRepository = equipmentRepository;
            this.exerciseCategoryRepository = exerciseCategoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDto>>> GetExercisesAsync([FromQuery] ExerciseSearchParams searchParams)
        {
            var exercises = await exerciseRepository.SearchAsync(searchParams);
            Response.AddPagination(exercises);
            var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDto>>(exercises);

            return Ok(exercisesToReturn);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> GetExercisesDetailedAsync([FromQuery] ExerciseSearchParams searchParams)
        {
            var exercises = await exerciseRepository.SearchDetailedAsync(searchParams);
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
            var exercise = await exerciseRepository.GetByIdAsync(id);
            var exerciseToReturn = mapper.Map<ExerciseForReturnDto>(exercise);

            return Ok(exerciseToReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<ExerciseForReturnDetailedDto>> GetExerciseDetailedAsync(int id)
        {
            var exercise = await exerciseRepository.GetByIdDetailedAsync(id);
            var exerciseToReturn = mapper.Map<ExerciseForReturnDetailedDto>(exercise);

            return Ok(exerciseToReturn);
        }

        [HttpGet("{id}/equipment")]
        public async Task<ActionResult<IEnumerable<EquipmentForReturnDto>>> GetEquipmentForExerciseAsync(int id, [FromQuery] PaginationParams searchParams)
        {
            var equipmentSearchParams = new EquipmentSearchParams
            {
                PageNumber = searchParams.PageNumber,
                PageSize = searchParams.PageSize,
                ExerciseIds = new List<int> { id }
            };

            var equipment = await equipmentRepository.SearchAsync(equipmentSearchParams);
            Response.AddPagination(equipment);
            var equipmentToReturn = mapper.Map<IEnumerable<EquipmentForReturnDto>>(equipment);

            return Ok(equipmentToReturn);
        }

        [HttpGet("{id}/exerciseCategories")]
        public async Task<ActionResult<IEnumerable<ExerciseCategoryForReturnDto>>> GetExerciseCategoriesForExerciseAsync(int id, [FromQuery] PaginationParams searchParams)
        {
            var categorySearchParams = new ExerciseCategorySearchParams
            {
                PageNumber = searchParams.PageNumber,
                PageSize = searchParams.PageSize,
                ExerciseId = new List<int> { id }
            };

            var categories = await exerciseCategoryRepository.SearchAsync(categorySearchParams);
            Response.AddPagination(categories);
            var categoriesToReturn = mapper.Map<IEnumerable<ExerciseCategoryForReturnDto>>(categories);

            return Ok(categoriesToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseForReturnDto>> CreateExercise([FromBody] ExerciseForCreationDto exercise)
        {
            var newExercise = mapper.Map<Exercise>(exercise);

            if (exercise.PrimaryMuscleId != null)
            {
                var primaryMuscle = await muscleRepository.GetByIdAsync(exercise.PrimaryMuscleId.Value);
                newExercise.PrimaryMuscle = primaryMuscle;
            }

            if (exercise.SecondaryMuscleId != null)
            {
                var secondaryMuscle = await muscleRepository.GetByIdAsync(exercise.SecondaryMuscleId.Value);
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
            var exercise = await exerciseRepository.GetByIdAsync(id);

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

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ExerciseForReturnDetailedDto>> UpdateExerciseAsync(int id, [FromBody] JsonPatchDocument<Exercise> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var exercise = await exerciseRepository.GetByIdDetailedAsync(id);

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