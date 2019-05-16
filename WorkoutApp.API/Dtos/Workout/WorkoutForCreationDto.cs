using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Dtos
{
    public class WorkoutForCreationDto
    {
    
        public int WorkoutPlanId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public List<ExerciseGroupForCreationDto> ExerciseGroups { get; set; }
    }
}