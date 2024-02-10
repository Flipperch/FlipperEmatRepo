CREATE TABLE [dbo].[PRINT_LOG] (
    [ID_PRINT_LOG] INT           IDENTITY (1, 1) NOT NULL,
    [DATETIME]     DATETIME      NULL,
    [N_MAT]        INT           NULL,
    [REPORT_NAME]  VARCHAR (50)  NULL,
    [ID_USER]      INT           NULL,
    [PARAMETERS]   VARCHAR (300) NULL
);

