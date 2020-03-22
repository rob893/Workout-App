using System;

namespace WorkoutApp.API.Models.Dtos
{
    public class ScheduledWorkoutForReturnDto : BaseDtoForReturn
    {
        public int ScheduledByUserId { get; set; }
        public string ScheduledByUserUrl { get => $"{baseUrl}/users/{ScheduledByUserId}"; }
        public int WorkoutId { get; set; }
        public string WorkoutUrl { get => $"{baseUrl}/workouts/{WorkoutId}"; }
        public string AttendeesUrl { get => $"{Url}/attendees"; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public DateTime ScheduledDateTime { get; set; }


        public ScheduledWorkoutForReturnDto(string baseUrl) : base(baseUrl, "scheduledWorkouts") { }
    }
}