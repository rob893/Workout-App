namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutInvitationForReturnDetailedDto : WorkoutInvitationForReturnDto
    {
        public UserForReturnDto Inviter { get; set; }
        public UserForReturnDto Invitee { get; set; }
        public ScheduledWorkoutForReturnDto ScheduledWorkout { get; set; }


        public WorkoutInvitationForReturnDetailedDto(string baseUrl) : base(baseUrl) { }
    }
}