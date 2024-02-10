CREATE VIEW [dbo].[vwTipoAtendimento]
	AS SELECT
		[CODIGO] TipoAtendimentoId, 
		[NOME] TipoAtendimentoNome,
		[COD_DISCIPLINA] DisciplinaId,
		[MENCAO] PermiteNota,
		[ATIVO] Ativo,
		[ORDEM] Ordem
	FROM
		ATENDIMENTO
