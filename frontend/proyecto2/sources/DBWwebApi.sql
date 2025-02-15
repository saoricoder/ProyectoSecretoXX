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
usuario varchar(60),
password varchar(60),
token  VARCHAR(MAX),
role VARCHAR(60)
)

go

if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'REFRESHTOKENS')
CREATE TABLE REFRESHTOKENS (
    Id INT PRIMARY KEY IDENTITY,
    UserId VARCHAR(60) NOT NULL,
    Token NVARCHAR(200) NOT NULL,
    ExpirationDate DATETIME NOT NULL,
    IsRevoked BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    CONSTRAINT FK_RefreshTokens_Users FOREIGN KEY (UserId) REFERENCES Usuarios(Id)
);
