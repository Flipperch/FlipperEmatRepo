CREATE VIEW [dbo].[vwNota]
	AS SELECT 
		[COD_ATENDIMENTO_ALUNO] AtendimentoId, 
		[NOTA] NotaValor
	FROM 
		NOTA
