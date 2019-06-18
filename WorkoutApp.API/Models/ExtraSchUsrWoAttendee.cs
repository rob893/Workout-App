namespace WorkoutApp.API.Models
{
    public class ExtraSchUsrWoAttendee
    {
        public int ScheduledUserWorkoutId { get; set; }
        public ScheduledUserWorkout ScheduledUserWorkout { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}