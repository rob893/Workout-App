using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Domain
{
    public class Workout : IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Label { get; set; }
        public string Description { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public DateTimeOffset CreatedOnDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        public bool Shareable { get; set; }
        public bool IsDeleted { get; set; }
        public Workout WorkoutCopiedFrom { get; set; }
        public List<ExerciseGroup> ExerciseGroups { get; set; }
    }
}