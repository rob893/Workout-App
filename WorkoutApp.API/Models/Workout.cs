using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public User CreatedByUser { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool Shareable { get; set; }
        public bool IsDeleted { get; set; }
        public Workout WorkoutCopiedFrom { get; set; }
        public List<ExerciseGroup> ExerciseGroups { get; set; }
    }
}