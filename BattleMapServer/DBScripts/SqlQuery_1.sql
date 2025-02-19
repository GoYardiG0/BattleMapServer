use master
Go
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'BattleMapDB')
BEGIN
    DROP DATABASE BattleMapDB;
END
Go
Create Database BattleMapDB

Go

Use BattleMapDB

Go

Create Table Users
(
UserId int Primary Key Identity,

UserName nvarchar(50) Unique Not Null,

UserEmail nvarchar(50) Unique Not Null,

UserPassword nvarchar(50) Not Null,
)
go

Create Table Monsters
(
MonsterId int Primary Key Identity,

UserId int foreign Key References Users(UserId) Not Null,

MonsterName nvarchar(50) Unique Not Null,

MonsterPic nvarchar(100),

AC int not null,

HP int not null,

[str] int not null,

dex int not null,

con int not null,

[int] int not null,

wis int not null,

cha int not null,

cr int not null,

passive_desc nvarchar(1000),

action_desc nvarchar(1000),

special_action_desc  nvarchar(1000)

)
go
Create Table Characters
(
CharacterId int Primary Key Identity,

UserId int foreign Key References Users(UserId) ,

CharacterName nvarchar(50) Unique Not Null,

CharacterPic nvarchar(100) not null,

AC int not null,

HP int not null,

[str] int not null,

dex int not null,

con int not null,

[int] int not null,

wis int not null,

cha int not null,

[level] int not null,

passive_desc nvarchar(1000) not null,

action_desc nvarchar(1000) not null,

special_action_desc  nvarchar(1000)

)

create table Friends
(
UserId int foreign Key References Users(UserId),
FriendId int
)
go

insert into Users (UserName,UserEmail,UserPassword) Values ('admin', 'yarden.golan07@gmail.com', 'admin')
insert into Users (UserName,UserEmail,UserPassword) Values ('test', 'test@gmail.com', 'test')
insert into Users (UserName,UserEmail,UserPassword) Values ('tes123t', 'te23st@gmail.com', 'te23st')
insert into Users (UserName,UserEmail,UserPassword) Values ('te232323st', 'te232323st@gmail.com', 'te23232323st')
go

-- Create a login for the admin user
CREATE LOGIN [MapAdminLogin] WITH PASSWORD = 'AdminPassword';
Go

-- Create a user in the TasksManagementDB database for the login
CREATE USER [AdminUser] FOR LOGIN [MapAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go
--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [MapAdminLogin];
Go



select * from Users
select * from Monsters
select * from Characters
select * from Friends

-- scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=BattleMapDB;User ID=[MapAdminLogin];Password=AdminPassword;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BattleMapDbContext -DataAnnotations –force