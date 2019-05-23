using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class ScheduledUserWorkouts
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Workout Workout { get; set; }
        public int WorkoutId { get; set; }
        public DateTime StartedDateTime { get; set; }
        public DateTime CompletedDateTime { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        //public List<User> UsersSharedWith { get; set; } //perhaps invite another user to a scheduled workout? Think more
    }
}