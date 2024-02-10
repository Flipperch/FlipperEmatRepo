CREATE VIEW [dbo].[vwMatricula]
	AS SELECT 
	[CODIGO] MatriculaId,
	[N_MAT] MatriculaCeeja,
	[COD_ENSINO] TipoEnsinoId,
	[ATUAL] Atual,
	[DT_INICIO] DataInicio,
	[DT_TERMINO] DataFinal
	FROM
	ENSINO_ALUNO
