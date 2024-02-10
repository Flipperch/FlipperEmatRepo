CREATE PROC [dbo].[usp_LOG_ACESSOUpdate] 
    @CODIGO int,
    @COD_USUARIO smallint,
    @TIPO bit,
    @DATA_HORA_ACESSO datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[LOG_ACESSO]
	SET    [COD_USUARIO] = @COD_USUARIO, [TIPO] = @TIPO, [DATA_HORA_ACESSO] = @DATA_HORA_ACESSO
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [COD_USUARIO], [TIPO], [DATA_HORA_ACESSO]
	FROM   [dbo].[LOG_ACESSO]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
