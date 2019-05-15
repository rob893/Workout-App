using System.Collections.Generic;

namespace WorkoutApp.API.Helpers.QueryParams
{
    public class ExerciseParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public List<int> ExerciseCategoryId { get; set; } = new List<int>();
        public List<int> EquipmentId { get; set; } = new List<int>();
    }
}