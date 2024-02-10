CREATE PROC [dbo].[usp_LOG_ACESSOInsert] 
    @CODIGO int,
    @COD_USUARIO smallint,
    @TIPO bit,
    @DATA_HORA_ACESSO datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LOG_ACESSO] ([CODIGO], [COD_USUARIO], [TIPO], [DATA_HORA_ACESSO])
	SELECT @CODIGO, @COD_USUARIO, @TIPO, @DATA_HORA_ACESSO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [COD_USUARIO], [TIPO], [DATA_HORA_ACESSO]
	FROM   [dbo].[LOG_ACESSO]
	WHERE  [CODIGO] = @CODIGO
	-- End Return Select <- do not remove
               
	COMMIT
