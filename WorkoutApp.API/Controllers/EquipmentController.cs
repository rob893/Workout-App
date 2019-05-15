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
        public async Task<IActionResult> GetEquipment([FromQuery] EquipmentQueryParams eqParams)
        {
            PagedList<Equipment> equipment = await repo.GetExerciseEquipment(eqParams);
            
            Response.AddPagination(equipment.CurrentPage, equipment.PageSize, equipment.TotalCount, equipment.TotalPages);

            IEnumerable<EquipmentForReturnDto> equipmentToReturn = mapper.Map<IEnumerable<EquipmentForReturnDto>>(equipment);

            return Ok(equipmentToReturn);
        }

        [HttpGet("{id}", Name = "GetSingleEquipment")]
        public async Task<IActionResult> GetSingleEquipment(int id)
        {
            Equipment equipment = await repo.GetSingleExerciseEquipment(id);

            EquipmentForReturnDto equipmentToReturn = mapper.Map<EquipmentForReturnDto>(equipment);
            
            return Ok(equipmentToReturn);
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
            Equipment equipment = await repo.GetSingleExerciseEquipment(id);

            if (equipment == null)
            {
                return NoContent();
            }

            repo.Delete<Equipment>(equipment);

            if (await repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to delete the equipment!");
        }
    }
}