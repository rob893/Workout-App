using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class WorkoutInvitationRepository : Repository<WorkoutInvitation, WorkoutInvitationSearchParams>
    {
        public WorkoutInvitationRepository(DataContext context) : base(context) { }

        protected override IQueryable<WorkoutInvitation> AddDetailedIncludes(IQueryable<WorkoutInvitation> query)
        {
            return query
                .Include(i => i.Inviter)
                .Include(i => i.Invitee)
                .Include(i => i.ScheduledWorkout);
        }

        protected override IQueryable<WorkoutInvitation> AddWhereClauses(IQueryable<WorkoutInvitation> query, WorkoutInvitationSearchParams searchParams)
        {
            if (searchParams.InviteeId != null)
            {
                query = query.Where(i => i.InviteeId == searchParams.InviteeId);
            }

            if (searchParams.InviterId != null)
            {
                query = query.Where(i => i.InviterId == searchParams.InviterId);
            }

            if (searchParams.ScheduledWorkoutId != null)
            {
                query = query.Where(i => i.ScheduledWorkoutId == searchParams.ScheduledWorkoutId);
            }

            if (searchParams.Status != null)
            {
                var status = searchParams.Status.ToLower();

                if (status == WorkoutInvitationStatus.Accepted)
                {
                    query = query.Where(i => i.Accepted == true && i.Declined == false);
                }
                else if (status == WorkoutInvitationStatus.Declined)
                {
                    query = query.Where(i => i.Accepted == false && i.Declined == true);
                }
                else if (status == WorkoutInvitationStatus.Pending)
                {
                    query = query.Where(i => i.Accepted == false && i.Declined == false);
                }
            }

            return query;
        }
    }
}