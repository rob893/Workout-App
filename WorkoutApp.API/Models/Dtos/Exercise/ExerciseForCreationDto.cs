using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public int? PrimaryMuscleId { get; set; }
        public int? SecondaryMuscleId { get; set; }
        [Required]
        public List<ExerciseStep> ExerciseSteps { get; set; }
        public List<int> EquipmentIds { get; set; }
        [Required]
        public List<int> ExerciseCategoryIds { get; set; }
    }
}