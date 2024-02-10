CREATE VIEW [dbo].[vwCidade]
	AS SELECT
		[CODIGO] CidadeId,
		[NOME] CidadeNome,
		[COD_UF] UfId
	FROM 
		CIDADE
