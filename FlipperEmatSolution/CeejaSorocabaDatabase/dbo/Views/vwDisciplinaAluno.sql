CREATE VIEW [dbo].[vwDisciplinaAluno]
	AS SELECT
 	    [CODIGO] DisciplinaAlunoId,
 	    [COD_ENSINO_ALUNO] MatriculaId,
 	    [COD_DISCIPLINA] DisciplinaId,
 	    [ATUAL] Atual,
 	    [CONCLUIDA] Concluida
 	FROM
 	    DISCIPLINA_ALUNO
