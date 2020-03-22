using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkoutApp.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Controllers
{
    public partial class UsersController : ControllerBase
    {

        [HttpGet("{userId}/favorites/exercises")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDto>>> GetFavoriteExercisesForUserAsync(int userId, [FromQuery] PaginationParams searchParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var exercises = await userRepository.GetFavoriteExercisesForUserAsync(userId, searchParams);
            Response.AddPagination(exercises);
            var exercisesForReturn = mapper.Map<IEnumerable<ExerciseForReturnDto>>(exercises);

            return Ok(exercisesForReturn);
        }

        [HttpGet("{userId}/favorites/exercises/detailed")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> GetFavoriteExercisesForUserDetailedAsync(int userId, [FromQuery] PaginationParams searchParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var exercises = await userRepository.GetFavoriteExercisesForUserDetailedAsync(userId, searchParams);
            Response.AddPagination(exercises);
            var exercisesForReturn = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(exercises);

            return Ok(exercisesForReturn);
        }
    }
}