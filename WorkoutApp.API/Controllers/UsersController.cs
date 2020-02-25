using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WorkoutApp.API.Data.Repositories;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly IMapper mapper;


        public UsersController(UserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserForReturnDto>>> GetUsersAsync([FromQuery] PaginationParams searchParams)
        {
            var users = await userRepository.SearchAsync(searchParams);
            var usersToReturn = mapper.Map<IEnumerable<UserForReturnDto>>(users);
            Response.AddPagination(users);

            return Ok(usersToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<UserForReturnDetailedDto>>> GetUsersDetailedAsync([FromQuery] PaginationParams searchParams)
        {
            var users = await userRepository.SearchDetailedAsync(searchParams);
            var usersToReturn = mapper.Map<IEnumerable<UserForReturnDetailedDto>>(users);
            Response.AddPagination(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUserAsync")]
        public async Task<ActionResult<UserForReturnDto>> GetUserAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            var userToReturn = mapper.Map<UserForReturnDto>(user);

            return Ok(userToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<UserForReturnDetailedDto>> GetUserDetailedAsync(int id)
        {
            var users = await userRepository.GetByIdDetailedAsync(id);
            var usersToReturn = mapper.Map<UserForReturnDetailedDto>(users);

            return Ok(usersToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("roles")]
        public async Task<ActionResult<RoleForReturnDto>> GetRolesAsync([FromQuery] PaginationParams searchParams)
        {
            var roles = await userRepository.GetRolesAsync(searchParams);
            var rolesForReturn = mapper.Map<IEnumerable<RoleForReturnDto>>(roles);
            Response.AddPagination(roles);

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

            var user = await userRepository.GetByIdDetailedAsync(id);
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

            var user = await userRepository.GetByIdDetailedAsync(id);
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

        [HttpGet("{userId}/scheduledWorkouts")]
        public async Task<ActionResult<IEnumerable<ScheduledWorkoutForReturnDto>>> GetScheduledWorkoutsForUserAsync(int userId, [FromQuery] ScheduledWorkoutSearchParams searchParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var workouts = await userRepository.GetScheduledWorkoutsForUserAsync(userId, searchParams);
            Response.AddPagination(workouts);
            var workoutsForReturn = mapper.Map<IEnumerable<ScheduledWorkoutForReturnDto>>(workouts);

            return Ok(workoutsForReturn);
        }

        [HttpGet("{userId}/workoutCompletionRecords")]
        public async Task<ActionResult<IEnumerable<object>>> GetWorkoutCompletionRecordsForUserAsync(int userId, [FromQuery] CompletionRecordSearchParams searchParams)
        {
            var records = await userRepository.GetWorkoutCompletionRecordsForUserAsync(userId, searchParams);
            Response.AddPagination(records);

            return Ok(records);
        }

        // [HttpGet("{userId}/favorites/exercises")]
        // public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> GetUserFavoriteExercisesAsync(int userId, [FromQuery] string exerciseCategory)
        // {
        //     if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //     {
        //         return Unauthorized();
        //     }

        //     var exercises = await exerciseProvider.GetFavoriteExercisesForUserAsync(userId);
        //     var dtos = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(exercises);

        //     return Ok(dtos);
        // }

        // [HttpPost("{userId}/favorites/exercises/{exerciseId}")]
        // public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> FavoriteAnExerciseAsync(int userId, int exerciseId)
        // {
        //     if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //     {
        //         return Unauthorized();
        //     }

        //     var user = await repo.GetUserAsync(userId);
        //     var exercise = await repo.GetExerciseAsync(exerciseId);

        //     if (user.FavoriteExercises.Any(e => e.ExerciseId == exerciseId))
        //     {
        //         return BadRequest(new ProblemDetailsWithErrors("You cannot favorite the same exercise more than once.", 400, Request));
        //     }

        //     user.FavoriteExercises.Add(new UserFavoriteExercise
        //     {
        //         Exercise = exercise
        //     });

        //     if (!await repo.SaveAllAsync())
        //     {
        //         return BadRequest(new ProblemDetailsWithErrors("Unable to favorite the exercise.", 400, Request));
        //     }

        //     return NoContent();
        // }

        // [HttpDelete("{userId}/favorites/exercises/{exerciseId}")]
        // public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> UnfavoriteAnExerciseAsync(int userId, int exerciseId)
        // {
        //     if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //     {
        //         return Unauthorized();
        //     }

        //     var user = await repo.GetUserAsync(userId);
        //     var exercise = await repo.GetExerciseAsync(exerciseId);
        //     var exerciseToRemove = user.FavoriteExercises.FirstOrDefault(fe => fe.ExerciseId == exerciseId);

        //     if (exerciseToRemove == null)
        //     {
        //         return BadRequest(new ProblemDetailsWithErrors("You cannot unfavorite an exercise you have not favorited.", 400, Request));
        //     }

        //     user.FavoriteExercises.Remove(exerciseToRemove);

        //     if (!await repo.SaveAllAsync())
        //     {
        //         return BadRequest(new ProblemDetailsWithErrors("Unable to favorite the exercise.", 400, Request));
        //     }

        //     return NoContent();
        // }
    }
}