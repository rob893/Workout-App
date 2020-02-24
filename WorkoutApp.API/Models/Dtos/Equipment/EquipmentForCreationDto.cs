using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class EquipmentForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}