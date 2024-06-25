USE [EmployeeDB];

SELECT
    p.first_name as Имя,
    LEFT(p.second_name, 1) + '.' AS Фамилия,
    LEFT(p.last_name, 1) + '.' AS Отчеество,
    s.name AS Статус,
    d.name AS Отдел,
    p.date_employ AS Приём, 
    p.date_uneploy AS Увольнение,
    ps.name AS Должность
FROM dbo.persons p
    JOIN dbo.status s ON p.status = s.id 
        JOIN dbo.deps d on p.id_dep = d.id
            join dbo.posts ps on p.id_post=ps.id;