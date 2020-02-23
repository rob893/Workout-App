using System;
using System.Collections.Generic;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutForReturnDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Color { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool Shareable { get; set; }
        public bool IsDeleted { get; set; }
        public List<ExerciseGroupForReturnDto> ExerciseGroups { get; set; }
    }
}