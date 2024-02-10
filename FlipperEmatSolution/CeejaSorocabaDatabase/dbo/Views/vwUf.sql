CREATE VIEW [dbo].[vwUf]
	AS SELECT 
		[CODIGO] UfId,
		[NOME] UfNome,
		[SIGLA] UfSigla,
		[COD_PAIS] PaisId
	FROM 
		UF
