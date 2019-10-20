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
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly UserManager<User> userManager;


        public UsersController(IWorkoutRepository repo, IMapper mapper, DataContext context, UserManager<User> userManager)
        {
            this.repo = repo; 
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserForReturnDto>>> GetUsers()
        {
            List<User> users = await repo.GetUsers();

            List<UserForReturnDto> usersToReturn = mapper.Map<List<UserForReturnDto>>(users);

            return Ok(usersToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles() //Change this. Dont want context in controller
        {   
            var userList = await (from user in context.Users orderby user.UserName
                                    select new 
                                    {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        Roles = (from userRole in user.UserRoles
                                            join role in context.Roles
                                            on userRole.RoleId
                                            equals role.Id
                                            select role.Name).ToList()
                                    }).ToListAsync();
            return Ok(userList);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("{id}/editRoles")]
        public async Task<IActionResult> EditRoles(int id, [FromBody] RoleEditDto roleEditDto)
        {
            User user = await userManager.FindByIdAsync(id.ToString());

            IList<string> userRoles = await userManager.GetRolesAsync(user);

            string[] selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] {};
            var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed to add to roles.");
            }

            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove roles.");
            }

            return Ok(await userManager.GetRolesAsync(user));
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await repo.GetUser(id);

            UserForReturnDto userToReturn = mapper.Map<UserForReturnDto>(user);

            return Ok(userToReturn);
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

        [HttpGet("{userId}/scheduledWorkouts")]
        public async Task<IActionResult> GetScheduledWorkoutsForUser(int userId, [FromQuery] SchUsrWoParams woParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            woParams.UserId = userId;

            PagedList<ScheduledUserWorkout> workouts = await repo.GetScheduledUserWorkouts(woParams);
            Response.AddPagination(workouts.CurrentPage, workouts.PageSize, workouts.TotalCount, workouts.TotalPages);

            //IEnumerable<WorkoutForReturnDto> workoutsForReturn = mapper.Map<IEnumerable<WorkoutForReturnDto>>(workouts);

            return Ok(workouts);//workoutsForReturn);
        }

        [HttpGet("test")]
        public async Task<IActionResult> GetTest()
        {
            var result = await repo.Find<Exercise>(new TestSpec());

            return Ok(result);
        }
    }
}