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
            CreateUserMaps();
            CreateMuscleMaps();
            CreateWorkoutMaps();
            CreateScheduledWorkoutMaps();
            CreateEquipmentMaps();
            CreateExerciseCategoryMaps();
            CreateExerciseMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserForReturnDto>();
            CreateMap<User, UserForReturnDetailedDto>()
                .ForMember(dto => dto.Roles, opt =>
                    opt.MapFrom(u => u.UserRoles.Select(ur => ur.Role.Name)));
            CreateMap<UserForRegisterDto, User>();
            
            CreateMap<Role, RoleForReturnDto>();
        }

        private void CreateMuscleMaps()
        {
            CreateMap<Muscle, MuscleForReturnDto>();
            CreateMap<Muscle, MuscleForReturnDetailedDto>();
            CreateMap<MuscleForCreateDto, Muscle>();
            CreateMap<MuscleForUpdateDto, Muscle>();
        }

        private void CreateWorkoutMaps()
        {
            CreateMap<Workout, WorkoutForReturnDto>();
            CreateMap<Workout, WorkoutForReturnDetailedDto>();
            CreateMap<WorkoutForCreationDto, Workout>();
            CreateMap<WorkoutForUpdateDto, Workout>();
        }

        private void CreateScheduledWorkoutMaps()
        {
            CreateMap<ScheduledWorkout, ScheduledWorkoutForReturnDetailedDto>()
                .ForMember(dto => dto.Attendees, opt =>
                    opt.MapFrom(wo => wo.Attendees.Select(x => x.User)));
            CreateMap<ScheduledWorkoutForCreationDto, ScheduledWorkout>();
        }

        private void CreateEquipmentMaps()
        {
            CreateMap<Equipment, EquipmentForReturnDto>();
            CreateMap<Equipment, EquipmentForReturnDetailedDto>()
                .ForMember(dto => dto.Exercises, opts =>
                    opts.MapFrom(equipment => equipment.Exercises.Select(e => e.Exercise)));
            CreateMap<EquipmentForCreationDto, Equipment>();
        }

        private void CreateExerciseCategoryMaps()
        {
            CreateMap<ExerciseCategory, ExerciseCategoryForReturnDto>();
            CreateMap<ExerciseCategory, ExerciseCategoryForReturnDetailedDto>()
                .ForMember(dto => dto.Exercises, opts =>
                    opts.MapFrom(e => e.Exercises.Select(e => e.Exercise)));
            CreateMap<ExerciseCategoryForCreationDto, ExerciseCategory>();
        }

        private void CreateExerciseMaps()
        {
            CreateMap<Exercise, ExerciseForReturnDto>();
            CreateMap<Exercise, ExerciseForReturnDetailedDto>()
                .ForMember(dto => dto.Equipment, opts =>
                    opts.MapFrom(e => e.Equipment.Select(e => e.Equipment)))
                .ForMember(dto => dto.ExerciseCategorys, opts =>
                    opts.MapFrom(ec => ec.ExerciseCategorys.Select(ec => ec.ExerciseCategory)));
            CreateMap<ExerciseForCreationDto, Exercise>();
            
            CreateMap<ExerciseStep, ExerciseStepForReturnDto>();

            CreateMap<ExerciseGroup, ExerciseGroupForReturnDto>();
            CreateMap<ExerciseGroupForCreationDto, ExerciseGroup>();
        }
    }
}