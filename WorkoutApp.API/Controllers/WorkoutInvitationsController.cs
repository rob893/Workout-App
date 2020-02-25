using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data.Repositories;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.QueryParams;
using System.Linq;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkoutInvitationsController : ControllerBase
    {
        private readonly WorkoutInvitationRepository workoutInvitationRepository;
        private readonly ScheduledWorkoutRepository scheduledWorkoutRepository;
        private readonly UserRepository userRepository;
        private readonly IMapper mapper;


        public WorkoutInvitationsController(
            WorkoutInvitationRepository workoutInvitationRepository, 
            ScheduledWorkoutRepository scheduledWorkoutRepository, 
            UserRepository userRepository,
            IMapper mapper)
        {
            this.workoutInvitationRepository = workoutInvitationRepository;
            this.scheduledWorkoutRepository = scheduledWorkoutRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutInvitationForReturnDto>>> GetWorkoutInvitationsAsync([FromQuery] WorkoutInvitationSearchParams searchParams)
        {
            var invitations = await workoutInvitationRepository.SearchAsync(searchParams);
            Response.AddPagination(invitations);
            var invitationsForReturn = mapper.Map<IEnumerable<WorkoutInvitationForReturnDto>>(invitations);

            return Ok(invitationsForReturn);
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<WorkoutInvitationForReturnDetailedDto>>> GetWorkoutInvitationsDetailedAsync([FromQuery] WorkoutInvitationSearchParams searchParams)
        {
            var invitations = await workoutInvitationRepository.SearchDetailedAsync(searchParams);
            Response.AddPagination(invitations);
            var invitationsForReturn = mapper.Map<IEnumerable<WorkoutInvitationForReturnDetailedDto>>(invitations);

            return Ok(invitationsForReturn);
        }

        [HttpGet("{id}", Name = "GetWorkoutInvitation")]
        public async Task<ActionResult<WorkoutInvitationForReturnDto>> GetWorkoutInvitationAsync(int id)
        {
            var invitation = await workoutInvitationRepository.GetByIdAsync(id);

            if (invitation == null)
            {
                return NotFound();
            }

            var invitationForReturn = mapper.Map<WorkoutInvitationForReturnDto>(invitation);

            return Ok(invitationForReturn);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<WorkoutInvitationForReturnDetailedDto>> GetWorkoutInvitationDetailedAsync(int id)
        {
            var invitation = await workoutInvitationRepository.GetByIdDetailedAsync(id);

            if (invitation == null)
            {
                return NotFound();
            }

            var invitationForReturn = mapper.Map<WorkoutInvitationForReturnDetailedDto>(invitation);

            return Ok(invitationForReturn);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutInvitationForReturnDto>> CreateWorkoutInvitationAsync([FromBody] WorkoutInvitationForCreationDto woInv)
        {
            var scheduledWorkout = await scheduledWorkoutRepository.GetByIdAsync(woInv.ScheduledWorkoutId.Value);
            var invitee = await userRepository.GetByIdAsync(woInv.InviteeId.Value);
            
            if (invitee == null || scheduledWorkout == null)
            {
                return BadRequest(new ProblemDetailsWithErrors("Invalid workout id or invitee id.", 400, Request));
            }

            int inviterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (inviterId != scheduledWorkout.ScheduledByUserId)
            {
                return BadRequest(new ProblemDetailsWithErrors("You cannot invite people to workouts that are not yours.", 400, Request));
            }

            if (inviterId == woInv.InviteeId)
            {
                return BadRequest(new ProblemDetailsWithErrors("You cannot invite yourself to your own workout.", 400, Request));
            }

            var pendingInvitation = await workoutInvitationRepository.SearchAsync(new WorkoutInvitationSearchParams
            {
                InviteeId = woInv.InviteeId,
                InviterId = inviterId,
                ScheduledWorkoutId = woInv.ScheduledWorkoutId
            });

            if (pendingInvitation.Count > 0)
            {
                return BadRequest(new ProblemDetailsWithErrors("This person already has a pending invitation from you for this workout.", 400, Request));
            }

            var newInvitation = new WorkoutInvitation
            {
                InviteeId = woInv.InviteeId.Value,
                InviterId = inviterId,
                ScheduledWorkoutId = woInv.ScheduledWorkoutId.Value
            };

            workoutInvitationRepository.Add(newInvitation);
            var saveResults = await workoutInvitationRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Failed to send invitation.", 400, Request));
            }

            return CreatedAtRoute("GetWorkoutInvitation", new { id = newInvitation.Id }, newInvitation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePendingInvitationAsync(int id)
        {
            var woInv = await workoutInvitationRepository.GetByIdAsync(id);

            if (woInv == null) 
            {
                return NotFound();
            }

            if (woInv.InviteeId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (woInv.Accepted || woInv.Declined)
            {
                return BadRequest(new ProblemDetailsWithErrors("You cannot delete an invitation that has already been responded to.", 400, Request));
            }

            workoutInvitationRepository.Delete(woInv);
            var saveResults = await workoutInvitationRepository.SaveAllAsync();

            if (!saveResults)
            {
                return BadRequest(new ProblemDetailsWithErrors("Failed to delete.", 400, Request));
            }

            return NoContent();
        }

        // [HttpGet("pending/invitee/{inviteeId}")]
        // public async Task<IActionResult> GetPendingInvitationsForInvitee(int inviteeId)
        // {
        //     if (inviteeId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //     {
        //         return Unauthorized();
        //     }

        //     List<WorkoutInvitation> woInvs = await workoutInvitationProvider.GetPendingInvitationsForInvitee(inviteeId);

        //     return Ok(woInvs);
        // }

        // [HttpPatch("{id}/accept")]
        // public async Task<IActionResult> AcceptPendingInvitation(int id)
        // {
        //     WorkoutInvitation woInv = await repo.GetWorkoutInvitationAsync(id);

        //     if (woInv == null)
        //     {
        //         return NotFound();
        //     }

        //     if (woInv.InviteeId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //     {
        //         return Unauthorized();
        //     }

        //     if (woInv.Accepted || woInv.Declined)
        //     {
        //         return BadRequest("This invitation has already been responeded to.");
        //     }

        //     var user = await repo.GetUserAsync(woInv.InviteeId);

        //     woInv.Accepted = true;
        //     woInv.Declined = false;
        //     woInv.RespondedAtDateTime = DateTime.Now;

        //     ScheduledWorkout schWo = await repo.GetScheduledUserWorkoutAsync(woInv.ScheduledWorkoutId);
        //     schWo.Attendees.Add(new ScheduledWorkoutUser
        //     {
        //         User = user,
        //         ScheduledWorkout = schWo
        //     });

        //     if (await repo.SaveAllAsync())
        //     {
        //         return Ok(woInv);
        //     }

        //     return BadRequest("Unable to accept invitation.");
        // }

        // [HttpPatch("{id}/decline")]
        // public async Task<IActionResult> DeclinePendingInvitation(int id)
        // {
        //     WorkoutInvitation woInv = await repo.GetWorkoutInvitationAsync(id);

        //     if (woInv == null)
        //     {
        //         return NotFound();
        //     }

        //     if (woInv.InviteeId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //     {
        //         return Unauthorized();
        //     }

        //     if (woInv.Accepted || woInv.Declined)
        //     {
        //         return BadRequest("This invitation has already been responeded to.");
        //     }

        //     woInv.Accepted = false;
        //     woInv.Declined = true;
        //     woInv.RespondedAtDateTime = DateTime.Now;

        //     if (await repo.SaveAllAsync())
        //     {
        //         return Ok(woInv);
        //     }

        //     return BadRequest("Unable to decline invitation.");
        // }
    }
}