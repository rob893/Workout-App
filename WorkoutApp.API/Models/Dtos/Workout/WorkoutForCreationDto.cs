using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutForCreationDto
    {
        [Required]
        [MaxLength(255)]
        public string Label { get; set; }
        [Required]
        public List<ExerciseGroupForCreationDto> ExerciseGroups { get; set; }
        public string Description { get; set; }
        public bool Shareable { get; set; }
    }
}