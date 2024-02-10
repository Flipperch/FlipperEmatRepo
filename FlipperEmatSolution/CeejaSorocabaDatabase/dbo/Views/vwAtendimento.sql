CREATE VIEW [dbo].[vwAtendimento]
	AS SELECT
 	    [CODIGO] AtendimentoId,
 	    [COD_DISCIPLINA_ALUNO] DisciplinaAlunoId,
 	    [COD_ATENDIMENTO] TipoAtendimentoId,
 	    [COD_PROFESSOR] ProfessorId,
 	    [DT_ATENDIMENTO] DataAtendimento,
 	    [COD_PROFESSOR_MODIFICACAO] ProfessorAlteracaoId,
 	    [DT_MODIFICACAO] DataAlteracao,
 	    [MODULO] Modulo,
 	    [ORDEM] Ordem
    FROM
 	    ATENDIMENTO_ALUNO
