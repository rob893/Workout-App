using System;

namespace WorkoutApp.API.Models.Domain
{
    public class ExerciseGroupCompletionRecord : IIdentifiable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int ExerciseGroupId { get; set; }
        public ExerciseGroup ExerciseGroup { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public int? TargetCompletionTimeInSeconds { get; set; }
        public int? CompletionTimeInSeconds { get; set; }
        public int? TargetSets { get; set; }
        public int? CompletedSets { get; set; }
        public string Notes { get; set; }
    }
}