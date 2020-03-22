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
        private readonly IMapper mapper;


        public MusclesController(MuscleRepository muscleRepository, IMapper mapper)
        {
            this.muscleRepository = muscleRepository; 
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MuscleForReturnDto>>> GetMusclesAsync([FromQuery] PaginationParams searchParams)
        {
            var muscles = await muscleRepository.SearchAsync(searchParams);
            Response.AddPagination(muscles);
            var musclesToReturn = mapper.Map<IEnumerable<MuscleForReturnDto>>(muscles);

            return Ok(musclesToReturn);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<MuscleForReturnDetailedDto>>> GetMusclesDetailedAsync([FromQuery] PaginationParams searchParams)
        {
            var muscles = await muscleRepository.SearchDetailedAsync(searchParams);
            Response.AddPagination(muscles);
            var musclesToReturn = mapper.Map<IEnumerable<MuscleForReturnDetailedDto>>(muscles);

            return Ok(musclesToReturn);
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
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDto>>> GetPrimaryExercisesForMuscleAsync(int id, [FromQuery] ExerciseSearchParams searchParams)
        {
            var exercises = await muscleRepository.GetPrimaryExercisesForMuscleAsync(id, searchParams);
            Response.AddPagination(exercises);
            var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDto>>(exercises);
            
            return Ok(exercisesToReturn);
        }

        [HttpGet("{id}/secondaryExercises")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDto>>> GetSecondaryExercisesForMuscleAsync(int id, [FromQuery] ExerciseSearchParams searchParams)
        {
            var exercises = await muscleRepository.GetSecondaryExercisesForMuscleAsync(id, searchParams);
            Response.AddPagination(exercises);
            var exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDto>>(exercises);
            
            return Ok(exercisesToReturn);
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

            var musclesToReturn = mapper.Map<MuscleForReturnDto>(muscle);

            return Ok(musclesToReturn);
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