using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly IMapper mapper;


        public WorkoutPlanController(IWorkoutRepository repo, IMapper mapper)
        {
            this.repo = repo; 
            this.mapper = mapper;   
        }

        [HttpGet("{id}", Name = "GetWorkoutPlan")]
        public async Task<IActionResult> GetWorkoutPlan(int id)
        {
            WorkoutPlan workoutPlan = await repo.GetWorkoutPlan(id);


            return Ok(workoutPlan);
        }

        [HttpPost("{planId}/workout")]
        public async Task<IActionResult> CreateWorkout(int planId, [FromBody] WorkoutForCreationDto newWorkout)
        {
            WorkoutPlan plan = await repo.GetWorkoutPlan(planId);

            if (plan == null)
            {
                return NotFound();
            }

            if (plan.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            newWorkout.WorkoutPlanId = planId;

            Workout workout = mapper.Map<Workout>(newWorkout);

            workout.WorkoutPlan = plan;

            repo.Add<Workout>(workout);

            if (await repo.SaveAll())
            {
                return Ok(workout);
            }

            return BadRequest("Unable to create workout!");
        }

        [HttpPatch("{planId}/workout/{woId}")]
        public async Task<IActionResult> UpdateWorkout(int planId, int woId, WorkoutForUpdateDto workoutUpdate)
        {
            Workout workout = await repo.GetWorkout(woId);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.WorkoutPlanId != planId)
            {
                return Unauthorized();
            }

            WorkoutPlan plan = await repo.GetWorkoutPlan(planId);

            if (plan == null)
            {
                return NotFound();
            }

            if (plan.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (workoutUpdate.Date != null)
            {
                workout.Date = (DateTime)workoutUpdate.Date;
            }

            if (workoutUpdate.ExerciseGroupsToAdd != null)
            {
                foreach (ExerciseGroupForCreationDto exGroup in workoutUpdate.ExerciseGroupsToAdd)
                {
                    ExerciseGroup newGroup = mapper.Map<ExerciseGroup>(exGroup);
                    newGroup.WorkoutId = woId;
                    repo.Add<ExerciseGroup>(newGroup);
                }
            }

            if (workoutUpdate.ExerciseGroupIdsToRemove != null)
            {
                HashSet<int> idsToRemove = new HashSet<int>(workoutUpdate.ExerciseGroupIdsToRemove);
                workout.ExerciseGroups.RemoveAll(eg => idsToRemove.Contains(eg.Id));
            }

            if (await repo.SaveAll())
            {
                Workout newWorkout = await repo.GetWorkout(woId);
                WorkoutForReturnDto workoutToReturn = mapper.Map<WorkoutForReturnDto>(newWorkout);
                return Ok(workoutToReturn);
            }

            return BadRequest("Could not update workout!");
        }
    }
}