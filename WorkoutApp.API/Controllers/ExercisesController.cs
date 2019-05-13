using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
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
        public async Task<IActionResult> GetExercises([FromQuery] ExerciseQueryParams exParams)
        {
            List<ExerciseForReturnDto> exercises = await repo.GetExercises(exParams);

            return Ok(exercises);
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetExercise(int exerciseId)
        {
            ExerciseForReturnDto exercise = await repo.GetExercise(exerciseId);

            return Ok(exercise);
        }

        [HttpGet("{exerciseId}/detailed")]
        public async Task<IActionResult> GetExerciseDetailed(int exerciseId)
        {
            ExerciseForReturnDetailedDto exercise = await repo.GetExerciseDetailed(exerciseId);

            return Ok(exercise);
        }

        [HttpGet("detailed")]
        public async Task<IActionResult> GetExercisesDetailed([FromQuery] ExerciseQueryParams exParams)
        {
            List<ExerciseForReturnDetailedDto> exercises = await repo.GetExercisesDetailed(exParams);

            return Ok(exercises);
        }

        [HttpGet("{exerciseId}/equipment")]
        public async Task<IActionResult> GetEquipmentForExercise(int exerciseId)
        {
            List<EquipmentForReturnDto> equipment = await repo.GetEquipmentForExercise(exerciseId);

            return Ok(equipment);
        }
    }
}