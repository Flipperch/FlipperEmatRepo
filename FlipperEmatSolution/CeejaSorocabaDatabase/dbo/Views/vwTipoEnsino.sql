CREATE VIEW [dbo].[vwTipoEnsino]
	AS SELECT 
	[CODIGO] TipoEnsinoId, 
	[NOME_ENSINO] TipoEnsinoNome
	FROM
	ENSINO
