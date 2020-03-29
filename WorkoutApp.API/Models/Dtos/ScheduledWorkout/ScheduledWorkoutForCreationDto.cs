using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Models.Dtos
{
    public class ScheduledWorkoutForCreationDto
    {
        [Required]
        public int WorkoutId { get; set; }
        [Required]
        public DateTimeOffset ScheduledDateTime { get; set; }
        public string CustomWorkout { get; set; }
        public List<ExerciseGroupForCreationDto> AdHocExercises { get; set; }
    }
}