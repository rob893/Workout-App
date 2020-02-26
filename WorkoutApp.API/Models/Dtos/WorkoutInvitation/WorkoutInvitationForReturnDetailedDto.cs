using System;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutInvitationForReturnDetailedDto
    {
        public int Id { get; set; }
        public int InviterId { get; set; }
        public UserForReturnDto Inviter { get; set; }
        public int InviteeId { get; set; }
        public UserForReturnDto Invitee { get; set; }
        public int ScheduledWorkoutId { get; set; }
        public ScheduledWorkoutForReturnDto ScheduledWorkout { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        public DateTime? RespondedAtDateTime { get; set; }
    }
}