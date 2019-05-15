using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Helpers.QueryParams;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly IMapper mapper;


        public ExercisesController(IWorkoutRepository repo, IMapper mapper)
        {
            this.repo = repo; 
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetExercises([FromQuery] ExerciseParams exParams)
        {
            PagedList<Exercise> exercises = await repo.GetExercises(exParams);
            
            Response.AddPagination(exercises.CurrentPage, exercises.PageSize, exercises.TotalCount, exercises.TotalPages);

            IEnumerable<ExerciseForReturnDto> exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDto>>(exercises);

            return Ok(exercisesToReturn);
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetExercise(int exerciseId)
        {
            Exercise exercise = await repo.GetExercise(exerciseId);

            ExerciseForReturnDto exToReturn = mapper.Map<ExerciseForReturnDto>(exercise);

            return Ok(exToReturn);
        }

        [HttpGet("{exerciseId}/detailed")]
        public async Task<IActionResult> GetExerciseDetailed(int exerciseId)
        {
            Exercise exercise = await repo.GetExercise(exerciseId);

            ExerciseForReturnDetailedDto exToReturn = mapper.Map<ExerciseForReturnDetailedDto>(exercise);

            return Ok(exToReturn);
        }

        [HttpGet("detailed")]
        public async Task<IActionResult> GetExercisesDetailed([FromQuery] ExerciseParams exParams)
        {
            PagedList<Exercise> exercises = await repo.GetExercises(exParams);

            Response.AddPagination(exercises.CurrentPage, exercises.PageSize, exercises.TotalCount, exercises.TotalPages);

            IEnumerable<ExerciseForReturnDetailedDto> exercisesToReturn = mapper.Map<IEnumerable<ExerciseForReturnDetailedDto>>(exercises);

            return Ok(exercisesToReturn);
        }

        [HttpGet("{exerciseId}/equipment")]
        public async Task<IActionResult> GetEquipmentForExercise(int exerciseId, [FromQuery] EquipmentParams eqParams)
        {
            eqParams.ExerciseIds.Add(exerciseId);

            PagedList<Equipment> equipment = await repo.GetExerciseEquipment(eqParams);

            Response.AddPagination(equipment.CurrentPage, equipment.PageSize, equipment.TotalCount, equipment.TotalPages);

            IEnumerable<EquipmentForReturnDto> equipmentForReturn = mapper.Map<IEnumerable<EquipmentForReturnDto>>(equipment);

            return Ok(equipmentForReturn);
        }
    }
}