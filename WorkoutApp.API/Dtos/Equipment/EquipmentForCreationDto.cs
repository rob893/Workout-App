using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Dtos
{
    public class EquipmentForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}