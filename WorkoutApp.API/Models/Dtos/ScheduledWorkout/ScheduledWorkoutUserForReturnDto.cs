namespace WorkoutApp.API.Models.Dtos
{
    public class ScheduledWorkoutUserForReturnDto
    {
        public int UserId { get; set; }
        public int ScheduledWorkoutId { get; set; }
        public string UserUrl { get => $"{baseUrl}/users/{UserId}"; }
        public string ScheduledWorkoutUrl { get => $"{baseUrl}/scheduledWorkouts/{ScheduledWorkoutId}"; }

        private readonly string baseUrl;


        public ScheduledWorkoutUserForReturnDto(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }
    }
}