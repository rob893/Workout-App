﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutApp.API.Data;

namespace WorkoutApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190524195854_UpdatedTime")]
    partial class UpdatedTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WorkoutApp.API.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.EquipmentExercise", b =>
                {
                    b.Property<int>("ExerciseId");

                    b.Property<int>("EquipmentId");

                    b.HasKey("ExerciseId", "EquipmentId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("EquipmentExercise");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("PrimaryMuscleId");

                    b.Property<int?>("SecondaryMuscleId");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryMuscleId");

                    b.HasIndex("SecondaryMuscleId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ExerciseCategorys");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseCategoryExercise", b =>
                {
                    b.Property<int>("ExerciseCategoryId");

                    b.Property<int>("ExerciseId");

                    b.HasKey("ExerciseCategoryId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("ExerciseCategoryExercise");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExerciseId");

                    b.Property<int>("Repetitions");

                    b.Property<int>("Sets");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ExerciseGroups");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseStep", b =>
                {
                    b.Property<int>("ExerciseId");

                    b.Property<int>("ExerciseStepNumber");

                    b.Property<string>("Description");

                    b.HasKey("ExerciseId", "ExerciseStepNumber");

                    b.ToTable("ExerciseStep");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Muscle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Muscles");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ScheduledUserWorkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CompletedDateTime");

                    b.Property<DateTime>("ScheduledDateTime");

                    b.Property<DateTime?>("StartedDateTime");

                    b.Property<int>("UserId");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ScheduledUserWorkouts");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<int>("CreatedByUserId");

                    b.Property<DateTime>("CreatedOnDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Label");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<bool>("Shareable");

                    b.Property<int?>("WorkoutCopiedFromId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("WorkoutCopiedFromId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.EquipmentExercise", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Equipment", "Equipment")
                        .WithMany("Exercises")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany("Equipment")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Exercise", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Muscle", "PrimaryMuscle")
                        .WithMany()
                        .HasForeignKey("PrimaryMuscleId");

                    b.HasOne("WorkoutApp.API.Models.Muscle", "SecondaryMuscle")
                        .WithMany()
                        .HasForeignKey("SecondaryMuscleId");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseCategoryExercise", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.ExerciseCategory", "ExerciseCategory")
                        .WithMany("Exercises")
                        .HasForeignKey("ExerciseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany("ExerciseCategorys")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseGroup", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.Workout", "Workout")
                        .WithMany("ExerciseGroups")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseStep", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany("ExerciseSteps")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ScheduledUserWorkout", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany("ScheduledUserWorkouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Workout", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", "CreatedByUser")
                        .WithMany("CreatedWorkouts")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.Workout", "WorkoutCopiedFrom")
                        .WithMany()
                        .HasForeignKey("WorkoutCopiedFromId");
                });
#pragma warning restore 612, 618
        }
    }
}
