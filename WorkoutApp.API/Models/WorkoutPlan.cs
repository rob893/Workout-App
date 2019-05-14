using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Workout> Workouts { get; set; }
    }
}