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
    public partial class UsersController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly ExerciseRepository exerciseRepository;
        private readonly WorkoutInvitationRepository workoutInvitationRepository;
        private readonly IMapper mapper;


        public UsersController(UserRepository userRepository, ExerciseRepository exerciseRepository, WorkoutInvitationRepository workoutInvitationRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.exerciseRepository = exerciseRepository;
            this.workoutInvitationRepository = workoutInvitationRepository;
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

            if (user == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"User with id {id} does not exist."));
            }

            var userToReturn = mapper.Map<UserForReturnDto>(user);

            return Ok(userToReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<UserForReturnDetailedDto>> GetUserDetailedAsync(int id)
        {
            var user = await userRepository.GetByIdDetailedAsync(id);

            if (user == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"User with id {id} does not exist."));
            }

            var userToReturn = mapper.Map<UserForReturnDetailedDto>(user);

            return Ok(userToReturn);
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

        [HttpGet("{userId}/scheduledWorkouts/detailed")]
        public async Task<ActionResult<IEnumerable<ScheduledWorkoutForReturnDetailedDto>>> GetScheduledWorkoutsForUserDetailedAsync(int userId, [FromQuery] ScheduledWorkoutSearchParams searchParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var workouts = await userRepository.GetScheduledWorkoutsForUserDetailedAsync(userId, searchParams);
            Response.AddPagination(workouts);
            var workoutsForReturn = mapper.Map<IEnumerable<ScheduledWorkoutForReturnDetailedDto>>(workouts);

            return Ok(workoutsForReturn);
        }

        [HttpGet("{userId}/workoutCompletionRecords")]
        public async Task<ActionResult<IEnumerable<object>>> GetWorkoutCompletionRecordsForUserAsync(int userId, [FromQuery] CompletionRecordSearchParams searchParams)
        {
            var records = await userRepository.GetWorkoutCompletionRecordsForUserAsync(userId, searchParams);
            Response.AddPagination(records);

            return Ok(records);
        }
    }
}