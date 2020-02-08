using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Data;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Helpers.QueryParams;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWorkoutRepository repo;

        public WorkoutsController(IMapper mapper, IWorkoutRepository repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetWorkouts([FromQuery] WorkoutParams woParams)
        {
            PagedList<Workout> workouts = await repo.GetWorkoutsAsync(woParams);

            IEnumerable<WorkoutForReturnDto> workoutsToReturn = mapper.Map<IEnumerable<WorkoutForReturnDto>>(workouts);
            Response.AddPagination(workouts.CurrentPage, workouts.PageSize, workouts.TotalCount, workouts.TotalPages);

            return Ok(workoutsToReturn);
        }

        [HttpGet("{id}", Name="GetWorkout")]
        public async Task<IActionResult> GetWorkout(int id)
        {
            Workout workout = await repo.GetWorkoutAsync(id);

            if (workout == null)
            {
                return NoContent();
            }

            WorkoutForReturnDto workoutToReturn = mapper.Map<WorkoutForReturnDto>(workout);

            return Ok(workoutToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] WorkoutForCreationDto newWorkout)
        {
            Workout workout = mapper.Map<Workout>(newWorkout);

            workout.CreatedOnDate = DateTime.Now;
            workout.LastModifiedDate = DateTime.Now;

            repo.Add<Workout>(workout);

            if (await repo.SaveAllAsync())
            {
                WorkoutForReturnDto woReturn = mapper.Map<WorkoutForReturnDto>(workout);

                return CreatedAtRoute("GetWorkout", new { id = workout.Id }, woReturn); 
            }
            
            return BadRequest("Could not create workout!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            Workout workoutToDelete = await repo.GetWorkoutAsync(id);

            if (workoutToDelete == null)
            {
                return NoContent();
            }

            if (workoutToDelete.CreatedByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            repo.Delete<Workout>(workoutToDelete);

            if (await repo.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest("Failed to delete workout!");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] WorkoutForUpdateDto workoutUpdate)
        {
            Workout workoutToUpdate = await repo.GetWorkoutAsync(id);
            bool changed = false;

            if (workoutToUpdate == null)
            {
                return NoContent();
            }

            if (workoutToUpdate.CreatedByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (workoutUpdate.Label != null && workoutUpdate.Label != workoutToUpdate.Label)
            {
                changed = true;
                workoutToUpdate.Label = workoutUpdate.Label;
            }

            if (workoutUpdate.Color != null && workoutUpdate.Color != workoutToUpdate.Color)
            {
                changed = true;
                workoutToUpdate.Color = workoutUpdate.Color;
            }

            if (workoutUpdate.Shareable != null && workoutUpdate.Shareable.Value != workoutToUpdate.Shareable)
            {
                changed = true;
                workoutToUpdate.Shareable = workoutUpdate.Shareable.Value;
            }

            if (workoutUpdate.ExerciseGroupIdsToRemove != null)
            {
                changed = true;

                HashSet<int> idsToRemove = new HashSet<int>(workoutUpdate.ExerciseGroupIdsToRemove);
                workoutToUpdate.ExerciseGroups.RemoveAll(eg => idsToRemove.Contains(eg.Id));
            }

            if (workoutUpdate.ExerciseGroupsToAdd != null)
            {
                changed = true;

                foreach (ExerciseGroupForCreationDto exGroup in workoutUpdate.ExerciseGroupsToAdd)
                {
                    ExerciseGroup newGroup = mapper.Map<ExerciseGroup>(exGroup);
                    newGroup.Workout = workoutToUpdate;
                    repo.Add<ExerciseGroup>(newGroup);
                }
            }

            if (changed)
            {
                workoutToUpdate.LastModifiedDate = DateTime.Now;
            }
            else
            {
                return BadRequest("The update has no changes!");
            }

            if (await repo.SaveAllAsync())
            {
                WorkoutForReturnDto updatedWorkout = mapper.Map<WorkoutForReturnDto>(workoutToUpdate);

                return Ok(updatedWorkout);
            }

            return BadRequest("Could not update workout!");
        }
    }
}