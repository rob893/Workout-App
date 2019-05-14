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
    public class EquipmentController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly IMapper mapper;


        public EquipmentController(IWorkoutRepository repo, IMapper mapper)
        {
            this.repo = repo; 
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipment([FromQuery] PaginationParams pgParams)
        {
            PagedList<EquipmentForReturnDto> equipment = await repo.GetExerciseEquipment(pgParams);
            
            Response.AddPagination(equipment.CurrentPage, equipment.PageSize, equipment.TotalCount, equipment.TotalPages);

            return Ok(equipment);
        }

        [HttpGet("{id}", Name = "GetSingleEquipment")]
        public async Task<IActionResult> GetSingleEquipment(int id)
        {
            EquipmentForReturnDto equipment = await repo.GetSingleExerciseEquipment(id);
            
            return Ok(equipment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEquipment([FromBody] EquipmentForCreationDto equipmentCreationDto)
        {
            Equipment equipment = mapper.Map<Equipment>(equipmentCreationDto);
            
            repo.Add<Equipment>(equipment);

            if (await repo.SaveAll())
            {
                EquipmentForReturnDto eqReturn = mapper.Map<EquipmentForReturnDto>(equipment);

                return CreatedAtRoute("GetSingleEquipment", new { id = equipment.Id }, eqReturn); 
            }

            return BadRequest("Could not create new equipment!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSingleEquipment(int id)
        {
            EquipmentForReturnDto equipmentDto = await repo.GetSingleExerciseEquipment(id);

            if (equipmentDto == null)
            {
                return NoContent();
            }

            Equipment equipment = mapper.Map<Equipment>(equipmentDto);

            repo.Delete<Equipment>(equipment);

            if (await repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to delete the equipment!");
        }
    }
}