using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkoutApp.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Controllers
{
    public partial class UsersController : ControllerBase
    {
        [HttpGet("{userId}/workoutInvitations")]
        public async Task<ActionResult<IEnumerable<WorkoutInvitationForReturnDto>>> GetWorkoutInvitationsForUserAsync(int userId, [FromQuery] PaginationParams searchParams, [FromQuery] string status)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var invSearchParams = new WorkoutInvitationSearchParams
            {
                PageNumber = searchParams.PageNumber,
                PageSize = searchParams.PageSize,
                InviteeId = userId,
                Status = status
            };

            var invitations = await workoutInvitationRepository.SearchAsync(invSearchParams);
            Response.AddPagination(invitations);
            var invitationsToReturn = mapper.Map<IEnumerable<WorkoutInvitationForReturnDto>>(invitations);

            return Ok(invitationsToReturn);
        }

        [HttpGet("{userId}/workoutInvitations/sent")]
        public async Task<ActionResult<IEnumerable<WorkoutInvitationForReturnDto>>> GetSentWorkoutInvitationsForUserAsync(int userId, [FromQuery] PaginationParams searchParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var invSearchParams = new WorkoutInvitationSearchParams
            {
                PageNumber = searchParams.PageNumber,
                PageSize = searchParams.PageSize,
                InviterId = userId
            };

            var invitations = await workoutInvitationRepository.SearchAsync(invSearchParams);
            Response.AddPagination(invitations);
            var invitationsToReturn = mapper.Map<IEnumerable<WorkoutInvitationForReturnDto>>(invitations);

            return Ok(invitationsToReturn);
        }
    }
}