using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Dtos
{
    public class ScheduledWoForCreationDto
    {
        [Required]
        public int WorkoutId { get; set; }
        [Required]
        public DateTime ScheduledDateTime { get; set; }
    }
}