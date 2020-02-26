using System;

namespace WorkoutApp.API.Models.Dtos
{
    public class ScheduledWorkoutForReturnDto
    {
        public int Id { get; set; }
        public int ScheduledByUserId { get; set; }
        public int WorkoutId { get; set; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public DateTime ScheduledDateTime { get; set; }
    }
}