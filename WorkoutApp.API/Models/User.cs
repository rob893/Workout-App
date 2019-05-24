using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Created { get; set; }
        public List<Workout> CreatedWorkouts { get; set; }
        public List<ScheduledUserWorkout> ScheduledUserWorkouts { get; set; }
    }
}