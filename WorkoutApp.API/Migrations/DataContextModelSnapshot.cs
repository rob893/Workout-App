﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutApp.API.Data;

namespace WorkoutApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.EquipmentExercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.HasKey("ExerciseId", "EquipmentId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("EquipmentExercise");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("PrimaryMuscleId")
                        .HasColumnType("int");

                    b.Property<int?>("SecondaryMuscleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryMuscleId");

                    b.HasIndex("SecondaryMuscleId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ExerciseCategorys");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseCategoryExercise", b =>
                {
                    b.Property<int>("ExerciseCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.HasKey("ExerciseCategoryId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("ExerciseCategoryExercise");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("Repetitions")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduledWorkoutId")
                        .HasColumnType("int");

                    b.Property<int>("Sets")
                        .HasColumnType("int");

                    b.Property<int>("TimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int?>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("ScheduledWorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ExerciseGroups");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseGroupCompletionRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CompletedSets")
                        .HasColumnType("int");

                    b.Property<int?>("CompletionTimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseGroupId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("ScheduledWorkoutId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("TargetCompletionTimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int?>("TargetSets")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseGroupId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("ScheduledWorkoutId");

                    b.HasIndex("UserId");

                    b.ToTable("ExerciseGroupCompletionRecords");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseSetCompletionRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CompletedReps")
                        .HasColumnType("int");

                    b.Property<float?>("CompletedWeightInPounds")
                        .HasColumnType("float");

                    b.Property<int?>("CompletionTimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("ScheduledWorkoutId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("TargetCompletionTimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int?>("TargetReps")
                        .HasColumnType("int");

                    b.Property<float?>("TargetWeightInPounds")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("ScheduledWorkoutId");

                    b.HasIndex("UserId");

                    b.ToTable("ExerciseSetCompletionRecords");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseStep", b =>
                {
                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseStepNumber")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ExerciseId", "ExerciseStepNumber");

                    b.ToTable("ExerciseStep");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Muscle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Muscles");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ScheduledWorkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ScheduledByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduledDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("StartedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduledByUserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ScheduledWorkouts");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ScheduledWorkoutUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduledWorkoutId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ScheduledWorkoutId");

                    b.HasIndex("ScheduledWorkoutId");

                    b.ToTable("ScheduledWorkoutUser");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.UserFavoriteExercise", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("UserFavoriteExercise");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Label")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Shareable")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("WorkoutCopiedFromId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("WorkoutCopiedFromId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.WorkoutCompletionRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CompletionTimeInSeconds")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("ScheduledWorkoutId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("TargetCompletionTimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduledWorkoutId");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutCompletionRecords");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.WorkoutInvitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Accepted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Declined")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("InviteeId")
                        .HasColumnType("int");

                    b.Property<int>("InviterId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RespondedAtDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ScheduledWorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InviteeId");

                    b.HasIndex("InviterId");

                    b.HasIndex("ScheduledWorkoutId");

                    b.ToTable("WorkoutInvitations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.EquipmentExercise", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Equipment", "Equipment")
                        .WithMany("Exercises")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany("Equipment")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany("ExerciseCategorys")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseGroup", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.ScheduledWorkout", "ScheduledWorkout")
                        .WithMany("AdHocExercises")
                        .HasForeignKey("ScheduledWorkoutId");

                    b.HasOne("WorkoutApp.API.Models.Workout", "Workout")
                        .WithMany("ExerciseGroups")
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseGroupCompletionRecord", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.ExerciseGroup", "ExerciseGroup")
                        .WithMany()
                        .HasForeignKey("ExerciseGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.ScheduledWorkout", null)
                        .WithMany("ExerciseGroupCompletionRecords")
                        .HasForeignKey("ScheduledWorkoutId");

                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany("ExerciseGroupCompletionRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseSetCompletionRecord", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.ScheduledWorkout", null)
                        .WithMany("ExerciseSetCompletionRecords")
                        .HasForeignKey("ScheduledWorkoutId");

                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany("ExerciseSetCompletionRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseStep", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany("ExerciseSteps")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ScheduledWorkout", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", "ScheduledByUser")
                        .WithMany("OwnedScheduledWorkouts")
                        .HasForeignKey("ScheduledByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ScheduledWorkoutUser", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.ScheduledWorkout", "ScheduledWorkout")
                        .WithMany("Attendees")
                        .HasForeignKey("ScheduledWorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany("ScheduledWorkouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.UserFavoriteExercise", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany("FavoritedBy")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany("FavoriteExercises")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.UserRole", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Workout", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", "CreatedByUser")
                        .WithMany("CreatedWorkouts")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.Workout", "WorkoutCopiedFrom")
                        .WithMany()
                        .HasForeignKey("WorkoutCopiedFromId");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.WorkoutCompletionRecord", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.ScheduledWorkout", null)
                        .WithMany("WorkoutCompletionRecords")
                        .HasForeignKey("ScheduledWorkoutId");

                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany("WorkoutCompletionRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.API.Models.WorkoutInvitation", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", "Invitee")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("InviteeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.User", "Inviter")
                        .WithMany("SentInvitations")
                        .HasForeignKey("InviterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.API.Models.ScheduledWorkout", "ScheduledWorkout")
                        .WithMany()
                        .HasForeignKey("ScheduledWorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}