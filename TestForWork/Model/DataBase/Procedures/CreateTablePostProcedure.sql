USE [EmployeeDB];
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'posts' AND schema_id = SCHEMA_ID('dbo'))
    BEGIN
    CREATE TABLE [dbo].[posts]
    (
        [id] [int] IDENTITY(1,1) NOT NULL,
        [name] [varchar](100) NOT NULL,
        CONSTRAINT [PK_posts] PRIMARY KEY CLUSTERED
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
    end 
