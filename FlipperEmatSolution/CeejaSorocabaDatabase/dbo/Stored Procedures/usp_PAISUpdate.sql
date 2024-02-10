CREATE PROC [dbo].[usp_PAISUpdate] 
    @CODIGO tinyint,
    @NOME varchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[PAIS]
	SET    [NOME] = @NOME
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME]
	FROM   [dbo].[PAIS]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
