using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkoutApp.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;
using WorkoutApp.API.Data.Repositories;
using System.Linq;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Controllers
{
    public partial class UsersController : ControllerBase
    {
        [HttpGet("{userId}/favorites/exercises")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDto>>> GetFavoriteExercisesForUserAsync(int userId, [FromQuery] ExerciseSearchParams searchParams)
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
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDetailedDto>>> GetFavoriteExercisesForUserDetailedAsync(int userId, [FromQuery] ExerciseSearchParams searchParams)
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