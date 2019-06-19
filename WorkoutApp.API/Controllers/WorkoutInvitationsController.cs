using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Data;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Helpers.QueryParams;
using WorkoutApp.API.Helpers.Specifications;
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

        [HttpPatch("{id}/accept")]
        public async Task<IActionResult> AcceptPendingInvitation(int id)
        {
            WorkoutInvitation woInv = await repo.GetWorkoutInvitation(id);

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

            woInv.Accepted = true;
            woInv.Declined = false;
            woInv.RespondedAtDateTime = DateTime.Now;

            ScheduledUserWorkout schWo = await repo.GetScheduledUserWorkout(woInv.ScheduledUserWorkoutId);
            schWo.ExtraSchUsrWoAttendees.Add(new ExtraSchUsrWoAttendee
            {
                ScheduledUserWorkoutId = schWo.Id,
                UserId = woInv.InviteeId
            });

            if (await repo.SaveAll())
            {
                return Ok("Invitation Accepted!");
            }

            return BadRequest("Unable to accept invitation.");
        }

        [HttpPatch("{id}/decline")]
        public async Task<IActionResult> DeclinePendingInvitation(int id)
        {
            WorkoutInvitation woInv = await repo.GetWorkoutInvitation(id);

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

            if (await repo.SaveAll())
            {
                return Ok("Invitation Declined!");
            }

            return BadRequest("Unable to decline invitation.");
        }

        [HttpPost()]
        public async Task<IActionResult> CreateWorkoutInvitation([FromBody] WorkoutInvitation woInv)
        {
            ScheduledUserWorkout scheduledWorkout = await repo.GetScheduledUserWorkout(woInv.ScheduledUserWorkoutId);
            
            if (await repo.GetUser(woInv.InviteeId) == null || scheduledWorkout == null)
            {
                return NotFound();
            }

            int inviterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (inviterId != scheduledWorkout.UserId)
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
            newInvitation.ScheduledUserWorkoutId = woInv.ScheduledUserWorkoutId;

            repo.Add<WorkoutInvitation>(newInvitation);

            if (await repo.SaveAll())
            {
                return CreatedAtAction(nameof(GetWorkoutInvitation), new { id = newInvitation.Id }, newInvitation);
            }

            return BadRequest("Could not send invitation.");
        }
    }
}