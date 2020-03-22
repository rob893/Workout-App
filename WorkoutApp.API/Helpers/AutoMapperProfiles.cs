using System.Linq;
using AutoMapper;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.Dtos;

namespace WorkoutApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        private readonly string baseUrl;


        public AutoMapperProfiles(string baseUrl)
        {
            this.baseUrl = baseUrl;

            CreateUserMaps();
            CreateMuscleMaps();
            CreateWorkoutMaps();
            CreateScheduledWorkoutMaps();
            CreateWorkoutInvitationMaps();
            CreateEquipmentMaps();
            CreateExerciseCategoryMaps();
            CreateExerciseMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserForReturnDto>().ConstructUsing(x => new UserForReturnDto(baseUrl));
            CreateMap<User, UserForReturnDetailedDto>().ConstructUsing(x => new UserForReturnDetailedDto(baseUrl))
                .ForMember(dto => dto.Roles, opt =>
                    opt.MapFrom(u => u.UserRoles.Select(ur => ur.Role.Name)));
            CreateMap<UserForRegisterDto, User>();
            
            CreateMap<Role, RoleForReturnDto>();
        }

        private void CreateMuscleMaps()
        {
            CreateMap<Muscle, MuscleForReturnDto>().ConstructUsing(x => new MuscleForReturnDto(baseUrl));
            CreateMap<Muscle, MuscleForReturnDetailedDto>().ConstructUsing(x => new MuscleForReturnDetailedDto(baseUrl));
            CreateMap<MuscleForCreateDto, Muscle>();
            CreateMap<MuscleForUpdateDto, Muscle>();
        }

        private void CreateWorkoutMaps()
        {
            CreateMap<Workout, WorkoutForReturnDto>().ConstructUsing(x => new WorkoutForReturnDto(baseUrl));
            CreateMap<Workout, WorkoutForReturnDetailedDto>().ConstructUsing(x => new WorkoutForReturnDetailedDto(baseUrl));
            CreateMap<WorkoutForCreationDto, Workout>();
            CreateMap<WorkoutForUpdateDto, Workout>();
        }

        private void CreateScheduledWorkoutMaps()
        {
            CreateMap<ScheduledWorkout, ScheduledWorkoutForReturnDto>().ConstructUsing(x => new ScheduledWorkoutForReturnDto(baseUrl));
            CreateMap<ScheduledWorkout, ScheduledWorkoutForReturnDetailedDto>().ConstructUsing(x => new ScheduledWorkoutForReturnDetailedDto(baseUrl))
                .ForMember(dto => dto.Attendees, opt =>
                    opt.MapFrom(wo => wo.Attendees.Select(x => x.User)));
            CreateMap<ScheduledWorkoutForCreationDto, ScheduledWorkout>();
        }

        private void CreateWorkoutInvitationMaps()
        {
            CreateMap<WorkoutInvitation, WorkoutInvitationForReturnDto>().ConstructUsing(x => new WorkoutInvitationForReturnDto(baseUrl));
            CreateMap<WorkoutInvitation, WorkoutInvitationForReturnDetailedDto>().ConstructUsing(x => new WorkoutInvitationForReturnDetailedDto(baseUrl));
            CreateMap<WorkoutInvitationForCreationDto, WorkoutInvitation>();
        }

        private void CreateEquipmentMaps()
        {
            CreateMap<Equipment, EquipmentForReturnDto>().ConstructUsing(x => new EquipmentForReturnDto(baseUrl));
            CreateMap<Equipment, EquipmentForReturnDetailedDto>().ConstructUsing(x => new EquipmentForReturnDetailedDto(baseUrl))
                .ForMember(dto => dto.Exercises, opts =>
                    opts.MapFrom(equipment => equipment.Exercises.Select(e => e.Exercise)));
            CreateMap<EquipmentForCreationDto, Equipment>();
        }

        private void CreateExerciseCategoryMaps()
        {
            CreateMap<ExerciseCategory, ExerciseCategoryForReturnDto>().ConstructUsing(x => new ExerciseCategoryForReturnDto(baseUrl));
            CreateMap<ExerciseCategory, ExerciseCategoryForReturnDetailedDto>().ConstructUsing(x => new ExerciseCategoryForReturnDetailedDto(baseUrl))
                .ForMember(dto => dto.Exercises, opts =>
                    opts.MapFrom(e => e.Exercises.Select(e => e.Exercise)));
            CreateMap<ExerciseCategoryForCreationDto, ExerciseCategory>();
        }

        private void CreateExerciseMaps()
        {
            CreateMap<Exercise, ExerciseForReturnDto>().ConstructUsing(x => new ExerciseForReturnDto(baseUrl));
            CreateMap<Exercise, ExerciseForReturnDetailedDto>().ConstructUsing(x => new ExerciseForReturnDetailedDto(baseUrl))
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