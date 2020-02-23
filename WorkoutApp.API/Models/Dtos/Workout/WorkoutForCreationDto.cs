using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutForCreationDto
    {
        [Required]
        public int CreatedByUserId { get; set; }
        public bool Shareable { get; set; }
        public string Color { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]
        public List<ExerciseGroupForCreationDto> ExerciseGroups { get; set; }
    }
}