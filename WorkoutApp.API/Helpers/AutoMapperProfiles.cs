using System.Linq;
using AutoMapper;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.Dtos;

namespace WorkoutApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForReturnDto>();
            CreateMap<User, UserForReturnDetailedDto>()
                .ForMember(dto => dto.Roles, opt =>
                    opt.MapFrom(u => u.UserRoles.Select(ur => ur.Role.Name)));

            CreateMap<UserForRegisterDto, User>();
            CreateMap<Role, RoleForReturnDto>();

            CreateMap<Muscle, MuscleForReturnDto>();
            CreateMap<Muscle, MuscleForReturnDetailedDto>();
            CreateMap<MuscleForCreateDto, Muscle>();

            CreateMap<Workout, WorkoutForReturnDetailedDto>();
            CreateMap<Workout, WorkoutForReturnDto>();
            CreateMap<WorkoutForCreationDto, Workout>();
            CreateMap<WorkoutForUpdateDto, Workout>();
            CreateMap<ScheduledWoForCreationDto, ScheduledWorkout>();
            CreateMap<ScheduledWorkout, ScheduledWoForReturnDto>()
                .ForMember(dto => dto.Attendees, opt =>
                    opt.MapFrom(wo => wo.Attendees.Select(x => x.User)));

            CreateMap<ExerciseGroup, ExerciseGroupForReturnDto>();

            CreateMap<ExerciseForCreationDto, Exercise>();
            CreateMap<Exercise, ExerciseForReturnDto>();
            CreateMap<Exercise, ExerciseForReturnDetailedDto>()
                .ForMember(dto => dto.Equipment, opts => 
                    opts.MapFrom(e => e.Equipment.Select(e => e.Equipment)))
                .ForMember(dto => dto.ExerciseCategorys, opts => 
                    opts.MapFrom(ec => ec.ExerciseCategorys.Select(ec => ec.ExerciseCategory)));

            CreateMap<ExerciseStep, ExerciseStepForReturnDto>();

            CreateMap<ExerciseCategory, ExerciseCategoryForReturnDto>();
            CreateMap<ExerciseCategory, ExerciseCategoryForReturnDetailedDto>()
                .ForMember(dto => dto.Exercises, opts =>
                    opts.MapFrom(e => e.Exercises.Select(e => e.Exercise)));
            CreateMap<ExerciseCategoryForCreationDto, ExerciseCategory>();
            
            CreateMap<ExerciseGroupForCreationDto, ExerciseGroup>();

            CreateMap<EquipmentForCreationDto, Equipment>();
            CreateMap<Equipment, EquipmentForReturnDto>();
            CreateMap<Equipment, EquipmentForReturnDetailedDto>()
                .ForMember(dto => dto.Exercises, opts =>
                    opts.MapFrom(equipment => equipment.Exercises.Select(e => e.Exercise)));
        }
    }
}