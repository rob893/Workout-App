using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Data;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Helpers.QueryParams;
using WorkoutApp.API.Helpers.Specifications;

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
        public async Task<IActionResult> GetScheduledWorkout(int id)
        {
            ScheduledUserWorkout workout = await repo.GetScheduledUserWorkout(id);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        [HttpPost]
        public async Task<IActionResult> CreateScheduledWorkout([FromBody] ScheduledWoForCreationDto newWorkoutDto)
        {
            if (await repo.GetWorkout(newWorkoutDto.WorkoutId) == null)
            {
                return BadRequest("Invalid workout id!");
            }

            ScheduledUserWorkout newWorkout = mapper.Map<ScheduledUserWorkout>(newWorkoutDto);

            newWorkout.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            repo.Add<ScheduledUserWorkout>(newWorkout);

            if (await repo.SaveAll())
            {
                return CreatedAtRoute("GetScheduledWorkout", new { id = newWorkout.Id }, newWorkout);
            }

            return BadRequest("Could not save the new workout.");
        }

        [HttpPatch("{id}/addAdHocExercise")]
        public async Task<IActionResult> AddAdHocExercise(int id, [FromBody] ExerciseGroupForCreationDto adHocExercise)
        {
            ScheduledUserWorkout workout = await repo.GetScheduledUserWorkout(id);

            if (workout == null)
            {
                return NotFound();
            }

            if (workout.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            ExerciseGroup newAdHocExercise = mapper.Map<ExerciseGroup>(adHocExercise);
            workout.AdHocExercises.Add(newAdHocExercise);

            if (await repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Could not add exercise.");
        }

        [HttpPatch("{id}/startWorkout")]
        public async Task<IActionResult> StartWorkout(int id)
        {
            ScheduledUserWorkout workout = await repo.GetScheduledUserWorkout(id);

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
                return BadRequest("This workout has already been started!");
            }

            workout.StartedDateTime = DateTime.Now;

            if (await repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Could not start workout.");
        }

        [HttpPatch("{id}/completeWorkout")]
        public async Task<IActionResult> CompleteWorkout(int id)
        {
            ScheduledUserWorkout workout = await repo.GetScheduledUserWorkout(id);

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
                return BadRequest("This workout has already been completed!");
            }

            workout.CompletedDateTime = DateTime.Now;

            if (workout.StartedDateTime == null)
            {
                workout.StartedDateTime = DateTime.Now;
            }

            if (await repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Could not complete workout.");
        }
    }
}