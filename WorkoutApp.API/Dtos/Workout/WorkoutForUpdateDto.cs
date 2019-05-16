using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Dtos
{
    public class WorkoutForUpdateDto
    {
        public DateTime? Date { get; set; }
        public List<ExerciseGroupForCreationDto> ExerciseGroups { get; set; }
    }
}