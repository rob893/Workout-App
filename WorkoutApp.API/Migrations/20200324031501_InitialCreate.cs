using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Muscles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Source = table.Column<string>(maxLength: 255, nullable: false),
                    Token = table.Column<string>(maxLength: 255, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => new { x.UserId, x.Source });
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Label = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    CreatedOnDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Shareable = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    WorkoutCopiedFromId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workouts_Workouts_WorkoutCopiedFromId",
                        column: x => x.WorkoutCopiedFromId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PrimaryMuscleId = table.Column<int>(nullable: true),
                    SecondaryMuscleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Muscles_PrimaryMuscleId",
                        column: x => x.PrimaryMuscleId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exercises_Muscles_SecondaryMuscleId",
                        column: x => x.SecondaryMuscleId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledWorkouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScheduledByUserId = table.Column<int>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: false),
                    StartedDateTime = table.Column<DateTime>(nullable: true),
                    CompletedDateTime = table.Column<DateTime>(nullable: true),
                    ScheduledDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledWorkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledWorkouts_AspNetUsers_ScheduledByUserId",
                        column: x => x.ScheduledByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledWorkouts_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentExercise",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(nullable: false),
                    EquipmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentExercise", x => new { x.ExerciseId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_EquipmentExercise_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseCategoryExercise",
                columns: table => new
                {
                    ExerciseCategoryId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseCategoryExercise", x => new { x.ExerciseCategoryId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_ExerciseCategoryExercise_ExerciseCategories_ExerciseCategory~",
                        column: x => x.ExerciseCategoryId,
                        principalTable: "ExerciseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseCategoryExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseStep",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(nullable: false),
                    ExerciseStepNumber = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseStep", x => new { x.ExerciseId, x.ExerciseStepNumber });
                    table.ForeignKey(
                        name: "FK_ExerciseStep_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteExercise",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteExercise", x => new { x.UserId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteExercise_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExerciseId = table.Column<int>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: true),
                    ScheduledWorkoutId = table.Column<int>(nullable: true),
                    Sets = table.Column<int>(nullable: false),
                    Repetitions = table.Column<int>(nullable: false),
                    TimeInSeconds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseGroups_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseGroups_ScheduledWorkouts_ScheduledWorkoutId",
                        column: x => x.ScheduledWorkoutId,
                        principalTable: "ScheduledWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseGroups_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSetCompletionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: true),
                    CompletedAt = table.Column<DateTime>(nullable: true),
                    TargetCompletionTimeInSeconds = table.Column<int>(nullable: true),
                    CompletionTimeInSeconds = table.Column<int>(nullable: true),
                    TargetReps = table.Column<int>(nullable: true),
                    CompletedReps = table.Column<int>(nullable: true),
                    TargetWeightInPounds = table.Column<float>(nullable: true),
                    CompletedWeightInPounds = table.Column<float>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ScheduledWorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSetCompletionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSetCompletionRecords_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseSetCompletionRecords_ScheduledWorkouts_ScheduledWork~",
                        column: x => x.ScheduledWorkoutId,
                        principalTable: "ScheduledWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseSetCompletionRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledWorkoutUser",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ScheduledWorkoutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledWorkoutUser", x => new { x.UserId, x.ScheduledWorkoutId });
                    table.ForeignKey(
                        name: "FK_ScheduledWorkoutUser_ScheduledWorkouts_ScheduledWorkoutId",
                        column: x => x.ScheduledWorkoutId,
                        principalTable: "ScheduledWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledWorkoutUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutCompletionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: true),
                    CompletedAt = table.Column<DateTime>(nullable: true),
                    TargetCompletionTimeInSeconds = table.Column<int>(nullable: true),
                    CompletionTimeInSeconds = table.Column<int>(nullable: true),
                    WorkoutId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    ScheduledWorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutCompletionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutCompletionRecords_ScheduledWorkouts_ScheduledWorkoutId",
                        column: x => x.ScheduledWorkoutId,
                        principalTable: "ScheduledWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutCompletionRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutCompletionRecords_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutInvitations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InviterId = table.Column<int>(nullable: false),
                    InviteeId = table.Column<int>(nullable: false),
                    ScheduledWorkoutId = table.Column<int>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false),
                    RespondedAtDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutInvitations_AspNetUsers_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutInvitations_AspNetUsers_InviterId",
                        column: x => x.InviterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutInvitations_ScheduledWorkouts_ScheduledWorkoutId",
                        column: x => x.ScheduledWorkoutId,
                        principalTable: "ScheduledWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseGroupCompletionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    ExerciseGroupId = table.Column<int>(nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: true),
                    CompletedAt = table.Column<DateTime>(nullable: true),
                    TargetCompletionTimeInSeconds = table.Column<int>(nullable: true),
                    CompletionTimeInSeconds = table.Column<int>(nullable: true),
                    TargetSets = table.Column<int>(nullable: true),
                    CompletedSets = table.Column<int>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ScheduledWorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseGroupCompletionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseGroupCompletionRecords_ExerciseGroups_ExerciseGroupId",
                        column: x => x.ExerciseGroupId,
                        principalTable: "ExerciseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseGroupCompletionRecords_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseGroupCompletionRecords_ScheduledWorkouts_ScheduledWo~",
                        column: x => x.ScheduledWorkoutId,
                        principalTable: "ScheduledWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseGroupCompletionRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentExercise_EquipmentId",
                table: "EquipmentExercise",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseCategoryExercise_ExerciseId",
                table: "ExerciseCategoryExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroupCompletionRecords_ExerciseGroupId",
                table: "ExerciseGroupCompletionRecords",
                column: "ExerciseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroupCompletionRecords_ExerciseId",
                table: "ExerciseGroupCompletionRecords",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroupCompletionRecords_ScheduledWorkoutId",
                table: "ExerciseGroupCompletionRecords",
                column: "ScheduledWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroupCompletionRecords_UserId",
                table: "ExerciseGroupCompletionRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroups_ExerciseId",
                table: "ExerciseGroups",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroups_ScheduledWorkoutId",
                table: "ExerciseGroups",
                column: "ScheduledWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroups_WorkoutId",
                table: "ExerciseGroups",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_PrimaryMuscleId",
                table: "Exercises",
                column: "PrimaryMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_SecondaryMuscleId",
                table: "Exercises",
                column: "SecondaryMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSetCompletionRecords_ExerciseId",
                table: "ExerciseSetCompletionRecords",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSetCompletionRecords_ScheduledWorkoutId",
                table: "ExerciseSetCompletionRecords",
                column: "ScheduledWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSetCompletionRecords_UserId",
                table: "ExerciseSetCompletionRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkouts_ScheduledByUserId",
                table: "ScheduledWorkouts",
                column: "ScheduledByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkouts_WorkoutId",
                table: "ScheduledWorkouts",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkoutUser_ScheduledWorkoutId",
                table: "ScheduledWorkoutUser",
                column: "ScheduledWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteExercise_ExerciseId",
                table: "UserFavoriteExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutCompletionRecords_ScheduledWorkoutId",
                table: "WorkoutCompletionRecords",
                column: "ScheduledWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutCompletionRecords_UserId",
                table: "WorkoutCompletionRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutCompletionRecords_WorkoutId",
                table: "WorkoutCompletionRecords",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutInvitations_InviteeId",
                table: "WorkoutInvitations",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutInvitations_InviterId",
                table: "WorkoutInvitations",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutInvitations_ScheduledWorkoutId",
                table: "WorkoutInvitations",
                column: "ScheduledWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_CreatedByUserId",
                table: "Workouts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_WorkoutCopiedFromId",
                table: "Workouts",
                column: "WorkoutCopiedFromId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EquipmentExercise");

            migrationBuilder.DropTable(
                name: "ExerciseCategoryExercise");

            migrationBuilder.DropTable(
                name: "ExerciseGroupCompletionRecords");

            migrationBuilder.DropTable(
                name: "ExerciseSetCompletionRecords");

            migrationBuilder.DropTable(
                name: "ExerciseStep");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ScheduledWorkoutUser");

            migrationBuilder.DropTable(
                name: "UserFavoriteExercise");

            migrationBuilder.DropTable(
                name: "WorkoutCompletionRecords");

            migrationBuilder.DropTable(
                name: "WorkoutInvitations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "ExerciseCategories");

            migrationBuilder.DropTable(
                name: "ExerciseGroups");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "ScheduledWorkouts");

            migrationBuilder.DropTable(
                name: "Muscles");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
