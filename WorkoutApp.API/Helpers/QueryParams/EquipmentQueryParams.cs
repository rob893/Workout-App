using System.Collections.Generic;

namespace WorkoutApp.API.Helpers
{
    public class EquipmentQueryParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public List<int> ExerciseIds { get; set; } = new List<int>();
    }
}