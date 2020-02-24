namespace WorkoutApp.API.Models.Domain
{
    public class EquipmentExercise
    {
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
    }
}