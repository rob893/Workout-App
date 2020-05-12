using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseGroupForReturnDto : IIdentifiable
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public ExerciseForReturnDto Exercise { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
    }
}