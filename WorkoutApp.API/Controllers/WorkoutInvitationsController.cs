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

            woInv.Accepted = false;
            woInv.Declined = true;
            woInv.RespondedAtDateTime = DateTime.Now;

            if (await repo.SaveAll())
            {
                return Ok("Invitation Declined!");
            }

            return BadRequest("Unable to decline invitation.");
        }

        [HttpPost("scheduledWorkout/{scheduledWorkoutId}/invitee/{inviteeId}")]
        public async Task<IActionResult> SendWorkoutInvitation(int scheduledWorkoutId, int inviteeId)
        {
            ScheduledUserWorkout scheduledWorkout = await repo.GetScheduledUserWorkout(scheduledWorkoutId);
            
            if (await repo.GetUser(inviteeId) == null || scheduledWorkout == null)
            {
                return NotFound();
            }

            int inviterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (inviterId != scheduledWorkout.UserId)
            {
                return BadRequest("You cannot invite people to workouts that are not yours!");
            }

            if (inviterId == inviteeId)
            {
                return BadRequest("You cannot invite yourself!");
            }

            if (await workoutInvitationProvider.GetWorkoutInvitation(inviteeId, inviterId, scheduledWorkoutId) != null)
            {
                return BadRequest("This person already has a pending invitation from you for this workout!");
            }

            WorkoutInvitation newInvitation = new WorkoutInvitation();

            newInvitation.InviteeId = inviteeId;
            newInvitation.InviterId = inviterId;
            newInvitation.ScheduledUserWorkoutId = scheduledWorkoutId;

            repo.Add<WorkoutInvitation>(newInvitation);

            if (await repo.SaveAll())
            {
                return Ok("Invitation sent!");
            }

            return BadRequest("Could not send invitation.");
        }
    }
}