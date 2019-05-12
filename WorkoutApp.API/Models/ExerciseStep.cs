namespace WorkoutApp.API.Models
{
    public class ExerciseStep
    {
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int ExerciseStepNumber { get; set; }
        public string Description { get; set; }
    }
}