namespace WorkoutApp.API.Models.Domain
{
    public class ExerciseCategoryExercise
    {
        public int ExerciseCategoryId { get; set; }
        public ExerciseCategory ExerciseCategory { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}