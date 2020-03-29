using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WorkoutApp.API.Models.Domain
{
    public class User : IdentityUser<int>, IIdentifiable
    {
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
        public DateTimeOffset Created { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public List<UserFavoriteExercise> FavoriteExercises { get; set; }
        public List<Workout> CreatedWorkouts { get; set; }
        public List<ScheduledWorkout> OwnedScheduledWorkouts { get; set; }
        public List<ScheduledWorkoutUser> ScheduledWorkouts { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<WorkoutInvitation> ReceivedInvitations { get; set; }
        public List<WorkoutInvitation> SentInvitations { get; set; }
        public List<WorkoutCompletionRecord> WorkoutCompletionRecords { get; set; }
        public List<ExerciseGroupCompletionRecord> ExerciseGroupCompletionRecords { get; set; }
        public List<ExerciseSetCompletionRecord> ExerciseSetCompletionRecords { get; set; }
    }
}