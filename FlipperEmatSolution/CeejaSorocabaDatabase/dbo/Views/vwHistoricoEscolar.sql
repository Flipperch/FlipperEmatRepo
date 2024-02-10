CREATE VIEW [dbo].[vwHistoricoEscolar]
	AS SELECT
 	    [COD_ENSINO_ALUNO] MatriculaId,
 	    [OBS] Observacoes,
 	    [COD_USUARIO_DIRETOR] UsuarioDiretorId,
 	    [COD_USUARIO_SECRETARIO] UsuarioSecretarioId,
 	    [COD_USUARIO] UsuarioId,
 	    [DT_LIVRO] DataLivro,
 	    [LIVRO] Livro,
 	    [PAGINA] Pagina,
 	    [TERMO] Termo,
 	    [DT_DOCUMENTO] DataDocumento,
 	    [DT_CONCLUSAO] DataConclusao,
 	    [SERIE_ANTERIOR] SerieAnterior,
 	    [INSTITUICAO_ANTERIOR] InstituicaoAnterior,
 	    [ANO_ANTERIOR] AnoAnterior,
 	    [COD_CIDADE_ANTERIOR] CidadeAnteriorId,
 	    [FUNDAMENTACAO] Fundamentacao,
 	    [GDAE] Gdae,
 	    [SEGUNDA_VIA] SegundaVia
 	FROM
 	    HISTORICO_ESCOLAR
