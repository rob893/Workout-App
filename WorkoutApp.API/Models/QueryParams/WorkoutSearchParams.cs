namespace WorkoutApp.API.Models.QueryParams
{
    public class WorkoutSearchParams : CursorPaginationParams
    {
        public int? UserId { get; set; }
    }
}