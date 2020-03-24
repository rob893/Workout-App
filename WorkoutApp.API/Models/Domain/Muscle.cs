using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Domain
{
    public class Muscle : IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Exercise> PrimaryExercises { get; set; }
        public List<Exercise> SecondaryExercises { get; set; }
    }
}