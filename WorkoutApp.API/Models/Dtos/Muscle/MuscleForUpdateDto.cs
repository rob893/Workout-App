using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class MuscleForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}