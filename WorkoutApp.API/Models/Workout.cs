using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ExerciseGroup> ExerciseGroups { get; set; }
    }
}