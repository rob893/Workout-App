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
            
            CreateMap<Workout, WorkoutForReturnDto>();
            CreateMap<WorkoutForCreationDto, Workout>();
            CreateMap<WorkoutForUpdateDto, Workout>();
            CreateMap<ScheduledWoForCreationDto, ScheduledUserWorkout>();
            CreateMap<ScheduledUserWorkout, ScheduledWoForReturnDto>()
                .ForMember(dto => dto.ExtraSchUsrWoAttendees, opt =>
                    opt.MapFrom(wo => wo.ExtraSchUsrWoAttendees.Select(attendee => attendee.User)));

            CreateMap<ExerciseGroup, ExerciseGroupForReturnDto>();

            CreateMap<ExerciseForCreationDto, Exercise>();
            CreateMap<Exercise, ExerciseForReturnDto>();
            CreateMap<Exercise, ExerciseForReturnDetailedDto>()
                .ForMember(dto => dto.ExerciseSteps, opt =>
                    opt.MapFrom(ex => ex.ExerciseSteps.Select(es => new ExerciseStepForReturnDto {
                        ExerciseStepNumber = es.ExerciseStepNumber,
                        Description = es.Description
                    }).ToList()))
                .ForMember(dto => dto.Equipment, opt => 
                    opt.MapFrom(ex => ex.Equipment.Select(exEq => new EquipmentForReturnDto {
                        Id = exEq.EquipmentId,
                        Name = exEq.Equipment.Name
                    }).ToList()))
                .ForMember(dto => dto.ExerciseCategorys, opt =>
                    opt.MapFrom(ex => ex.ExerciseCategorys.Select(ece => new ExerciseCategoryForReturnDto {
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