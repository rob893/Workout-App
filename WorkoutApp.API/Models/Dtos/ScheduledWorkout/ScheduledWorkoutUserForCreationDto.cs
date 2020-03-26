using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class ScheduledWorkoutUserForCreationDto
    {
        [Required]
        public int UserId { get; set; }
    }
}