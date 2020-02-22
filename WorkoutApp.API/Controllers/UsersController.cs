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
using WorkoutApp.API.Data.Providers;
using WorkoutApp.API.Data.Repositories;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly ExerciseProvider exerciseProvider;
        private readonly IMapper mapper;
        private readonly UserRepository userRepository;


        public UsersController(IWorkoutRepository repo, IMapper mapper, ExerciseProvider exerciseProvider, UserRepository userRepository)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.exerciseProvider = exerciseProvider;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserForReturnDto>>> GetUsersAsync()
        {
            var users = await userRepository.GetUsersAsync();

            var usersToReturn = mapper.Map<IEnumerable<UserForReturnDto>>(users);

            return Ok(usersToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<UserForReturnDetailedDto>>> GetUsersDetailedAsync()
        {
            var users = await userRepository.GetUsersDetailedAsync();
            var usersToReturn = mapper.Map<IEnumerable<UserForReturnDetailedDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUserAsync")]
        public async Task<ActionResult<UserForReturnDto>> GetUserAsync(int id)
        {
            var user = await userRepository.GetUserAsync(id);

            var userToReturn = mapper.Map<UserForReturnDto>(user);

            return Ok(userToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<UserForReturnDetailedDto>> GetUserDetailedAsync(int id)
        {
            var users = await userRepository.GetUserDetailedAsync(id);
            var usersToReturn = mapper.Map<UserForReturnDetailedDto>(users);

            return Ok(usersToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("roles")]
        public async Task<ActionResult<RoleForReturnDto>> GetRolesAsync()
        {
            var roles = await userRepository.GetRolesAsync();

            var rolesForReturn = mapper.Map<IEnumerable<RoleForReturnDto>>(roles);

            return Ok(rolesForReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("{id}/roles")]
        public async Task<ActionResult<UserForReturnDetailedDto>> AddRolesAsync(int id, [FromBody] RoleEditDto roleEditDto)
        {
            if (roleEditDto.RoleNames == null || roleEditDto.RoleNames.Length == 0)
            {
                return BadRequest(new ProblemDetailsWithErrors("At least one role must be specified.", 400, Request));
            }

            var user = await userRepository.GetUserDetailedAsync(id);
            var roles = await userRepository.GetRolesAsync();

            var userRoles = user.UserRoles.Select(ur => ur.Role.Name.ToUpper()).ToHashSet();
            var selectedRoles = roleEditDto.RoleNames.Select(role => role.ToUpper()).ToHashSet();

            var rolesToAdd = roles.Where(role =>
            {
                var upperName = role.Name.ToUpper();
                return selectedRoles.Contains(upperName) && !userRoles.Contains(upperName);
            });

            if (rolesToAdd.Count() == 0)
            {
                return Ok(mapper.Map<UserForReturnDetailedDto>(user));
            }

            user.UserRoles.AddRange(rolesToAdd.Select(role => new UserRole
            {
                Role = role
            }));

            var success = await userRepository.SaveAllAsync();

            if (!success)
            {
                return BadRequest(new ProblemDetailsWithErrors("Failed to add roles.", 400, Request));
            }

            var userToReturn = mapper.Map<UserForReturnDetailedDto>(user);

            return Ok(userToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}/roles")]
        public async Task<ActionResult<UserForReturnDetailedDto>> RemoveRolesAsync(int id, [FromBody] RoleEditDto roleEditDto)
        {
            if (roleEditDto.RoleNames == null || roleEditDto.RoleNames.Length == 0)
            {
                return BadRequest(new ProblemDetailsWithErrors("At least one role must be specified.", 400, Request));
            }

            var user = await userRepository.GetUserDetailedAsync(id);
            var roles = await userRepository.GetRolesAsync();

            var userRoles = user.UserRoles.Select(ur => ur.Role.Name.ToUpper()).ToHashSet();
            var selectedRoles = roleEditDto.RoleNames.Select(role => role.ToUpper()).ToHashSet();

            var roleIdsToRemove = roles.Where(role =>
            {
                var upperName = role.Name.ToUpper();
                return selectedRoles.Contains(upperName) && userRoles.Contains(upperName);
            }).Select(role => role.Id).ToHashSet();

            if (roleIdsToRemove.Count() == 0)
            {
                return Ok(mapper.Map<UserForReturnDetailedDto>(user));
            }

            user.UserRoles.RemoveAll(ur => roleIdsToRemove.Contains(ur.RoleId));

            var success = await userRepository.SaveAllAsync();

            if (!success)
            {
                return BadRequest(new ProblemDetailsWithErrors("Failed to remove roles.", 400, Request));
            }

            var userToReturn = mapper.Map<UserForReturnDetailedDto>(user);

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