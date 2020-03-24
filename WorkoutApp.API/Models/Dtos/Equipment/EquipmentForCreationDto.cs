using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class EquipmentForCreationDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}