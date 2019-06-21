CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `AspNetRoles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(256) NULL,
    `NormalizedName` varchar(256) NULL,
    `ConcurrencyStamp` longtext NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserName` varchar(256) NULL,
    `NormalizedUserName` varchar(256) NULL,
    `Email` varchar(256) NULL,
    `NormalizedEmail` varchar(256) NULL,
    `EmailConfirmed` bit NOT NULL,
    `PasswordHash` longtext NULL,
    `SecurityStamp` longtext NULL,
    `ConcurrencyStamp` longtext NULL,
    `PhoneNumber` longtext NULL,
    `PhoneNumberConfirmed` bit NOT NULL,
    `TwoFactorEnabled` bit NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` bit NOT NULL,
    `AccessFailedCount` int NOT NULL,
    `FirstName` longtext NULL,
    `LastName` longtext NULL,
    `Created` datetime(6) NOT NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
);

CREATE TABLE `Equipment` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    CONSTRAINT `PK_Equipment` PRIMARY KEY (`Id`)
);

CREATE TABLE `ExerciseCategorys` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    CONSTRAINT `PK_ExerciseCategorys` PRIMARY KEY (`Id`)
);

CREATE TABLE `Muscles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    CONSTRAINT `PK_Muscles` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` int NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) NOT NULL,
    `ProviderKey` varchar(255) NOT NULL,
    `ProviderDisplayName` longtext NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` int NOT NULL,
    `RoleId` int NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` int NOT NULL,
    `LoginProvider` varchar(255) NOT NULL,
    `Name` varchar(255) NOT NULL,
    `Value` longtext NULL,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Workouts` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Label` longtext NULL,
    `Color` longtext NULL,
    `CreatedByUserId` int NOT NULL,
    `CreatedOnDate` datetime(6) NOT NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `Shareable` bit NOT NULL,
    `IsDeleted` bit NOT NULL,
    `WorkoutCopiedFromId` int NULL,
    CONSTRAINT `PK_Workouts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Workouts_AspNetUsers_CreatedByUserId` FOREIGN KEY (`CreatedByUserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Workouts_Workouts_WorkoutCopiedFromId` FOREIGN KEY (`WorkoutCopiedFromId`) REFERENCES `Workouts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Exercises` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `PrimaryMuscleId` int NULL,
    `SecondaryMuscleId` int NULL,
    CONSTRAINT `PK_Exercises` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Exercises_Muscles_PrimaryMuscleId` FOREIGN KEY (`PrimaryMuscleId`) REFERENCES `Muscles` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Exercises_Muscles_SecondaryMuscleId` FOREIGN KEY (`SecondaryMuscleId`) REFERENCES `Muscles` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `ScheduledUserWorkouts` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `WorkoutId` int NOT NULL,
    `StartedDateTime` datetime(6) NULL,
    `CompletedDateTime` datetime(6) NULL,
    `ScheduledDateTime` datetime(6) NOT NULL,
    CONSTRAINT `PK_ScheduledUserWorkouts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ScheduledUserWorkouts_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ScheduledUserWorkouts_Workouts_WorkoutId` FOREIGN KEY (`WorkoutId`) REFERENCES `Workouts` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `EquipmentExercise` (
    `ExerciseId` int NOT NULL,
    `EquipmentId` int NOT NULL,
    CONSTRAINT `PK_EquipmentExercise` PRIMARY KEY (`ExerciseId`, `EquipmentId`),
    CONSTRAINT `FK_EquipmentExercise_Equipment_EquipmentId` FOREIGN KEY (`EquipmentId`) REFERENCES `Equipment` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EquipmentExercise_Exercises_ExerciseId` FOREIGN KEY (`ExerciseId`) REFERENCES `Exercises` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ExerciseCategoryExercise` (
    `ExerciseCategoryId` int NOT NULL,
    `ExerciseId` int NOT NULL,
    CONSTRAINT `PK_ExerciseCategoryExercise` PRIMARY KEY (`ExerciseCategoryId`, `ExerciseId`),
    CONSTRAINT `FK_ExerciseCategoryExercise_ExerciseCategorys_ExerciseCategoryId` FOREIGN KEY (`ExerciseCategoryId`) REFERENCES `ExerciseCategorys` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ExerciseCategoryExercise_Exercises_ExerciseId` FOREIGN KEY (`ExerciseId`) REFERENCES `Exercises` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ExerciseStep` (
    `ExerciseId` int NOT NULL,
    `ExerciseStepNumber` int NOT NULL,
    `Description` longtext NULL,
    CONSTRAINT `PK_ExerciseStep` PRIMARY KEY (`ExerciseId`, `ExerciseStepNumber`),
    CONSTRAINT `FK_ExerciseStep_Exercises_ExerciseId` FOREIGN KEY (`ExerciseId`) REFERENCES `Exercises` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ExerciseGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ExerciseId` int NOT NULL,
    `WorkoutId` int NULL,
    `ScheduledUserWorkoutId` int NULL,
    `Sets` int NOT NULL,
    `Repetitions` int NOT NULL,
    CONSTRAINT `PK_ExerciseGroups` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ExerciseGroups_Exercises_ExerciseId` FOREIGN KEY (`ExerciseId`) REFERENCES `Exercises` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ExerciseGroups_ScheduledUserWorkouts_ScheduledUserWorkoutId` FOREIGN KEY (`ScheduledUserWorkoutId`) REFERENCES `ScheduledUserWorkouts` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_ExerciseGroups_Workouts_WorkoutId` FOREIGN KEY (`WorkoutId`) REFERENCES `Workouts` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_EquipmentExercise_EquipmentId` ON `EquipmentExercise` (`EquipmentId`);

