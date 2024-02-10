CREATE VIEW [dbo].[vwDisciplina]
	AS SELECT
		[CODIGO] DisciplinaId,
		[NOME] NomeDisciplina,
		[NOME_HISTORICO] NomeDisciplinaHistorico,
		[HORARIO] Horario,
		[CAPACIDADE] Capacidade,
		[ORDEM] Ordem,
		[BLOQ_ATRIBUICAO] BloqueioAtribuicao
	FROM
		DISCIPLINA
