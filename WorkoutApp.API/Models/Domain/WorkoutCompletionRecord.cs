using System;

namespace WorkoutApp.API.Models.Domain
{
    public class WorkoutCompletionRecord : IIdentifiable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? TargetCompletionTimeInSeconds { get; set; }
        public int? CompletionTimeInSeconds { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public string Notes { get; set; }
    }
}