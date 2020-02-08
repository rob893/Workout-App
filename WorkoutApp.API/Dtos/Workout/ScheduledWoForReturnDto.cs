using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Dtos
{
    public class ScheduledWoForReturnDto
    {
        public int Id { get; set; }
        public UserForReturnDto User { get; set; }
        public WorkoutForReturnDto Workout { get; set; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public List<ExerciseGroupForReturnDto> AdHocExercises { get; set; }
        public List<UserForReturnDto> ExtraSchUsrWoAttendees { get; set; }
    }
}