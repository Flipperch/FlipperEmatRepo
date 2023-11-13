CREATE TABLE [dbo].[Matricula]
(
	[MatriculaId] INT NOT NULL PRIMARY KEY, 
    [AlunoId] INT NOT NULL, 
    [EnsinoId] INT NOT NULL, 
    [Data] DATETIME NOT NULL, 
    [SituacaoId] INT NOT NULL, 
    [DataSituacao] DATETIME NOT NULL, 
    CONSTRAINT [FK_Matricula_Aluno] FOREIGN KEY ([AlunoId]) REFERENCES Aluno([AlunoId]),
    CONSTRAINT [FK_Matricula_Ensino] FOREIGN KEY ([EnsinoId]) REFERENCES Ensino([EnsinoId]),
    CONSTRAINT [FK_Matricula_Situacao] FOREIGN KEY ([SituacaoId]) REFERENCES Situacao([SituacaoId]) 
)
