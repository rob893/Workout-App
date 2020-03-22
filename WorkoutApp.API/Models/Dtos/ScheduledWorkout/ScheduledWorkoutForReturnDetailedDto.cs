using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class ScheduledWorkoutForReturnDetailedDto : ScheduledWorkoutForReturnDto
    {
        public UserForReturnDto ScheduledByUser { get; set; }
        public List<UserForReturnDto> Attendees { get; set; }
        public WorkoutForReturnDetailedDto Workout { get; set; }
        public List<ExerciseGroupForReturnDto> AdHocExercises { get; set; }


        public ScheduledWorkoutForReturnDetailedDto(string baseUrl) : base(baseUrl) { }
    }
}