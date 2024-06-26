USE [EmployeeDB];


IF OBJECT_ID('dbo.CreateStatusTable', 'P') IS NULL
    BEGIN
        EXEC('CREATE PROCEDURE dbo.CreateStatusTable AS BEGIN
    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = ''status'' AND schema_id = SCHEMA_ID(''dbo''))
        BEGIN
            CREATE TABLE [dbo].[status]
            (
                [id] [int] IDENTITY(1,1) NOT NULL,
                  NOT NULL,
                CONSTRAINT [PK_status] PRIMARY KEY CLUSTERED
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
            )
                ON [PRIMARY]
        END
    END')
    END;

