using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutForReturnDetailedDto : WorkoutForReturnDto
    {
        public UserForReturnDto CreatedByUser { get; set; }
        public WorkoutForReturnDto WorkoutCopiedFrom { get; set; }
        public List<ExerciseGroupForReturnDto> ExerciseGroups { get; set; }


        public WorkoutForReturnDetailedDto(string baseUrl) : base(baseUrl) { }
    }
}