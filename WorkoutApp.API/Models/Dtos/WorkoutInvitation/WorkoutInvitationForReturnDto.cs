using System;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutInvitationForReturnDto
    {
        public int Id { get; set; }
        public int InviterId { get; set; }
        public int InviteeId { get; set; }
        public int ScheduledWorkoutId { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        public DateTime? RespondedAtDateTime { get; set; }
    }
}