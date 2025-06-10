USE master;
GO

-- Declare the database name
DECLARE @DatabaseName NVARCHAR(255) = 'BattleMapDB';

-- Generate and execute the kill commands for all active connections
DECLARE @KillCommand NVARCHAR(MAX);

SET @KillCommand = (
    SELECT STRING_AGG('KILL ' + CAST(session_id AS NVARCHAR), '; ')
    FROM sys.dm_exec_sessions
    WHERE database_id = DB_ID(@DatabaseName)
);

IF @KillCommand IS NOT NULL
BEGIN
    EXEC sp_executesql @KillCommand;
    PRINT 'All connections to the database have been terminated.';
END
ELSE
BEGIN
    PRINT 'No active connections to the database.';
END
Go

IF EXISTS (SELECT * FROM sys.databases WHERE name = N'BattleMapDB')
BEGIN
    DROP DATABASE BattleMapDB;
END
Go
-- Create a login for the admin user
CREATE LOGIN [MapAdminLogin] WITH PASSWORD = 'AdminPassword';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [MapAdminLogin];
Go

CREATE Database BattleMapDB;
Go



Use master
Go


USE master;
ALTER DATABASE BattleMapDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE BattleMapDB FROM DISK = '\source\repos\BattleMapServer\BattleMapServer\wwwroot\..\DbScripts\backup.bak' WITH REPLACE, --להחליף את זה לנתיב של קובץ הגיבוי
    MOVE 'BattleMapDB' TO '\BattleMapDB.mdf',   --להחליף לנתיב שנמצא על המחשב שלך
    MOVE 'BattleMapDB_log' TO '\BattleMapDB_log.ldf';  
ALTER DATABASE BattleMapDB SET MULTI_USER;