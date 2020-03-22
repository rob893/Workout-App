using System;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutInvitationForReturnDto : BaseDtoForReturn
    {
        public int InviterId { get; set; }
        public string InviterUrl { get => $"{baseUrl}/users/{InviterId}"; }
        public int InviteeId { get; set; }
        public string InviteeUrl { get => $"{baseUrl}/users/{InviteeId}"; }
        public int ScheduledWorkoutId { get; set; }
        public string ScheduledWorkoutUrl { get => $"{baseUrl}/scheduledWorkouts/{ScheduledWorkoutId}"; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        public DateTime? RespondedAtDateTime { get; set; }


        public WorkoutInvitationForReturnDto(string baseUrl) : base(baseUrl, "workoutInvitations") { }
    }
}