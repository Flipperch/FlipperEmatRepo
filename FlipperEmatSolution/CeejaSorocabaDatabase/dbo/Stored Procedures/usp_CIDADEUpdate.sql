CREATE PROC [dbo].[usp_CIDADEUpdate] 
    @CODIGO smallint,
    @NOME varchar(MAX),
    @COD_UF smallint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[CIDADE]
	SET    [NOME] = @NOME, [COD_UF] = @COD_UF
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [COD_UF]
	FROM   [dbo].[CIDADE]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
