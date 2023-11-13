CREATE VIEW [dbo].[vwCarteirinha]
	AS SELECT 
		[AlunoId], 
		[Nome],
		[Rg],
		[Ra],
		[Ensino]
	FROM [vwMatriculaAluno]
