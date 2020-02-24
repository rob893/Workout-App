using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Data;
using WorkoutApp.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Data.Repositories;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScheduledWorkoutsController : ControllerBase
    {
        private readonly ScheduledWorkoutRepository scheduledWorkoutRepository;
        private readonly IMapper mapper;

        
        public ScheduledWorkoutsController(ScheduledWorkoutRepository scheduledWorkoutRepository, IMapper mapper)
        {
            this.scheduledWorkoutRepository = scheduledWorkoutRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetScheduledWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> GetScheduledWorkout(int id)
        {
            var workout = await repo.GetScheduledUserWorkoutAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateScheduledWorkout([FromBody] ScheduledWorkoutForCreationDto newWorkoutDto)
        {
            if (await repo.GetWorkoutAsync(newWorkoutDto.WorkoutId) == null)
            {
                return BadRequest(new ProblemDetailsWithErrors("Invalid workout id!", 400, Request));
            }

            var newWorkout = mapper.Map<ScheduledWorkout>(newWorkoutDto);

            newWorkout.ScheduledByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            repo.Add<ScheduledWorkout>(newWorkout);

            if (await repo.SaveAllAsync())
            {
                var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(newWorkout);

                return CreatedAtAction(nameof(GetScheduledWorkout), new { id = newWorkout.Id }, workoutToReturn);
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not save the new workout.", 400, Request));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSceduledWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetScheduledWorkoutAsync(id, w => w.AdHocExercises);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.ScheduledByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            repo.DeleteRange<ExerciseGroup>(workout.AdHocExercises);
            scheduledWorkoutRepository.Delete(workout);

            if (await repo.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not delete scheduled workout!", 400, Request));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> UpdateScheduledWorkout(int id, [FromBody] JsonPatchDocument<ScheduledWorkout> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var workout = await repo.GetScheduledUserWorkoutAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.ScheduledByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            patchDoc.ApplyTo(workout);

            if (await repo.SaveAllAsync())
            {
                var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

                return Ok(workoutToReturn);
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
        }

        [HttpPatch("{id}/startWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> StartWorkout(int id)
        {
            var workout = await repo.GetScheduledUserWorkoutAsync(id);

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

            if (await repo.SaveAllAsync())
            {
                var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

                return Ok(workoutToReturn);
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not start workout.", 400, Request));
        }

        [HttpPatch("{id}/completeWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> CompleteWorkout(int id)
        {
            var workout = await repo.GetScheduledUserWorkoutAsync(id);

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

            if (await repo.SaveAllAsync())
            {
                var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

                return Ok(workoutToReturn);
            }

            return BadRequest(new ProblemDetailsWithErrors("Could not complete workout.", 400, Request));
        }
    }
}