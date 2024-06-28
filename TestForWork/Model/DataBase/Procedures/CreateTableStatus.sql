USE [EmployeeDB];
    IF OBJECT_ID('dbo.status', 'U') IS NULL
        BEGIN
            CREATE TABLE [dbo].[status]
            (
                [id] [int] IDENTITY(1,1) NOT NULL,
                [name] [varchar](100) NOT NULL,
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
   
