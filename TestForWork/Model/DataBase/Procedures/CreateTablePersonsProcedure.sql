USE [EmployeeDB];
GO

IF OBJECT_ID('dbo.CreatePersonsTable', 'P') IS NULL
    BEGIN
        EXEC('CREATE PROCEDURE dbo.CreatePersonsTable
    AS
    BEGIN
        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = ''persons'' AND schema_id = SCHEMA_ID(''dbo''))
        BEGIN
            CREATE TABLE [dbo].[persons]
            (
                [id] [int] IDENTITY(1,1) NOT NULL,
                  NOT NULL,
                  NOT NULL,
                  NOT NULL,
                [date_employ] [datetime] NULL,
                [date_uneploy] [datetime] NULL,
                [status] [int] NOT NULL,
                [id_dep] [int] NOT NULL,
                [id_post] [int] NOT NULL,
                CONSTRAINT [PK_persons] PRIMARY KEY CLUSTERED
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
