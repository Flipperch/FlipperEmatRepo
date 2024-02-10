CREATE PROC [dbo].[usp_CARTAO_ACESSOInsert] 
    @CODIGO int,
    @N_MAT int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[CARTAO_ACESSO] ([CODIGO], [N_MAT])
	SELECT @CODIGO, @N_MAT
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [N_MAT]
	FROM   [dbo].[CARTAO_ACESSO]
	WHERE  [CODIGO] = @CODIGO
	-- End Return Select <- do not remove
               
	COMMIT
