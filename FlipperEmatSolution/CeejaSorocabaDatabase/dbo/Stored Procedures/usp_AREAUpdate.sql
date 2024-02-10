CREATE PROC [dbo].[usp_AREAUpdate] 
    @CODIGO tinyint,
    @NOME varchar(80)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[AREA]
	SET    [NOME] = @NOME
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME]
	FROM   [dbo].[AREA]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
