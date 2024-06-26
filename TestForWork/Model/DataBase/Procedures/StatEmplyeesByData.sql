USE [EmployeeDB];

IF OBJECT_ID('dbo.StatEmplyeesByData', 'P') IS NULL
    BEGIN
        EXEC('
        CREATE PROCEDURE dbo.StatEmplyeesByData
        AS
        BEGIN
            SELECT
                s.name AS Статус,
                p.date_employ AS Дата_Найма,
                p.date_uneploy AS Дата_Увольнения
            FROM dbo.persons p
            JOIN dbo.status s ON p.status = s.id;
        END
    ');
    END;
