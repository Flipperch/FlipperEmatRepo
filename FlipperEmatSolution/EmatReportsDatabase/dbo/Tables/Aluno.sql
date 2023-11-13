CREATE TABLE [dbo].[Aluno]
(
    [AlunoId] INT NOT NULL, 
    [Nome] VARCHAR(200) NOT NULL, 
    [Rg] VARCHAR(50) NULL, 
    [Ra] VARCHAR(50) NULL, 
    CONSTRAINT [PK_Aluno] PRIMARY KEY ([AlunoId])
)
