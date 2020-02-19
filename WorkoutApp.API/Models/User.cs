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
        public List<ScheduledUserWorkout> ScheduledUserWorkouts { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<WorkoutInvitation> ReceivedInvitations { get; set; }
        public List<WorkoutInvitation> SentInvitations { get; set; }
    }
}