CREATE INDEX `IX_ExerciseCategoryExercise_ExerciseId` ON `ExerciseCategoryExercise` (`ExerciseId`);

CREATE INDEX `IX_ExerciseGroups_ExerciseId` ON `ExerciseGroups` (`ExerciseId`);

CREATE INDEX `IX_ExerciseGroups_ScheduledUserWorkoutId` ON `ExerciseGroups` (`ScheduledUserWorkoutId`);

CREATE INDEX `IX_ExerciseGroups_WorkoutId` ON `ExerciseGroups` (`WorkoutId`);

CREATE INDEX `IX_Exercises_PrimaryMuscleId` ON `Exercises` (`PrimaryMuscleId`);

CREATE INDEX `IX_Exercises_SecondaryMuscleId` ON `Exercises` (`SecondaryMuscleId`);

CREATE INDEX `IX_ScheduledUserWorkouts_UserId` ON `ScheduledUserWorkouts` (`UserId`);

CREATE INDEX `IX_ScheduledUserWorkouts_WorkoutId` ON `ScheduledUserWorkouts` (`WorkoutId`);

CREATE INDEX `IX_Workouts_CreatedByUserId` ON `Workouts` (`CreatedByUserId`);

CREATE INDEX `IX_Workouts_WorkoutCopiedFromId` ON `Workouts` (`WorkoutCopiedFromId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190527184505_IdentityInitial', '2.2.4-servicing-10062');

CREATE TABLE `WorkoutInvitations` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `InviterId` int NOT NULL,
    `InviteeId` int NOT NULL,
    `ScheduledUserWorkoutId` int NOT NULL,
    `Accepted` bit NOT NULL,
    `Declined` bit NOT NULL,
    `RespondedAtDateTime` datetime(6) NULL,
    CONSTRAINT `PK_WorkoutInvitations` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_WorkoutInvitations_AspNetUsers_InviteeId` FOREIGN KEY (`InviteeId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_WorkoutInvitations_AspNetUsers_InviterId` FOREIGN KEY (`InviterId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_WorkoutInvitations_ScheduledUserWorkouts_ScheduledUserWorkou~` FOREIGN KEY (`ScheduledUserWorkoutId`) REFERENCES `ScheduledUserWorkouts` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_WorkoutInvitations_InviteeId` ON `WorkoutInvitations` (`InviteeId`);

CREATE INDEX `IX_WorkoutInvitations_InviterId` ON `WorkoutInvitations` (`InviterId`);

CREATE INDEX `IX_WorkoutInvitations_ScheduledUserWorkoutId` ON `WorkoutInvitations` (`ScheduledUserWorkoutId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190617201601_AddedWorkoutInvitations', '2.2.4-servicing-10062');

CREATE TABLE `ExtraSchUsrWoAttendee` (
    `ScheduledUserWorkoutId` int NOT NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_ExtraSchUsrWoAttendee` PRIMARY KEY (`UserId`, `ScheduledUserWorkoutId`),
    CONSTRAINT `FK_ExtraSchUsrWoAttendee_ScheduledUserWorkouts_ScheduledUserWor~` FOREIGN KEY (`ScheduledUserWorkoutId`) REFERENCES `ScheduledUserWorkouts` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ExtraSchUsrWoAttendee_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_ExtraSchUsrWoAttendee_ScheduledUserWorkoutId` ON `ExtraSchUsrWoAttendee` (`ScheduledUserWorkoutId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190618021028_AddedExtraWoAttendees', '2.2.4-servicing-10062');