using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Models.Dtos;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Controllers
{
    public partial class UsersController : ControllerBase
    {
        [HttpGet("{userId}/workoutInvitations")]
        public async Task<ActionResult<CursorPaginatedResponse<WorkoutInvitationForReturnDto>>> GetWorkoutInvitationsForUserAsync(int userId, [FromQuery] CursorPaginationParams searchParams, [FromQuery] string status)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var invSearchParams = new WorkoutInvitationSearchParams
            {
                First = searchParams.First,
                After = searchParams.After,
                Last = searchParams.Last,
                Before = searchParams.Before,
                IncludeTotal = searchParams.IncludeTotal,
                InviteeId = userId,
                Status = status
            };

            var invitations = await workoutInvitationRepository.SearchAsync(invSearchParams);
            var paginatedResponse = CursorPaginatedResponse<WorkoutInvitationForReturnDto>.CreateFrom(invitations, mapper.Map<IEnumerable<WorkoutInvitationForReturnDto>>);

            return Ok(paginatedResponse);
        }

        [HttpGet("{userId}/workoutInvitations/sent")]
        public async Task<ActionResult<CursorPaginatedResponse<WorkoutInvitationForReturnDto>>> GetSentWorkoutInvitationsForUserAsync(int userId, [FromQuery] CursorPaginationParams searchParams, string status)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var invSearchParams = new WorkoutInvitationSearchParams
            {
                First = searchParams.First,
                After = searchParams.After,
                Last = searchParams.Last,
                Before = searchParams.Before,
                IncludeTotal = searchParams.IncludeTotal,
                InviterId = userId,
                Status = status
            };

            var invitations = await workoutInvitationRepository.SearchAsync(invSearchParams);
            var paginatedResponse = CursorPaginatedResponse<WorkoutInvitationForReturnDto>.CreateFrom(invitations, mapper.Map<IEnumerable<WorkoutInvitationForReturnDto>>);

            return Ok(paginatedResponse);
        }
    }
}