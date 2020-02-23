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
                .ForMember(dto => dto.ExerciseSteps, opt =>
                    opt.MapFrom(ex => ex.ExerciseSteps.Select(es => new ExerciseStepForReturnDto
                    {
                        ExerciseStepNumber = es.ExerciseStepNumber,
                        Description = es.Description
                    }).ToList()))
                .ForMember(dto => dto.Equipment, opt =>
                    opt.MapFrom(ex => ex.Equipment.Select(exEq => new EquipmentForReturnDto
                    {
                        Id = exEq.EquipmentId,
                        Name = exEq.Equipment.Name
                    }).ToList()))
                .ForMember(dto => dto.ExerciseCategorys, opt =>
                    opt.MapFrom(ex => ex.ExerciseCategorys.Select(ece => new ExerciseCategoryForReturnDto
                    {
                        Id = ece.ExerciseCategoryId,
                        Name = ece.ExerciseCategory.Name
                    }).ToList()));
            CreateMap<ExerciseStep, ExerciseStepForReturnDto>();
            CreateMap<ExerciseCategory, ExerciseCategoryForReturnDto>();
            CreateMap<ExerciseGroupForCreationDto, ExerciseGroup>();

            CreateMap<EquipmentForCreationDto, Equipment>();
            // CreateMap<EquipmentExercise, EquipmentForReturnDto>()
            //     .ForMember(dto => dto.Id, opts =>
            //         opts.MapFrom(exEq => exEq.EquipmentId))
            //     .ForMember(dto => dto.Name, opts => 
            //         opts.MapFrom(exEq => exEq.Equipment.Name));
            CreateMap<Equipment, EquipmentForReturnDto>().ReverseMap();
        }
    }
}