using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WorkoutApp.API.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public List<UserFavoriteExercise> FavoriteExercises { get; set; }
        public List<Workout> CreatedWorkouts { get; set; }
        public List<ScheduledWorkout> OwnedScheduledWorkouts { get; set; }
        public List<ScheduledWorkout> ScheduledWorkouts { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<WorkoutInvitation> ReceivedInvitations { get; set; }
        public List<WorkoutInvitation> SentInvitations { get; set; }
        public List<WorkoutCompletionRecord> WorkoutCompletionRecords { get; set; }
        public List<ExerciseGroupCompletionRecord> ExerciseGroupCompletionRecords { get; set; }
        public List<ExerciseSetCompletionRecord> ExerciseSetCompletionRecords { get; set; }
    }
}