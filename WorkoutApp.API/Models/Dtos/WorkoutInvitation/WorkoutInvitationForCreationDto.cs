using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutInvitationForCreationDto
    {
        [Required]
        public int? InviteeId { get; set; }

        [Required]
        public int? ScheduledWorkoutId { get; set; }
    }
}