using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MusclesController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly IMapper mapper;


        public MusclesController(IWorkoutRepository repo, IMapper mapper)
        {
            this.repo = repo; 
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Muscle>>> GetMuscles()
        {
            var muscles = await repo.GetMusclesAsync();

            return Ok(muscles);
        }

        [HttpGet("{id}", Name = "GetMuscle")]
        public async Task<ActionResult<Muscle>> GetMuscle(int id)
        {
            var muscle = await repo.GetMuscleAsync(id);

            if (muscle == null)
            {
                return NotFound(new ProblemDetailsWithErrors($"Muscle with id {id} does not exist.", 404, Request));
            }
            
            return Ok(muscle);
        }
    }
}