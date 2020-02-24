namespace WorkoutApp.API.Models.QueryParams
{
    public class WorkoutSearchParams : PaginationParams
    {
        public int? UserId { get; set; }
    }
}