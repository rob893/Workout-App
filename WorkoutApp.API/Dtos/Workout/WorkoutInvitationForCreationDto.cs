using Newtonsoft.Json;

namespace WorkoutApp.API.Dtos
{
    public class WorkoutInvitationForCreationDto
    {
        [JsonRequired]
        public int InviteeId { get; set; }

        [JsonRequired]
        public int ScheduledUserWorkoutId { get; set; }
    }
}