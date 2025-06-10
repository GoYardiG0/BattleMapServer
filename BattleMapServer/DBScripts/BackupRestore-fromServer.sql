﻿
-- REPLACE YOUR DATABASE LOGIN AND PASSWORD IN THE SCRIPT BELOW 

Use master
Go

-- Create a login for the admin user
CREATE LOGIN [MapAdminLogin] WITH PASSWORD = 'AdminPassword';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [MapAdminLogin];
Go

Create Database BattleMapDB
go


select * from Users
select * from Monsters
select * from Characters
