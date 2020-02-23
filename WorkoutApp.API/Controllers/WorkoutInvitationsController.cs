using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Data;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data.Providers;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkoutInvitationsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWorkoutRepository repo;
        private readonly WorkoutInvitationProvider workoutInvitationProvider;


        public WorkoutInvitationsController(IMapper mapper, IWorkoutRepository repo, WorkoutInvitationProvider workoutInvitationProvider)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.workoutInvitationProvider = workoutInvitationProvider;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkoutInvitation(int id)
        {
            WorkoutInvitation woInv = await workoutInvitationProvider.GetWorkoutInvitation(id);

            if (woInv == null)
            {
                return NotFound();
            }

            return Ok(woInv);
        }

        [HttpGet("pending/invitee/{inviteeId}")]
        public async Task<IActionResult> GetPendingInvitationsForInvitee(int inviteeId)
        {
            if (inviteeId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            List<WorkoutInvitation> woInvs = await workoutInvitationProvider.GetPendingInvitationsForInvitee(inviteeId);

            return Ok(woInvs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePendingInvitation(int id)
        {
            WorkoutInvitation woInv = await repo.GetWorkoutInvitationAsync(id);

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
                return BadRequest("Cannot delete an invitation that has already been responded to.");
            }

            repo.Delete<WorkoutInvitation>(woInv);

            if (await repo.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest("Could not delete invitation.");
        }

        [HttpPatch("{id}/accept")]
        public async Task<IActionResult> AcceptPendingInvitation(int id)
        {
            WorkoutInvitation woInv = await repo.GetWorkoutInvitationAsync(id);

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
                return BadRequest("This invitation has already been responeded to.");
            }

            var user = await repo.GetUserAsync(woInv.InviteeId);

            woInv.Accepted = true;
            woInv.Declined = false;
            woInv.RespondedAtDateTime = DateTime.Now;

            ScheduledWorkout schWo = await repo.GetScheduledUserWorkoutAsync(woInv.ScheduledWorkoutId);
            schWo.Attendees.Add(new ScheduledWorkoutUser
            {
                User = user,
                ScheduledWorkout = schWo
            });

            if (await repo.SaveAllAsync())
            {
                return Ok(woInv);
            }

            return BadRequest("Unable to accept invitation.");
        }

        [HttpPatch("{id}/decline")]
        public async Task<IActionResult> DeclinePendingInvitation(int id)
        {
            WorkoutInvitation woInv = await repo.GetWorkoutInvitationAsync(id);

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
                return BadRequest("This invitation has already been responeded to.");
            }

            woInv.Accepted = false;
            woInv.Declined = true;
            woInv.RespondedAtDateTime = DateTime.Now;

            if (await repo.SaveAllAsync())
            {
                return Ok(woInv);
            }

            return BadRequest("Unable to decline invitation.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkoutInvitation([FromBody] WorkoutInvitationForCreationDto woInv)
        {
            ScheduledWorkout scheduledWorkout = await repo.GetScheduledUserWorkoutAsync(woInv.ScheduledUserWorkoutId);
            
            if (await repo.GetUserAsync(woInv.InviteeId) == null || scheduledWorkout == null)
            {
                return NotFound();
            }

            int inviterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (inviterId != scheduledWorkout.ScheduledByUserId)
            {
                return BadRequest("You cannot invite people to workouts that are not yours!");
            }

            if (inviterId == woInv.InviteeId)
            {
                return BadRequest("You cannot invite yourself!");
            }

            if (await workoutInvitationProvider.GetWorkoutInvitation(woInv.InviteeId, inviterId, woInv.ScheduledUserWorkoutId) != null)
            {
                return BadRequest("This person already has a pending invitation from you for this workout!");
            }

            WorkoutInvitation newInvitation = new WorkoutInvitation();

            newInvitation.InviteeId = woInv.InviteeId;
            newInvitation.InviterId = inviterId;
            newInvitation.ScheduledWorkoutId = woInv.ScheduledUserWorkoutId;

            repo.Add<WorkoutInvitation>(newInvitation);

            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetWorkoutInvitation), new { id = newInvitation.Id }, newInvitation);
            }

            return BadRequest("Could not send invitation.");
        }
    }
}