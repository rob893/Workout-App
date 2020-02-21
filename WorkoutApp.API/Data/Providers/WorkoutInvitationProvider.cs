using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Helpers.QueryParams;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data.Providers
{
    public class WorkoutInvitationProvider
    {
        private readonly DataContext context;
        private readonly IMapper mapper;


        public WorkoutInvitationProvider(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<WorkoutInvitation> GetWorkoutInvitation(int id)
        {
            return await context.WorkoutInvitations
                .AsNoTracking()
                .Where(woInv => woInv.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutInvitation> GetWorkoutInvitation(int inviteeId, int inviterId, int scheduledWorkoutId)
        {
            return await context.WorkoutInvitations
                .AsNoTracking()
                .Where(woInv => woInv.InviteeId == inviteeId && woInv.InviterId == inviterId && woInv.ScheduledWorkoutId == scheduledWorkoutId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<WorkoutInvitation>> GetPendingInvitationsForInvitee(int inviteeId)
        {
            return await context.WorkoutInvitations
                .AsNoTracking()
                .Where(woInv => woInv.InviteeId == inviteeId && woInv.Accepted == false && woInv.Declined == false)
                .ToListAsync();
        }

        public async Task<List<WorkoutInvitation>> GetPendingSentInvitationsForInviter(int inviterId)
        {
            return await context.WorkoutInvitations
                .AsNoTracking()
                .Where(woInv => woInv.InviterId == inviterId && woInv.Accepted == false && woInv.Declined == false)
                .ToListAsync();
        }
    }
}