using System.Collections.Generic;

namespace WorkoutApp.API.Models.Domain
{
    public class Exercise : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Muscle PrimaryMuscle { get; set; }
        public Muscle SecondaryMuscle { get; set; }
        public List<UserFavoriteExercise> FavoritedBy { get; set; }
        public List<ExerciseStep> ExerciseSteps { get; set; }
        public List<EquipmentExercise> Equipment { get; set; }
        public List<ExerciseCategoryExercise> ExerciseCategorys { get; set; }
    }
}