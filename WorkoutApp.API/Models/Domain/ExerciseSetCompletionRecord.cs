using System;

namespace WorkoutApp.API.Models.Domain
{
    public class ExerciseSetCompletionRecord : IIdentifiable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public int? TargetCompletionTimeInSeconds { get; set; }
        public int? CompletionTimeInSeconds { get; set; }
        public int? TargetReps { get; set; }
        public int? CompletedReps { get; set; }
        public float? TargetWeightInPounds { get; set; }
        public float? CompletedWeightInPounds { get; set; }
        public string Notes { get; set; }
    }
}