using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class MuscleForCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}