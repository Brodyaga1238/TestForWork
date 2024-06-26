USE [EmployeeDB];
GO

IF OBJECT_ID('dbo.GetStatData', 'P') IS NULL
    BEGIN
        EXEC('
        CREATE PROCEDURE dbo.GetStatData
        AS
        BEGIN
            SELECT s.name AS Статус
            FROM dbo.status s;
        END
    ');
    END;
GO
