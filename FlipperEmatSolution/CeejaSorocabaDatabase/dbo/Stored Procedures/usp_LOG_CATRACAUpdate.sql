CREATE PROC [dbo].[usp_LOG_CATRACAUpdate] 
    @CODIGO int,
    @COD_CARTAO_ACESSO int,
    @DATA_HORA datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[LOG_CATRACA]
	SET    [COD_CARTAO_ACESSO] = @COD_CARTAO_ACESSO, [DATA_HORA] = @DATA_HORA
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [COD_CARTAO_ACESSO], [DATA_HORA]
	FROM   [dbo].[LOG_CATRACA]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
