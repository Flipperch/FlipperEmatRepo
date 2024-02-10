CREATE PROC [dbo].[usp_DEFICIENCIAUpdate] 
    @CODIGO smallint,
    @NOME int,
    @TIPO_DEFICIENCIA tinyint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[DEFICIENCIA]
	SET    [NOME] = @NOME, [TIPO_DEFICIENCIA] = @TIPO_DEFICIENCIA
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [TIPO_DEFICIENCIA]
	FROM   [dbo].[DEFICIENCIA]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
