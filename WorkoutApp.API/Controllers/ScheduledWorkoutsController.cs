using System.Linq;
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
        public async Task<ActionResult<ScheduledWorkoutForReturnDetailedDto>> GetScheduledWorkoutDetailedAsync(int id)
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

            workout.StartedDateTime = DateTimeOffset.UtcNow;
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

            workout.CompletedDateTime = DateTimeOffset.UtcNow;

            if (workout.StartedDateTime == null)
            {
                workout.StartedDateTime = DateTimeOffset.UtcNow;
            }

            var saveResults = await scheduledWorkoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not complete workout.", 400, Request));
            }

            var workoutToReturn = mapper.Map<ScheduledWorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpGet("{id}/attendees")]
        public async Task<ActionResult<IEnumerable<UserForReturnDto>>> GetScheduledWorkoutAttendeesAsync(int id, [FromQuery] PaginationParams searchParams)
        {
            var workout = await scheduledWorkoutRepository.GetByIdAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var attendees = await scheduledWorkoutRepository.GetScheduledWorkoutAttendeesAsync(id, searchParams);
            Response.AddPagination(attendees);
            var attendeesToReturn = mapper.Map<IEnumerable<UserForReturnDto>>(attendees);

            return Ok(attendeesToReturn);
        }

        [HttpGet("{id}/attendees/{userId}", Name = "GetScheduledWorkoutAttendee")]
        public async Task<ActionResult<UserForReturnDto>> GetScheduledWorkoutAttendeeAsync(int id, int userId)
        {
            var workout = await scheduledWorkoutRepository.GetByIdDetailedAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            var attendee = workout.Attendees.Where(a => a.UserId == userId).FirstOrDefault();

            if (attendee == null)
            {
                return NotFound();
            }

            var attendeeToReturn = mapper.Map<UserForReturnDto>(attendee.User);

            return Ok(attendeeToReturn);
        }

        [HttpPost("{id}/attendees")]
        public async Task<ActionResult<ScheduledWorkoutUserForReturnDto>> AddAttendeeToScheduledWorkoutAsync(int id, [FromBody] ScheduledWorkoutUserForCreationDto attendeeToAdd)
        {
            var workout = await scheduledWorkoutRepository.GetByIdAsync(id, w => w.Attendees);

            if (workout == null)
            {
                return NotFound();
            }

            if (int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) != workout.ScheduledByUserId)
            {
                return Unauthorized();
            }

            if (workout.Attendees.Where(a => a.UserId == attendeeToAdd.UserId).FirstOrDefault() != null)
            {
                return BadRequest(new ProblemDetailsWithErrors($"User {attendeeToAdd.UserId} is already attending this workout.", 400, Request));
            }

            var newAttendee = new ScheduledWorkoutUser
            {
                UserId = attendeeToAdd.UserId,
                ScheduledWorkoutId = id
            };

            workout.Attendees.Add(newAttendee);

            var saveResults = await scheduledWorkoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not add attendee.", 400, Request));
            }
            
            var attendeeToReturn = mapper.Map<ScheduledWorkoutUserForReturnDto>(newAttendee);

            return CreatedAtRoute("GetScheduledWorkoutAttendee", new { userId = attendeeToReturn.UserId }, attendeeToReturn);
        }

        [HttpDelete("{id}/attendees/{userId}")]
        public async Task<ActionResult> DeleteScheduledWorkoutAttendeeAsync(int id, int userId)
        {
            var workout = await scheduledWorkoutRepository.GetByIdAsync(id, w => w.Attendees);

            if (workout == null)
            {
                return NotFound();
            }

            var tokenUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (tokenUser != workout.ScheduledByUserId || tokenUser != userId)
            {
                return Unauthorized();
            }

            var attendeeToRemove = workout.Attendees.Where(a => a.UserId == userId).FirstOrDefault();

            if (attendeeToRemove == null)
            {
                return BadRequest(new ProblemDetailsWithErrors($"User {userId} was not attending this workout.", 400, Request));
            }

            workout.Attendees.Remove(attendeeToRemove);

            var saveResults = await scheduledWorkoutRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not remove attendee.", 400, Request));
            }
            
            return NoContent();
        }
    }
}