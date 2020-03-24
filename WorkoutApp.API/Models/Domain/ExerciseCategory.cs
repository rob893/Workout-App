using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Domain
{
    public class ExerciseCategory : IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public List<ExerciseCategoryExercise> Exercises { get; set; }
    }
}