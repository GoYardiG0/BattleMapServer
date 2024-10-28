
﻿use master
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

UserId int foreign Key References Users(UserId) ,

MonsterName nvarchar(50) Unique Not Null,

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
CharaterId int Primary Key Identity,

UserId int foreign Key References Users(UserId) ,

CharaterName nvarchar(50) Unique Not Null,

AC int not null,

HP int not null,

[str] int not null,

dex int not null,

con int not null,

[int] int not null,

wis int not null,

cha int not null,

[level] int not null,

passive_desc nvarchar(1000),

action_desc nvarchar(1000),

special_action_desc  nvarchar(1000)

)
go
create table friends
(
UserId int foreign Key References Users(UserId),
FriendId int
)
go
insert into users Values('admin', 'yarden.golan07@gmail.com', 'admin')
go

CREATE LOGIN [MapAdminLogin] WITH PASSWORD = 'AdminPassword';
Go

CREATE USER [AdminUser] FOR LOGIN [MapAdminLogin];
Go

ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go


select * from Users
select * from Monsters
select * from Characters