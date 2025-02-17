use master
go
IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE NAME = 'DBWEBAPI')
CREATE DATABASE DDBWEBAPI

GO 

USE DBWEBAPI

GO

if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'USUARIOS')
create table USUARIOS(
Id VARCHAR(60) primary key IDENTITY,
UserName varchar(60),
Password varchar(60),
Role VARCHAR(60)
)

go

if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'RefreshTokens')
CREATE TABLE RefreshTokens (
    Id INT PRIMARY KEY IDENTITY,
    UserId VARCHAR(60) NOT NULL,
    Token NVARCHAR(200) NOT NULL,
    ExpirationDate DATETIME NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_RefreshTokens_Users FOREIGN KEY (UserId) REFERENCES Usuarios(Id)
);
