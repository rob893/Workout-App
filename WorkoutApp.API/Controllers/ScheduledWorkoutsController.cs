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
        private readonly UserRepository userRepository;
        private readonly IMapper mapper;


        public ScheduledWorkoutsController(ScheduledWorkoutRepository scheduledWorkoutRepository, WorkoutRepository workoutRepository, UserRepository userRepository, IMapper mapper)
        {
            this.scheduledWorkoutRepository = scheduledWorkoutRepository;
            this.workoutRepository = workoutRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduledWorkoutForReturnDto>>> GetScheduledWorkoutsAsync([FromQuery] ScheduledWorkoutSearchParams searchParams)
        {
            var workouts = await scheduledWorkoutRepository.SearchAsync(searchParams);
            Response.AddPagination(workouts);
            var workoutsToReturn = mapper.Map<IEnumerable<ScheduledWorkoutForReturnDto>>(workouts);

            return Ok(workoutsToReturn);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<ScheduledWorkoutForReturnDetailedDto>>> GetScheduledWorkoutsDetailedAsync([FromQuery] ScheduledWorkoutSearchParams searchParams)
        {
            var workouts = await scheduledWorkoutRepository.SearchDetailedAsync(searchParams);
            Response.AddPagination(workouts);
            var workoutsToReturn = mapper.Map<IEnumerable<ScheduledWorkoutForReturnDetailedDto>>(workouts);

            return Ok(workoutsToReturn);
        }

        [HttpGet("{id}", Name = "GetScheduledWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> GetScheduledWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetByIdAsync(id);

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
            var workout = await scheduledWorkoutRepository.GetByIdDetailedAsync(id);

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
            var workoutToSchedule = await workoutRepository.GetByIdAsync(newWorkoutDto.WorkoutId);
            if (workoutToSchedule == null)
            {
                return BadRequest(new ProblemDetailsWithErrors("Invalid workout id.", 400, Request));
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await userRepository.GetByIdAsync(userId);

            var newWorkout = mapper.Map<ScheduledWorkout>(newWorkoutDto);
            newWorkout.Workout = workoutToSchedule;
            newWorkout.ScheduledByUserId = userId;
            newWorkout.ScheduledByUser = user;
            newWorkout.Attendees = new List<ScheduledWorkoutUser>
            {
                new ScheduledWorkoutUser
                {
                    User = user
                }
            };

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
            var workout = await scheduledWorkoutRepository.GetByIdAsync(id, w => w.AdHocExercises);

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

            var workout = await scheduledWorkoutRepository.GetByIdAsync(id);

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

        [HttpPut("{id}/startWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> StartWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetByIdAsync(id);

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

        [HttpPut("{id}/completeWorkout")]
        public async Task<ActionResult<ScheduledWorkoutForReturnDto>> CompleteWorkoutAsync(int id)
        {
            var workout = await scheduledWorkoutRepository.GetByIdAsync(id);

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