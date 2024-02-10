CREATE PROC [dbo].[usp_CARTAO_ACESSOUpdate] 
    @CODIGO int,
    @N_MAT int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[CARTAO_ACESSO]
	SET    [N_MAT] = @N_MAT
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [N_MAT]
	FROM   [dbo].[CARTAO_ACESSO]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
