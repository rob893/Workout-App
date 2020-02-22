namespace WorkoutApp.API.Models
{
    public class ScheduledWorkoutUser
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ScheduledWorkoutId { get; set; }
        public ScheduledWorkout ScheduledWorkout { get; set; }
    }
}