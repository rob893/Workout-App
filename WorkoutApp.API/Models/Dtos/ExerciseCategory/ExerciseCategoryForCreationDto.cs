using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseCategoryForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}