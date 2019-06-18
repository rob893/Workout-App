using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class ScheduledUserWorkout
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Workout Workout { get; set; }
        public int WorkoutId { get; set; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public List<ExerciseGroup> AdHocExercises { get; set; }
        public List<ExtraSchUsrWoAttendee> ExtraSchUsrWoAttendees { get; set; }
    }
}