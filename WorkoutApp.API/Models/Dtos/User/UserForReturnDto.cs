using System.Collections.Generic;
using System;

namespace WorkoutApp.API.Models.Dtos
{
    public class UserForReturnDto : DtoWithUrls
    {
        public int Id { get; set; }
        public string Url { get => $"{baseUrl}/users/{Id}"; }
        public string ScheduledWorkoutsUrl { get => $"{Url}/scheduledWorkouts"; }
        public string FavoriteExercisesUrl { get => $"{Url}/favorites/exercises";}
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }

        public UserForReturnDto(string baseUrl) : base(baseUrl) {}
    }

    public class UserForReturnDetailedDto : DtoWithUrls
    {
        public int Id { get; set; }
        public string Url { get => $"{baseUrl}/users/{Id}"; }
        public string ScheduledWorkoutsUrl { get => $"{Url}/scheduledWorkouts"; }
        public string FavoriteExercisesUrl { get => $"{Url}/favorites/exercises";}
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserForReturnDetailedDto(string baseUrl) : base(baseUrl) {}
    }
}