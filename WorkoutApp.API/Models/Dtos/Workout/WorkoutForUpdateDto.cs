using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutForUpdateDto
    {
        public string Label { get; set; }
        public bool? Shareable { get; set; }
        public string Color { get; set; }
        public List<ExerciseGroupForCreationDto> ExerciseGroupsToAdd { get; set; }
        public List<int> ExerciseGroupIdsToRemove { get; set; }
    }
}