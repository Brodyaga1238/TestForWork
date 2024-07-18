Программа-клиент для просмотра данных сотрудников

Этот проект представляет собой программу-клиент для просмотра и анализа данных сотрудников, разработанную с использованием архитектуры MVP (Model-View-Presenter).
Содержание

    Описание проекта
    Функциональные возможности
    Структура проекта
    Требования
    Настройка
    Запуск
    Использование
    Лицензия

Описание проекта

Данное приложение разработано для просмотра списков сотрудников и получения статистической информации по ним. Подключение к базе данных настраивается через конфигурационный файл.
Функциональные возможности

    Просмотр списка сотрудников:
        Отображение ФИО (в формате Фамилия И. О.), статуса, отдела, должности, даты приема и увольнения.

    Получение статистики:
        Количество сотрудников выбранного статуса.
        Количество сотрудников, принятых или уволенных за заданный период с разбивкой по дням.
        Выбор статуса и периода, переключение между принятыми и уволенными сотрудниками.

    Хранимые процедуры:
        Обращение к данным через хранимые процедуры в MSSQL.
        Проверка корректности ввода данных.

Структура проекта

    Model: Логика и структуры данных.
    View: Интерфейс пользователя на Windows Forms.
    Presenter: Посредник между Model и View, управляющий логикой отображения.

Требования

    .NET Framework
    MSSQL сервер

Настройка

    Клонируйте репозиторий:

    sh

    git clone https://github.com/Brodyaga1238/TestForWork.git

    Настройте строку подключения к базе данных в конфигурационном файле app.config или введите ее при запуске программы.

Запуск

    Откройте решение в Visual Studio.
    Постройте и запустите проект.

Использование

    Откройте приложение.
    Используйте интерфейс для просмотра и фильтрации списка сотрудников, а также для получения статистической информации.