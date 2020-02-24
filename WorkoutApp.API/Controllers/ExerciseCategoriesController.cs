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
        private readonly IMapper mapper;


        public ExerciseCategoriesController(ExerciseCategoryRepository exerciseCategoryRepository, IMapper mapper)
        {
            this.exerciseCategoryRepository = exerciseCategoryRepository; 
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseCategoryForReturnDto>>> GetExerciseCategoriesAsync([FromQuery] PaginationParams searchParams)
        {
            var categories = await exerciseCategoryRepository.GetExerciseCategoriesAsync(searchParams);
            Response.AddPagination(categories);
            var categoriesForReturn = mapper.Map<IEnumerable<ExerciseCategoryForReturnDto>>(categories);

            return Ok(categoriesForReturn);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<ExerciseCategoryForReturnDetailedDto>>> GetExerciseCategoriesDetailedAsync([FromQuery] PaginationParams searchParams)
        {
            var categories = await exerciseCategoryRepository.GetExerciseCategoriesDetailedAsync(searchParams);
            Response.AddPagination(categories);
            var categoriesForReturn = mapper.Map<IEnumerable<ExerciseCategoryForReturnDetailedDto>>(categories);

            return Ok(categoriesForReturn);
        }

        [HttpGet("{id}", Name = "GetExerciseCategory")]
        public async Task<ActionResult<ExerciseCategoryForReturnDto>> GetExerciseCategoryAsync(int id)
        {
            var category = await exerciseCategoryRepository.GetExerciseCategoryAsync(id);

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
            var category = await exerciseCategoryRepository.GetExerciseCategoryDetailedAsync(id);

            if (category == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"Exercise category with id {id} does not exist.", 404, Request));
            }

            var categoryForReturnDto = mapper.Map<ExerciseCategoryForReturnDetailedDto>(category);
            
            return Ok(categoryForReturnDto);
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
            var category = await exerciseCategoryRepository.GetExerciseCategoryAsync(id);

            if (category == null)
            {
                return NoContent();
            }

            exerciseCategoryRepository.Delete(category);
            var saveResult = await exerciseCategoryRepository.SaveAllAsync();

            if (!saveResult)
            {
               return BadRequest(new ProblemDetailsWithErrors("Failed to delete the category.", 400, Request)); 
            }
            
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ExerciseCategoryForReturnDto>> UpdateExerciseCategoryAsync(int id, [FromBody] JsonPatchDocument<ExerciseCategory> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var category = await exerciseCategoryRepository.GetExerciseCategoryAsync(id);

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