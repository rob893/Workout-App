using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class ScheduledWorkoutForReturnDetailedDto
    {
        public int Id { get; set; }
        public UserForReturnDto ScheduledByUser { get; set; }
        public int ScheduledByUserId { get; set; }
        public List<UserForReturnDto> Attendees { get; set; }
        public WorkoutForReturnDetailedDto Workout { get; set; }
        public int WorkoutId { get; set; }
        public List<ExerciseGroupForReturnDto> AdHocExercises { get; set; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public DateTime ScheduledDateTime { get; set; }
    }
}