CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Username` longtext NULL,
    `FristName` longtext NULL,
    `LastName` longtext NULL,
    `EmailAddress` longtext NULL,
    `PasswordHash` longblob NULL,
    `PasswordSalt` longblob NULL,
    `Created` datetime(6) NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190511234115_InitialCreate', '2.2.4-servicing-10062');

ALTER TABLE `Users` CHANGE `FristName` `FirstName` longtext NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190511235750_UpdatedUserModel', '2.2.4-servicing-10062');

