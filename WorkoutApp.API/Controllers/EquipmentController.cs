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
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentRepository equipmentRepository;
        private readonly ExerciseRepository exerciseRepository;
        private readonly IMapper mapper;


        public EquipmentController(EquipmentRepository equipmentRepository, ExerciseRepository exerciseRepository, IMapper mapper)
        {
            this.equipmentRepository = equipmentRepository;
            this.exerciseRepository = exerciseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CursorPaginatedResponse<EquipmentForReturnDto>>> GetEquipmentAsync([FromQuery] EquipmentCursorSearchParams searchParams)
        {
            var equipment = await equipmentRepository.CursorSearchAsync(searchParams);
            //Response.AddPagination(equipment.PageNumber, equipment.PageSize, equipment.TotalItems, equipment.TotalPages);
            var equipmentToReturn = mapper.Map<IEnumerable<EquipmentForReturnDto>>(equipment);
            var mappedPaginatedList = new CursorPagedList<EquipmentForReturnDto>(equipmentToReturn, equipment.HasNextPage, equipment.HasPreviousPage, equipment.StartCursor, equipment.EndCursor, equipment.TotalItems);
            var paginatedResponse = new CursorPaginatedResponse<EquipmentForReturnDto>(mappedPaginatedList);

            return Ok(paginatedResponse);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<EquipmentForReturnDetailedDto>> GetEquipmentDetailedAsync([FromQuery] EquipmentSearchParams searchParams)
        {
            var equipment = await equipmentRepository.SearchDetailedAsync(searchParams);
            Response.AddPagination(equipment.PageNumber, equipment.PageSize, equipment.TotalItems, equipment.TotalPages);
            var equipmentToReturn = mapper.Map<IEnumerable<EquipmentForReturnDetailedDto>>(equipment);

            return Ok(equipmentToReturn);
        }

        [HttpGet("{id}", Name = "GetSingleEquipment")]
        public async Task<ActionResult<EquipmentForReturnDto>> GetSingleEquipmentAsync(int id)
        {
            var equipment = await equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            var equipmentToReturn = mapper.Map<EquipmentForReturnDto>(equipment);

            return Ok(equipmentToReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<EquipmentForReturnDetailedDto>> GetSingleEquipmentDetailedAsync(int id)
        {
            var equipment = await equipmentRepository.GetByIdDetailedAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            var equipmentToReturn = mapper.Map<EquipmentForReturnDetailedDto>(equipment);

            return Ok(equipmentToReturn);
        }

        [HttpGet("{id}/exercises")]
        public async Task<ActionResult<IEnumerable<ExerciseForReturnDto>>> GetExercisesForEquipmentAsync(int id, [FromQuery] PaginationParams searchParams)
        {
            var exerciseSearchParams = new ExerciseSearchParams
            {
                PageNumber = searchParams.PageNumber,
                PageSize = searchParams.PageSize,
                EquipmentId = new List<int> { id }
            };

            var exercises = await exerciseRepository.SearchAsync(exerciseSearchParams);
            Response.AddPagination(exercises);
            var exercisesForReturn = mapper.Map<IEnumerable<ExerciseForReturnDto>>(exercises);

            return Ok(exercisesForReturn);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentForReturnDto>> CreateNewEquipmentAsync([FromBody] EquipmentForCreationDto equipmentCreationDto)
        {
            var equipment = mapper.Map<Equipment>(equipmentCreationDto);

            equipmentRepository.Add(equipment);
            var saveResults = await equipmentRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Failed to create the equipment.", 400, Request));
            }

            var eqReturn = mapper.Map<EquipmentForReturnDto>(equipment);

            return CreatedAtRoute("GetSingleEquipment", new { id = equipment.Id }, eqReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSingleEquipmentAsync(int id)
        {
            var equipment = await equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            equipmentRepository.Delete(equipment);
            var saveResults = await equipmentRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Failed to delete the equipment.", 400, Request));
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<EquipmentForReturnDto>> UpdateEquipmentAsync(int id, [FromBody] JsonPatchDocument<Equipment> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var equipment = await equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(equipment);

            var saveResult = await equipmentRepository.SaveAllAsync();

            if (!saveResult)
            {
                return BadRequest(new ProblemDetailsWithErrors("Could not apply changes.", 400, Request));
            }

            var equipmentToReturn = mapper.Map<EquipmentForReturnDto>(equipment);

            return Ok(equipmentToReturn);
        }
    }
}