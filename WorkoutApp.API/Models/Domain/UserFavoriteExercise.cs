namespace WorkoutApp.API.Models.Domain
{
    public class UserFavoriteExercise
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}