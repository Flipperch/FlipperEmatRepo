﻿CREATE PROC [dbo].[usp_ATENDIMENTOSelect] 
    @CODIGO smallint,
	@COD_DISCIPLINA TINYINT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN
	IF @CODIGO IS NOT NULL
		SELECT [CODIGO], [NOME], [COD_DISCIPLINA], [MENCAO], [ATIVO] , [ORDEM]
		FROM   [dbo].[ATENDIMENTO]
		WHERE  ([CODIGO] = @CODIGO OR @CODIGO IS NULL)
		ORDER BY ORDEM
	ELSE
		SELECT [CODIGO], [NOME], [COD_DISCIPLINA], [MENCAO], [ATIVO] , [ORDEM]
		FROM   [dbo].[ATENDIMENTO]
		WHERE  ((([COD_DISCIPLINA] = @COD_DISCIPLINA) OR (@COD_DISCIPLINA IS NULL)) AND ATIVO = 1)
		ORDER BY ORDEM
	COMMIT
