using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data.Repositories;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExerciseCategoriesController : ControllerBase
    {
        private readonly ExerciseCategoryRepository exerciseCategoryRepository;
        private readonly ExerciseRepository exerciseRepository;
        private readonly IMapper mapper;


        public ExerciseCategoriesController(ExerciseCategoryRepository exerciseCategoryRepository, ExerciseRepository exerciseRepository, IMapper mapper)
        {
            this.exerciseCategoryRepository = exerciseCategoryRepository;
            this.exerciseRepository = exerciseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseCategoryForReturnDto>>> GetExerciseCategoriesAsync([FromQuery] ExerciseCategorySearchParams searchParams)
        {
            var categories = await exerciseCategoryRepository.SearchAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseCategoryForReturnDto>.CreateFrom(categories, mapper.Map<IEnumerable<ExerciseCategoryForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseCategoryForReturnDetailedDto>>> GetExerciseCategoriesDetailedAsync([FromQuery] ExerciseCategorySearchParams searchParams)
        {
            var categories = await exerciseCategoryRepository.SearchDetailedAsync(searchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseCategoryForReturnDetailedDto>.CreateFrom(categories, mapper.Map<IEnumerable<ExerciseCategoryForReturnDetailedDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}", Name = "GetExerciseCategory")]
        public async Task<ActionResult<ExerciseCategoryForReturnDto>> GetExerciseCategoryAsync(int id)
        {
            var category = await exerciseCategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"Exercise category with id {id} does not exist.", 404, Request));
            }

            var categoryForReturnDto = mapper.Map<ExerciseCategoryForReturnDto>(category);

            return Ok(categoryForReturnDto);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<ExerciseCategoryForReturnDetailedDto>> GetExerciseCategoryDetailedAsync(int id)
        {
            var category = await exerciseCategoryRepository.GetByIdDetailedAsync(id);

            if (category == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"Exercise category with id {id} does not exist.", 404, Request));
            }

            var categoryForReturnDto = mapper.Map<ExerciseCategoryForReturnDetailedDto>(category);

            return Ok(categoryForReturnDto);
        }

        [HttpGet("{id}/exercises")]
        public async Task<ActionResult<CursorPaginatedResponse<ExerciseForReturnDto>>> GetExercisesForExerciseCategoryAsync(int id, [FromQuery] CursorPaginationParams searchParams)
        {
            var exerciseSearchParams = new ExerciseSearchParams
            {
                First = searchParams.First,
                After = searchParams.After,
                Last = searchParams.Last,
                Before = searchParams.Before,
                IncludeTotal = searchParams.IncludeTotal,
                ExerciseCategoryId = new List<int> { id }
            };

            var exercises = await exerciseRepository.SearchAsync(exerciseSearchParams);
            var paginatedResponse = CursorPaginatedResponse<ExerciseForReturnDto>.CreateFrom(exercises, mapper.Map<IEnumerable<ExerciseForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseCategoryForReturnDto>> CreateExerciseCategoryAsync([FromBody] ExerciseCategoryForCreationDto categoryForCreation)
        {
            var newCategory = mapper.Map<ExerciseCategory>(categoryForCreation);
            exerciseCategoryRepository.Add(newCategory);

            var saveResult = await exerciseCategoryRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Unable to create category.", 400, Request));
            }

            var categoryForReturn = mapper.Map<ExerciseCategoryForReturnDto>(newCategory);

            return CreatedAtRoute("GetExerciseCategory", new { id = newCategory.Id }, categoryForReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExerciseCategoryAsync(int id)
        {
            var category = await exerciseCategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            exerciseCategoryRepository.Delete(category);
            var saveResult = await exerciseCategoryRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Failed to delete the category.", 400, Request));
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ExerciseCategoryForReturnDto>> UpdateExerciseCategoryAsync(int id, [FromBody] JsonPatchDocument<ExerciseCategory> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var category = await exerciseCategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(category);

            var saveResult = await exerciseCategoryRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
            }

            var categoryToReturn = mapper.Map<ExerciseCategoryForReturnDto>(category);

            return Ok(categoryToReturn);
        }
    }
}