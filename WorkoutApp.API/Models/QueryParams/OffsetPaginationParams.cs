namespace WorkoutApp.API.Models.QueryParams
{
    public class OffsetPaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 0;
    }
}