using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;
using WorkoutApp.API.Data.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly WorkoutRepository workoutRepository;
        private readonly IMapper mapper;


        public WorkoutsController(WorkoutRepository workoutRepository, IMapper mapper)
        {
            this.workoutRepository = workoutRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutForReturnDto>>> GetWorkoutsAsync([FromQuery] WorkoutSearchParams searchParams)
        {
            var workouts = await workoutRepository.GetWorkoutsAsync(searchParams);
            Response.AddPagination(workouts);
            var workoutsToReturn = mapper.Map<IEnumerable<WorkoutForReturnDto>>(workouts);

            return Ok(workoutsToReturn);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<WorkoutForReturnDetailedDto>>> GetWorkoutsDetailedAsync([FromQuery] WorkoutSearchParams searchParams)
        {
            var workouts = await workoutRepository.GetWorkoutsDetailedAsync(searchParams);
            Response.AddPagination(workouts);
            var workoutsToReturn = mapper.Map<IEnumerable<WorkoutForReturnDetailedDto>>(workouts);

            return Ok(workoutsToReturn);
        }

        [HttpGet("{id}", Name = "GetWorkout")]
        public async Task<ActionResult<WorkoutForReturnDto>> GetWorkoutAsync(int id)
        {
            var workout = await workoutRepository.GetWorkoutAsync(id);

            if (workout == null)
            {
                return NoContent();
            }

            var workoutToReturn = mapper.Map<WorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<WorkoutForReturnDetailedDto>> GetWorkoutDetailedAsync(int id)
        {
            var workout = await workoutRepository.GetWorkoutDetailedAsync(id);

            if (workout == null)
            {
                return NoContent();
            }

            var workoutToReturn = mapper.Map<WorkoutForReturnDetailedDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutForReturnDetailedDto>> CreateWorkoutAsync([FromBody] WorkoutForCreationDto newWorkout)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var workout = mapper.Map<Workout>(newWorkout);

            workout.CreatedOnDate = DateTime.Now;
            workout.LastModifiedDate = DateTime.Now;
            workout.CreatedByUserId = userId;

            workoutRepository.Add(workout);
            var saveResults = await workoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to create workout.", 400, Request));
            }

            var workoutForReturn = mapper.Map<WorkoutForReturnDetailedDto>(workout);

            return CreatedAtRoute("GetWorkout", new { id = workout.Id }, workoutForReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkoutAsync(int id)
        {
            var workoutToDelete = await workoutRepository.GetWorkoutAsync(id);

            if (workoutToDelete == null)
            {
                return NoContent();
            }

            if (workoutToDelete.CreatedByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            workoutRepository.Delete(workoutToDelete);
            var saveResults = await workoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to delete workout.", 400, Request));
            }

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<WorkoutForReturnDetailedDto>> UpdateWorkoutAsync(int id, [FromBody] JsonPatchDocument<Workout> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var workout = await workoutRepository.GetWorkoutDetailedAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(workout);

            var saveResult = await workoutRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
            }

            var workoutToReturn = mapper.Map<WorkoutForReturnDetailedDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkoutForReturnDetailedDto>> UpdateWorkoutPutAsync(int id, [FromBody] WorkoutForUpdateDto updateDto)
        {
            var workout = await workoutRepository.GetWorkoutDetailedAsync(id);
            mapper.Map(updateDto, workout);

            workout.LastModifiedDate = DateTime.Now;

            var saveResult = await workoutRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
            }

            var workoutToReturn = mapper.Map<WorkoutForReturnDetailedDto>(workout);

            return Ok(workoutToReturn);
        }
    }
}