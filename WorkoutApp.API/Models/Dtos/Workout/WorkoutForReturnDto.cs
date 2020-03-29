using System;

namespace WorkoutApp.API.Models.Dtos
{
    public class WorkoutForReturnDto : BaseDtoForReturn
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public string ExerciseGroupsUrl { get => $"{Url}/exerciseGroups"; }
        public int CreatedByUserId { get; set; }
        public string CreatedByUserUrl { get => $"{baseUrl}/users/{CreatedByUserId}"; }
        public DateTimeOffset CreatedOnDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        public bool Shareable { get; set; }
        public bool IsDeleted { get; set; }


        public WorkoutForReturnDto(string baseUrl) : base(baseUrl, "workouts") { }
    }
}