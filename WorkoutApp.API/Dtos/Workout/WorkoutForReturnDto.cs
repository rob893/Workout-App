using System;
using System.Collections.Generic;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Dtos
{
    public class WorkoutForReturnDto
    {
        public int Id { get; set; }
        public int WorkoutPlanId { get; set; }
        public DateTime Date { get; set; }
        public List<ExerciseGroupForReturnDto> ExerciseGroups { get; set; }
    }
}