USE [EmployeeDB];

IF OBJECT_ID('dbo.Statuses', 'P') IS NULL
    BEGIN
        EXEC('
        CREATE PROCEDURE dbo.Statuses
        AS
        BEGIN
            SELECT s.name AS Статус
            FROM dbo.status s;
        END
    ');
    END;
