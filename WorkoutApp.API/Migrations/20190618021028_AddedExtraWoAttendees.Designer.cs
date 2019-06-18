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
    [Migration("20190618021028_AddedExtraWoAttendees")]
    partial class AddedExtraWoAttendees
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

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

                    b.Property<int?>("ScheduledUserWorkoutId");

                    b.Property<int>("Sets");

                    b.Property<int?>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("ScheduledUserWorkoutId");

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

            modelBuilder.Entity("WorkoutApp.API.Models.ExtraSchUsrWoAttendee", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ScheduledUserWorkoutId");

                    b.HasKey("UserId", "ScheduledUserWorkoutId");

                    b.HasIndex("ScheduledUserWorkoutId");

                    b.ToTable("ExtraSchUsrWoAttendee");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Muscle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Muscles");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
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

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
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

            modelBuilder.Entity("WorkoutApp.API.Models.WorkoutInvitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<bool>("Declined");

                    b.Property<int>("InviteeId");

                    b.Property<int>("InviterId");

                    b.Property<DateTime?>("RespondedAtDateTime");

                    b.Property<int>("ScheduledUserWorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("InviteeId");

                    b.HasIndex("InviterId");

                    b.HasIndex("ScheduledUserWorkoutId");

                    b.ToTable("WorkoutInvitations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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

                    b.HasOne("WorkoutApp.API.Models.ScheduledUserWorkout", "ScheduledUserWorkout")
                        .WithMany("AdHocExercises")
                        .HasForeignKey("ScheduledUserWorkoutId");

                    b.HasOne("WorkoutApp.API.Models.Workout", "Workout")
                        .WithMany("ExerciseGroups")
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExerciseStep", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Exercise", "Exercise")
                        .WithMany("ExerciseSteps")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutApp.API.Models.ExtraSchUsrWoAttendee", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.ScheduledUserWorkout", "ScheduledUserWorkout")
                        .WithMany("ExtraSchUsrWoAttendees")
                        .HasForeignKey("ScheduledUserWorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
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

            modelBuilder.Entity("WorkoutApp.API.Models.UserRole", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
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

            modelBuilder.Entity("WorkoutApp.API.Models.WorkoutInvitation", b =>
                {
                    b.HasOne("WorkoutApp.API.Models.User", "Invitee")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("InviteeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.User", "Inviter")
                        .WithMany("SentInvitations")
                        .HasForeignKey("InviterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutApp.API.Models.ScheduledUserWorkout", "ScheduledUserWorkout")
                        .WithMany()
                        .HasForeignKey("ScheduledUserWorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
