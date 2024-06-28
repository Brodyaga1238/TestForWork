USE EmployeeDB;


    IF OBJECT_ID('dbo.persons', 'U') IS NULL
        BEGIN
            CREATE TABLE [dbo].[persons]
            (
                [id] [int] IDENTITY(1,1) NOT NULL,
                [first_name] [varchar](100) NOT NULL,
                [second_name] [varchar](100) NOT NULL,
                [last_name] [varchar](100) NOT NULL,
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
 
