namespace WorkoutApp.API.Models.QueryParams
{
    public class WorkoutParams : PaginationParams
    {
        public int? UserId { get; set; }
    }
}