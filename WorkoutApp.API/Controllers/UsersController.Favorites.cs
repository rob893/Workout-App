using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkoutApp.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;
using System.Linq;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Controllers
{
    public partial class UsersController : ControllerBase
    {
        [HttpGet("{userId}/favorites/exercises")]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseForReturnDto>>> GetFavoriteExercisesForUserAsync(int userId, [FromQuery] ExerciseSearchParams searchParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var exercises = await userRepository.GetFavoriteExercisesForUserAsync(userId, searchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseForReturnDto>.CreateFrom(exercises, mapper.Map<IEnumerable<ExerciseForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{userId}/favorites/exercises/detailed")]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseForReturnDetailedDto>>> GetFavoriteExercisesForUserDetailedAsync(int userId, [FromQuery] ExerciseSearchParams searchParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var exercises = await userRepository.GetFavoriteExercisesForUserDetailedAsync(userId, searchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseForReturnDetailedDto>.CreateFrom(exercises, mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>);

            return Ok(paginatedResponse);
        }

        [HttpPost("{userId}/favorites/exercises/{exerciseId}")]
        public async Task<ActionResult> FavoriteAnExerciseAsync(int userId, int exerciseId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await userRepository.GetByIdAsync(userId);
            var exercise = await exerciseRepository.GetByIdAsync(exerciseId);

            if (user.FavoriteExercises.Any(e => e.ExerciseId == exerciseId))
            {
                return BadRequest(new ProblemDetailsWithErrors("You cannot favorite the same exercise more than once.", 400, Request));
            }

            user.FavoriteExercises.Add(new UserFavoriteExercise
            {
                Exercise = exercise
            });

            if (!await userRepository.SaveAllAsync())
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to favorite the exercise.", 400, Request));
            }

            return NoContent();
        }

        [HttpDelete("{userId}/favorites/exercises/{exerciseId}")]
        public async Task<ActionResult> UnfavoriteAnExerciseAsync(int userId, int exerciseId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await userRepository.GetByIdAsync(userId);
            var exercise = await exerciseRepository.GetByIdAsync(exerciseId);
            var exerciseToRemove = user.FavoriteExercises.FirstOrDefault(fe => fe.ExerciseId == exerciseId);

            if (exerciseToRemove == null)
            {
                return BadRequest(new ProblemDetailsWithErrors("You cannot unfavorite an exercise you have not favorited.", 400, Request));
            }

            user.FavoriteExercises.Remove(exerciseToRemove);

            if (!await userRepository.SaveAllAsync())
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to favorite the exercise.", 400, Request));
            }

            return NoContent();
        }
    }
}