using System;

namespace WorkoutApp.API.Models
{
    public class ExerciseGroupCompletionRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? TargetCompletionTimeInSeconds { get; set; }
        public int? CompletionTimeInSeconds { get; set; }
        public int? TargetSets { get; set; }
        public int? CompletedSets { get; set; }
        public string Notes { get; set; }
    }
}