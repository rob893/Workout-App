using System;
using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 10 characters")]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        
        public DateTime Created { get; set; }


        public UserForRegisterDto()
        {
            Created = DateTime.Now;
        }
    }
}