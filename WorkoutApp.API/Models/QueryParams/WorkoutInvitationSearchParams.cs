namespace WorkoutApp.API.Models.QueryParams
{
    public class WorkoutInvitationSearchParams : PaginationParams
    {
        public string Status { get; set; }
        public int? InviteeId { get; set; }
        public int? InviterId { get; set; }
        public int? ScheduledWorkoutId { get; set; }
    }
}