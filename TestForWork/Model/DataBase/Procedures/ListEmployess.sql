USE [EmployeeDB];


IF OBJECT_ID('dbo.GetEmployeeData', 'P') IS NULL
    BEGIN
        EXEC('CREATE PROCEDURE dbo.GetEmployeeData AS BEGIN SELECT
        p.first_name AS Имя,
        LEFT(p.second_name, 1) + ''.'' AS Фамилия,
        LEFT(p.last_name, 1) + ''.'' AS Отчество,
        s.name AS Статус,
        d.name AS Отдел,
        ps.name AS Должность,
        p.date_employ AS Приём,
        p.date_uneploy AS Увольнение
    FROM dbo.persons p
             JOIN dbo.status s ON p.status = s.id
             JOIN dbo.deps d ON p.id_dep = d.id
             JOIN dbo.posts ps ON p.id_post = ps.id;
    END')
    END;

