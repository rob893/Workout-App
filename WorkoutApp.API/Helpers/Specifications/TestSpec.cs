using System;
using System.Linq;
using System.Linq.Expressions;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Helpers.Specifications
{
    public class TestSpec : Specification<Exercise>
    {
        


        public TestSpec()
        {
            AddInclude(ex => ex.ExerciseCategorys);
            AddInclude(ex => ex.PrimaryMuscle);
            AddInclude(ex => ex.Equipment);
            AddInclude("Equipment.Equipment");
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            return exercise => exercise.Id ==1;
        }
    }
}