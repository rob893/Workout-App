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

namespace WorkoutApp.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly IMapper mapper;


        public UsersController(IWorkoutRepository repo, IMapper mapper)
        {
            this.repo = repo; 
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await repo.GetUsers();

            List<UserForReturnDto> usersToReturn = mapper.Map<List<UserForReturnDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await repo.GetUser(id);

            UserForReturnDto userToReturn = mapper.Map<UserForReturnDto>(user);

            return Ok(userToReturn);
        }

        [HttpPost("{userId}/workoutPlan")]
        public async Task<IActionResult> CreateWorkoutPlanForUser(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            WorkoutPlan newWorkoutPlan = new WorkoutPlan();

            newWorkoutPlan.UserId = userId;

            repo.Add<WorkoutPlan>(newWorkoutPlan);

            if (await repo.SaveAll())
            {
                return CreatedAtRoute("GetWorkoutPlan", new { id = newWorkoutPlan.Id }, newWorkoutPlan);
            }

            return BadRequest("Could not create workout plan.");
        }

        [HttpGet("{userId}/workoutPlan")]
        public async Task<IActionResult> GetWorkoutPlansForUser(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            List<WorkoutPlan> workoutPlans = await repo.GetWorkoutPlansForUser(userId);


            return Ok(workoutPlans);
        }

        [HttpGet("{userId}/workouts")]
        public async Task<IActionResult> GetWorkoutsForUser(int userId, [FromQuery] WorkoutParams woParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            woParams.UserId = userId;

            PagedList<Workout> workouts = await repo.GetWorkouts(woParams);
            Response.AddPagination(workouts.CurrentPage, workouts.PageSize, workouts.TotalCount, workouts.TotalPages);

            IEnumerable<WorkoutForReturnDto> workoutsForReturn = mapper.Map<IEnumerable<WorkoutForReturnDto>>(workouts);

            return Ok(workoutsForReturn);
        }
    }
}