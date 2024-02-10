CREATE VIEW [dbo].[vwMedia]
	AS SELECT
 	    [COD_DISCIPLINA_ALUNO] DisciplinaAlunoId,
 	    [VALOR] MediaValor,
 	    [DT_MEDIA] MediaData,
 	    [COD_USUARIO] MediaUsuarioId,
 	    [COD_USUARIO_MODIFICACAO] ModificacaoMediaUsuarioId,
 	    [DT_MODIFICACAO] ModificacaoMediaData,
 	    [COD_ATENDIMENTO_ALUNO] AtendimentoId,
 	    [COD_CIDADE] CidadeId,
 	    [INSTITUICAO] Instituicao
    FROM
 	    MEDIA
