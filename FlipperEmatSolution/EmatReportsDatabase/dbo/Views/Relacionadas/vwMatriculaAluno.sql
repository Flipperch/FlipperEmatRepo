CREATE VIEW [dbo].[vwMatriculaAluno]
	AS SELECT 
		[dbo].[vwMatricula].[MatriculaId], 
		[dbo].[vwMatricula].[AlunoId], 
		[dbo].[vwMatricula].[EnsinoId], 
		[dbo].[vwMatricula].[Data], 
		[dbo].[vwMatricula].[SituacaoId], 
		[dbo].[vwMatricula].[DataSituacao], 
		[dbo].[vwAluno].[Nome], 
		[dbo].[vwAluno].[Rg],
		[dbo].[vwAluno].[Ra], 
		[dbo].[vwEnsino].[Nome] Ensino, 
		[dbo].[vwSituacao].[Nome] Situacao 
	FROM [vwMatricula]
	JOIN [vwAluno] ON [vwMatricula].[AlunoId] = [vwAluno].[AlunoId]
	JOIN [vwEnsino] ON [vwMatricula].[EnsinoId] = [vwEnsino].[EnsinoId]
	JOIN [vwSituacao] ON [vwMatricula].[SituacaoId] = [vwSituacao].[SituacaoId]