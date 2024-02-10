CREATE PROC [dbo].[usp_UFUpdate] 
    @CODIGO smallint,
    @NOME varchar(MAX),
    @SIGLA varchar(2) = NULL,
    @COD_PAIS tinyint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[UF]
	SET    [NOME] = @NOME, [SIGLA] = @SIGLA, [COD_PAIS] = @COD_PAIS
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [SIGLA], [COD_PAIS]
	FROM   [dbo].[UF]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
