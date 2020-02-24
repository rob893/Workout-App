using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutForUpdateDto
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public bool? Shareable { get; set; }
        public string Color { get; set; }
        public List<ExerciseGroupForCreationDto> ExerciseGroups { get; set; } = new List<ExerciseGroupForCreationDto>();
    }
}