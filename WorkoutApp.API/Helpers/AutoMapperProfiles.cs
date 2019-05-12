using System.Linq;
using AutoMapper;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForReturnDto>();
            CreateMap<UserForRegisterDto, User>();
        }
    }
}