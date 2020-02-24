using System;

namespace WorkoutApp.API.Models.Domain
{
    public class WorkoutInvitation : IIdentifiable
    {
        public int Id { get; set; }
        public int InviterId { get; set; }
        public User Inviter { get; set; }
        public int InviteeId { get; set; }
        public User Invitee { get; set; }
        public int ScheduledWorkoutId { get; set; }
        public ScheduledWorkout ScheduledWorkout { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        public DateTime? RespondedAtDateTime { get; set; }
    }
}