using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int WorkoutPlanId { get; set; }
        public bool Complete { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }
        public DateTime Date { get; set; }
        public List<ExerciseGroup> ExerciseGroups { get; set; }
    }
}