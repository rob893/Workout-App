using System.Collections.Generic;

namespace WorkoutApp.API.Models.Domain
{
    public class ExerciseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ExerciseCategoryExercise> Exercises { get; set; }
    }
}