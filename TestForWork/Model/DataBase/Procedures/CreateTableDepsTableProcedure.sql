USE [EmployeeDB];
GO

IF OBJECT_ID('dbo.CreateDepsTable', 'P') IS NULL
BEGIN
    EXEC('CREATE PROCEDURE dbo.CreateDepsTable
    AS
    BEGIN
        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = ''deps'' AND schema_id = SCHEMA_ID(''dbo''))
        BEGIN
            CREATE TABLE [dbo].[deps]
            (
                [id] [int] IDENTITY(1,1) NOT NULL,
                  NOT NULL,
                CONSTRAINT [PK_deps] PRIMARY KEY CLUSTERED
                (
                    [id] ASC
                )
                WITH 
                (
                    PAD_INDEX  = OFF,
                    STATISTICS_NORECOMPUTE  = OFF,
                    IGNORE_DUP_KEY = OFF,
                    ALLOW_ROW_LOCKS  = ON,
                    ALLOW_PAGE_LOCKS  = ON
                )
                ON [PRIMARY]
            ) ON [PRIMARY]
        END
    END')
        END;
GO
