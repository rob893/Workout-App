namespace WorkoutApp.API.Models.Domain
{
    public class ExerciseGroup : IIdentifiable
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public Workout Workout { get; set; }
        public ScheduledWorkout ScheduledWorkout { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public int TimeInSeconds { get; set; }
    }
}