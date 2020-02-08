using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Data;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using WorkoutApp.API.Helpers;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScheduledWorkoutsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWorkoutRepository repo;

        
        public ScheduledWorkoutsController(IMapper mapper, IWorkoutRepository repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet("{id}", Name = "GetScheduledWorkout")]
        public async Task<ActionResult<ScheduledWoForReturnDto>> GetScheduledWorkout(int id)
        {
            var workout = await repo.GetScheduledUserWorkoutAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var workoutToReturn = mapper.Map<ScheduledWoForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateScheduledWorkout([FromBody] ScheduledWoForCreationDto newWorkoutDto)
        {
            if (await repo.GetWorkoutAsync(newWorkoutDto.WorkoutId) == null)
            {
                return BadRequest(new ProblemDetailsWithErrors("Invalid workout id!", 400, Request));
            }

            ScheduledUserWorkout newWorkout = mapper.Map<ScheduledUserWorkout>(newWorkoutDto);

            newWorkout.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            repo.Add<ScheduledUserWorkout>(newWorkout);

            if (await repo.SaveAllAsync())
            {
                var workoutToReturn = mapper.Map<ScheduledWoForReturnDto>(newWorkout);

                return CreatedAtAction(nameof(GetScheduledWorkout), new { id = newWorkout.Id }, workoutToReturn);
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not save the new workout.", 400, Request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSceduledWorkout(int id)
        {
            ScheduledUserWorkout workout = await repo.GetScheduledUserWorkoutAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            repo.DeleteRange<ExerciseGroup>(workout.AdHocExercises);
            repo.Delete<ScheduledUserWorkout>(workout);

            if (await repo.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not delete scheduled workout!", 400, Request));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ScheduledWoForReturnDto>> UpdateScheduledWorkout(int id, [FromBody] JsonPatchDocument<ScheduledUserWorkout> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            ScheduledUserWorkout workout = await repo.GetScheduledUserWorkoutAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            patchDoc.ApplyTo(workout);

            if (await repo.SaveAllAsync())
            {
                var workoutToReturn = mapper.Map<ScheduledWoForReturnDto>(workout);

                return Ok(workoutToReturn);
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
        }

        [HttpPatch("{id}/startWorkout")]
        public async Task<ActionResult<ScheduledWoForReturnDto>> StartWorkout(int id)
        {
            ScheduledUserWorkout workout = await repo.GetScheduledUserWorkoutAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (workout.StartedDateTime != null)
            {
                return BadRequest(new ProblemDetailsWithErrors("This workout has already been started!", 400, Request));
            }

            workout.StartedDateTime = DateTime.Now;

            if (await repo.SaveAllAsync())
            {
                var workoutToReturn = mapper.Map<ScheduledWoForReturnDto>(workout);

                return Ok(workoutToReturn);
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not start workout.", 400, Request));
        }

        [HttpPatch("{id}/completeWorkout")]
        public async Task<ActionResult<ScheduledWoForReturnDto>> CompleteWorkout(int id)
        {
            var workout = await repo.GetScheduledUserWorkoutAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
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

            if (await repo.SaveAllAsync())
            {
                var workoutToReturn = mapper.Map<ScheduledWoForReturnDto>(workout);

                return Ok(workoutToReturn);
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not complete workout.", 400, Request));
        }
    }
}