CREATE PROC [dbo].[usp_UFInsert] 
    @NOME varchar(MAX),
    @SIGLA varchar(2) = NULL,
    @COD_PAIS tinyint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[UF] ([NOME], [SIGLA], [COD_PAIS])
	SELECT @NOME, @SIGLA, @COD_PAIS
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [SIGLA], [COD_PAIS]
	FROM   [dbo].[UF]
	WHERE  [CODIGO] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
