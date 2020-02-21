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
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WorkoutApp.API.Data.Providers;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly ExerciseProvider exerciseProvider;
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly UserManager<User> userManager;


        public UsersController(IWorkoutRepository repo, IMapper mapper, DataContext context, UserManager<User> userManager, ExerciseProvider exerciseProvider)
        {
            this.repo = repo; 
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
            this.exerciseProvider = exerciseProvider;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserForReturnDto>>> GetUsers()
        {
            List<User> users = await repo.GetUsersAsync();

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
            User user = await repo.GetUserAsync(id);

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

            PagedList<Workout> workouts = await repo.GetWorkoutsAsync(woParams);
            Response.AddPagination(workouts.CurrentPage, workouts.PageSize, workouts.TotalCount, workouts.TotalPages);

            IEnumerable<WorkoutForReturnDto> workoutsForReturn = mapper.Map<IEnumerable<WorkoutForReturnDto>>(workouts);

            return Ok(workoutsForReturn);
        }

        [HttpGet("{userId}/favorites/exercises")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> GetUserFavoriteExercises(int userId, [FromQuery] string exerciseCategory)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var exercises = await exerciseProvider.GetFavoriteExercisesForUserAsync(userId);

            var dtos = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(exercises);

            return Ok(dtos);
        }

        [HttpPost("{userId}/favorites/exercises/{exerciseId}")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> FavoriteAnExercise(int userId, int exerciseId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await repo.GetUserAsync(userId);
            var exercise = await repo.GetExerciseAsync(exerciseId);

            if (user.FavoriteExercises.Any(e => e.ExerciseId == exerciseId))
            {
                return BadRequest(new ProblemDetailsWithErrors("You cannot favorite the same exercise more than once.", 400, Request));
            }

            user.FavoriteExercises.Add(new UserFavoriteExercise
            {
                Exercise = exercise
            });

            if (!await repo.SaveAllAsync())
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to favorite the exercise.", 400, Request));
            }

            return NoContent();
        }

        [HttpDelete("{userId}/favorites/exercises/{exerciseId}")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> UnfavoriteAnExercise(int userId, int exerciseId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await repo.GetUserAsync(userId);
            var exercise = await repo.GetExerciseAsync(exerciseId);

            var exerciseToRemove = user.FavoriteExercises.FirstOrDefault(fe => fe.ExerciseId == exerciseId);

            if (exerciseToRemove == null)
            {
                return BadRequest(new ProblemDetailsWithErrors("You cannot unfavorite an exercise you have not favorited.", 400, Request));
            }

            user.FavoriteExercises.Remove(exerciseToRemove);

            if (!await repo.SaveAllAsync())
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to favorite the exercise.", 400, Request));
            }

            return NoContent();
        }

        [HttpGet("{userId}/scheduledWorkouts")]
        public async Task<ActionResult<IEnumerable<ScheduledWoForReturnDto>>> GetScheduledWorkoutsForUser(int userId, [FromQuery] SchUsrWoParams woParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            woParams.UserId = userId;

            PagedList<ScheduledWorkout> workouts = await repo.GetScheduledUserWorkoutsAsync(woParams);
            Response.AddPagination(workouts.CurrentPage, workouts.PageSize, workouts.TotalCount, workouts.TotalPages);

            var workoutsForReturn = mapper.Map<IEnumerable<ScheduledWoForReturnDto>>(workouts);

            return Ok(workoutsForReturn);
        }
    }
}