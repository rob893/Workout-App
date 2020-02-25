using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Data.Repositories;
using System.Collections.Generic;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScheduledWorkoutsController : ControllerBase
    {
        private readonly ScheduledWorkoutRepository scheduledWorkoutRepository;
        private readonly WorkoutRepository workoutRepository;
        private readonly IMapper mapper;

        
        public ScheduledWorkoutsController(ScheduledWorkoutRepository scheduledWorkoutRepository, IMapper mapper)
        {
            this.scheduledWorkoutRepository = scheduledWorkoutRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduledWorkoutForReturnDto>>> GetScheduledWorkoutsAsync([FromBody] ScheduledWorkoutSearchParams searchParams)
        {
            var workouts = await scheduledWorkoutRepository.SearchAsync(searchParams);
            Response.AddPagination(workouts);
            var workoutsToReturn = mapper.Map<IEnumerable<ScheduledWorkoutForReturnDto>>(workouts);

            return Ok(workoutsToReturn);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<ScheduledWorkoutForReturnDetailedDto>>> GetScheduledWorkoutsDetailedAsync([FromBody] ScheduledWorkoutSearchParams searchParams)
        {
            var workouts = await scheduledWorkoutRepository.SearchDetailedAsync(searchParams);
            Response.AddPagination(workouts);
            var workoutsToReturn = mapper.Map<IEnumerable<ScheduledWorkoutForReturnDetailedDto>>(workouts);

            return Ok(workoutsToReturn);
        }

        [HttpGet("{id}", Name = "GetScheduledWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> GetScheduledWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDetailedDto>> GetScheduledDetailedWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetDetailedAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDetailedDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> CreateScheduledWorkoutAsync([FromBody] ScheduledWorkoutForCreationDto newWorkoutDto)
        {
            if (await workoutRepository.GetAsync(newWorkoutDto.WorkoutId) == null)
            {
                return BadRequest(new ProblemDetailsWithErrors("Invalid workout id!", 400, Request));
            }

            var newWorkout = mapper.Map<ScheduledWorkout>(newWorkoutDto);

            newWorkout.ScheduledByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            scheduledWorkoutRepository.Add(newWorkout);
            var saveResults = await scheduledWorkoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not save the new workout.", 400, Request));
            }

            var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(newWorkout);

            return CreatedAtRoute("GetScheduledWorkout", new { id = newWorkout.Id }, workoutToReturn);        
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSceduledWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetAsync(id, w => w.AdHocExercises);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.ScheduledByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            scheduledWorkoutRepository.Delete(workout);
            var saveResults = await scheduledWorkoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not delete scheduled workout!", 400, Request));
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> UpdateScheduledWorkoutAsync(int id, [FromBody] JsonPatchDocument<ScheduledWorkout> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var workout = await scheduledWorkoutRepository.GetAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.ScheduledByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            patchDoc.ApplyTo(workout);
            var saveResults = await scheduledWorkoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
            }

            var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpPatch("{id}/startWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> StartWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.ScheduledByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (workout.StartedDateTime != null)
            {
                return BadRequest(new ProblemDetailsWithErrors("This workout has already been started!", 400, Request));
            }

            workout.StartedDateTime = DateTime.Now;
            var saveResults = await scheduledWorkoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not start workout.", 400, Request));
            }

            var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpPatch("{id}/completeWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> CompleteWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.ScheduledByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (workout.CompletedDateTime != null)
            {
                return BadRequest(new ProblemDetailsWithErrors("This workout has already been completed!", 400, Request));
            }

            workout.CompletedDateTime = DateTime.Now;

            if (workout.StartedDateTime == null)
            {
                workout.StartedDateTime = DateTime.Now;
            }

            var saveResults = await scheduledWorkoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not complete workout.", 400, Request));
            }

            var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }
    }
}