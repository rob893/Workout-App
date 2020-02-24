using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Data.Repositories
{
    public class WorkoutRepository : Repository<Workout, WorkoutSearchParams>
    {
        public WorkoutRepository(DataContext context) : base(context) { }

        protected override IQueryable<Workout> EntitySet => context.Workouts;

        protected override IQueryable<Workout> AddDetailedIncludes(IQueryable<Workout> query)
        {
            return query
                .Include(w => w.CreatedByUser)
                .Include(w => w.WorkoutCopiedFrom)
                .Include(w => w.ExerciseGroups).ThenInclude(eg => eg.Exercise);
        }

        protected override IQueryable<Workout> AddWhereClauses(IQueryable<Workout> query, WorkoutSearchParams searchParams)
        {
            if (searchParams.UserId != null)
            {
                query = query.Where(w => w.CreatedByUserId == searchParams.UserId.Value);
            }

            return query;
        }

        protected override void BeforeDelete(Workout entity)
        {
            if (entity.ExerciseGroups == null)
            {
                throw new ArgumentNullException("ExerciseGroups must be loaded before delete.");
            }

            context.ExerciseGroups.RemoveRange(entity.ExerciseGroups);
        }
    }
}