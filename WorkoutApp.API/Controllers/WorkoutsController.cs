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
        private readonly ExerciseGroupRepository exerciseGroupRepository;
        private readonly IMapper mapper;


        public WorkoutsController(WorkoutRepository workoutRepository, ExerciseGroupRepository exerciseGroupRepository, IMapper mapper)
        {
            this.workoutRepository = workoutRepository;
            this.exerciseGroupRepository = exerciseGroupRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<WorkoutForReturnDto>>> GetWorkoutsAsync([FromQuery] WorkoutSearchParams searchParams)
        {
            var workouts = await workoutRepository.SearchAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<WorkoutForReturnDto>.CreateFrom(workouts, mapper.Map<IEnumerable<WorkoutForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<CursorPaginatedResponse<WorkoutForReturnDetailedDto>>> GetWorkoutsDetailedAsync([FromQuery] WorkoutSearchParams searchParams)
        {
            var workouts = await workoutRepository.SearchDetailedAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<WorkoutForReturnDetailedDto>.CreateFrom(workouts, mapper.Map<IEnumerable<WorkoutForReturnDetailedDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}", Name = "GetWorkout")]
        public async Task<ActionResult<WorkoutForReturnDto>> GetWorkoutAsync(int id)
        {
            var workout = await workoutRepository.GetByIdAsync(id);

            if (workout == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"No workout with id {id} exists."));
            }

            var workoutToReturn = mapper.Map<WorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<WorkoutForReturnDetailedDto>> GetWorkoutDetailedAsync(int id)
        {
            var workout = await workoutRepository.GetByIdDetailedAsync(id);

            if (workout == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"No workout with id {id} exists."));
            }

            var workoutToReturn = mapper.Map<WorkoutForReturnDetailedDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpGet("{id}/exerciseGroups")]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseGroupForReturnDto>>> GetExerciseGroupsForWorkoutAsync(int id, [FromQuery] CursorPaginationParams searchParams)
        {
            var exerciseGroupSearchParams = new ExerciseGroupSearchParams
            {
                First = searchParams.First,
                After = searchParams.After,
                Last = searchParams.Last,
                Before = searchParams.Before,
                IncludeTotal = searchParams.IncludeTotal,
                WorkoutId = new List<int> { id }
            };

            var groups = await exerciseGroupRepository.SearchDetailedAsync(exerciseGroupSearchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseGroupForReturnDto>.CreateFrom(groups, mapper.Map<IEnumerable<ExerciseGroupForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutForReturnDetailedDto>> CreateWorkoutAsync([FromBody] WorkoutForCreationDto newWorkout)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var workout = mapper.Map<Workout>(newWorkout);

            workout.CreatedOnDate = DateTimeOffset.UtcNow;
            workout.LastModifiedDate = DateTimeOffset.UtcNow;
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
            var workoutToDelete = await workoutRepository.GetByIdAsync(id);

            if (workoutToDelete == null)
            {
                return NotFound();
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

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<WorkoutForReturnDetailedDto>> UpdateWorkoutAsync(int id, [FromBody] JsonPatchDocument<Workout> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var workout = await workoutRepository.GetByIdDetailedAsync(id);

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
            var workout = await workoutRepository.GetByIdAsync(id);
            mapper.Map(updateDto, workout);

            workout.LastModifiedDate = DateTimeOffset.UtcNow;

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