﻿CREATE PROC [dbo].[usp_USUARIOSelect] 
    @CODIGO smallint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [CODIGO], [NOME], [NOME_ACESSO], [SENHA], [RG], [NIVEL_ACESSO], [ATIVO] 
	FROM   [dbo].[USUARIO] 
	WHERE  ([CODIGO] = @CODIGO OR @CODIGO IS NULL) 

	COMMIT
