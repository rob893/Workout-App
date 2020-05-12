using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    public class MusclesController : ControllerBase
    {
        private readonly MuscleRepository muscleRepository;
        private readonly ExerciseRepository exerciseRepository;
        private readonly IMapper mapper;


        public MusclesController(MuscleRepository muscleRepository, ExerciseRepository exerciseRepository, IMapper mapper)
        {
            this.muscleRepository = muscleRepository;
            this.exerciseRepository = exerciseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<MuscleForReturnDto>>> GetMusclesAsync([FromQuery] CursorPaginationParams searchParams)
        {
            var muscles = await muscleRepository.SearchAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<MuscleForReturnDto>.CreateFrom(muscles, mapper.Map<IEnumerable<MuscleForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<CursorPaginatedResponse<MuscleForReturnDetailedDto>>> GetMusclesDetailedAsync([FromQuery] CursorPaginationParams searchParams)
        {
            var muscles = await muscleRepository.SearchDetailedAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<MuscleForReturnDetailedDto>.CreateFrom(muscles, mapper.Map<IEnumerable<MuscleForReturnDetailedDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}", Name = "GetMuscle")]
        public async Task<ActionResult<MuscleForReturnDto>> GetMuscleAsync(int id)
        {
            var muscle = await muscleRepository.GetByIdAsync(id);

            if (muscle == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"Muscle with id {id} does not exist.", 404, Request));
            }

            var muscleToReturn = mapper.Map<MuscleForReturnDto>(muscle);

            return Ok(muscleToReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<MuscleForReturnDetailedDto>> GetMuscleDetailedAsync(int id)
        {
            var muscle = await muscleRepository.GetByIdDetailedAsync(id);

            if (muscle == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"Muscle with id {id} does not exist.", 404, Request));
            }

            var muscleToReturn = mapper.Map<MuscleForReturnDetailedDto>(muscle);

            return Ok(muscleToReturn);
        }

        [HttpGet("{id}/primaryExercises")]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseForReturnDto>>> GetPrimaryExercisesForMuscleAsync(int id, [FromQuery] CursorPaginationParams searchParams)
        {
            var exerciseSearchParams = new ExerciseSearchParams
            {
                First = searchParams.First,
                After = searchParams.After,
                Last = searchParams.Last,
                Before = searchParams.Before,
                IncludeTotal = searchParams.IncludeTotal,
                PrimaryMuscleId = new List<int> { id }
            };

            var exercises = await exerciseRepository.SearchAsync(exerciseSearchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseForReturnDto>.CreateFrom(exercises, mapper.Map<IEnumerable<ExerciseForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}/secondaryExercises")]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseForReturnDto>>> GetSecondaryExercisesForMuscleAsync(int id, [FromQuery] CursorPaginationParams searchParams)
        {
            var exerciseSearchParams = new ExerciseSearchParams
            {
                First = searchParams.First,
                After = searchParams.After,
                Last = searchParams.Last,
                Before = searchParams.Before,
                IncludeTotal = searchParams.IncludeTotal,
                SecondaryMuscleId = new List<int> { id }
            };

            var exercises = await exerciseRepository.SearchAsync(exerciseSearchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseForReturnDto>.CreateFrom(exercises, mapper.Map<IEnumerable<ExerciseForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpPost]
        public async Task<ActionResult<MuscleForReturnDto>> CreateMuscleAsync([FromBody] MuscleForCreateDto muscleForCreate)
        {
            var newMuscle = mapper.Map<Muscle>(muscleForCreate);
            muscleRepository.Add(newMuscle);

            var saveResult = await muscleRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to create muscle.", 400, Request));
            }

            var muscleToReturn = mapper.Map<MuscleForReturnDto>(newMuscle);

            return CreatedAtRoute("GetMuscle", new { id = newMuscle.Id }, muscleToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMuscleAsync(int id)
        {
            var muscle = await muscleRepository.GetByIdAsync(id);

            if (muscle == null)
            {
                return NotFound();
            }

            muscleRepository.Delete(muscle);

            if (!await muscleRepository.SaveAllAsync())
            {
                return BadRequest(new ProblemDetailsWithErrors("Failed to delete the muscle.", 400, Request));
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<MuscleForReturnDto>> UpdateMuscleAsync(int id, [FromBody] JsonPatchDocument<Muscle> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var muscle = await muscleRepository.GetByIdAsync(id);

            if (muscle == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(muscle);

            var saveResult = await muscleRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
            }

            var muscleToReturn = mapper.Map<MuscleForReturnDto>(muscle);

            return Ok(muscleToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MuscleForReturnDto>> UpdateMusclePutAsync(int id, [FromBody] MuscleForUpdateDto updateDto)
        {
            var muscle = await muscleRepository.GetByIdAsync(id);

            if (muscle == null)
            {
                return NotFound();
            }

            mapper.Map(updateDto, muscle);

            var saveResult = await muscleRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
            }

            var musclesToReturn = mapper.Map<MuscleForReturnDto>(muscle);

            return Ok(musclesToReturn);
        }
    }
}