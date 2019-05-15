using System;
using System.Collections.Generic;

namespace WorkoutApp.API.Helpers.QueryParams
{
    public class WorkoutParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int? UserId { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}