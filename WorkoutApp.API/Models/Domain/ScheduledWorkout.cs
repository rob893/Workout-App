using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models.Domain
{
    public class ScheduledWorkout : IIdentifiable
    {
        public int Id { get; set; }
        public User ScheduledByUser { get; set; }
        public int ScheduledByUserId { get; set; }
        public List<ScheduledWorkoutUser> Attendees { get; set; }
        public Workout Workout { get; set; }
        public int WorkoutId { get; set; }
        public List<ExerciseGroup> AdHocExercises { get; set; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public List<WorkoutCompletionRecord> WorkoutCompletionRecords { get; set; }
        public List<ExerciseGroupCompletionRecord> ExerciseGroupCompletionRecords { get; set; }
        public List<ExerciseSetCompletionRecord> ExerciseSetCompletionRecords { get; set; }
    }
}