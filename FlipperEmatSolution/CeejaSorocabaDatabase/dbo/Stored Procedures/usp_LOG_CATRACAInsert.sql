CREATE PROC [dbo].[usp_LOG_CATRACAInsert] 
    @COD_CARTAO_ACESSO int,
    @DATA_HORA datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LOG_CATRACA] ([COD_CARTAO_ACESSO], [DATA_HORA])
	SELECT @COD_CARTAO_ACESSO, @DATA_HORA
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [COD_CARTAO_ACESSO], [DATA_HORA]
	FROM   [dbo].[LOG_CATRACA]
	WHERE  [CODIGO] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
