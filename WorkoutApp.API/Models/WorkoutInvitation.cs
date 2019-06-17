using System;

namespace WorkoutApp.API.Models
{
    public class WorkoutInvitation
    {
        public int Id { get; set; }
        public int InviterId { get; set; }
        public User Inviter { get; set; }
        public int InviteeId { get; set; }
        public User Invitee { get; set; }
        public int ScheduledUserWorkoutId { get; set; }
        public ScheduledUserWorkout ScheduledUserWorkout { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        public DateTime? RespondedAtDateTime { get; set; }
    }
}