CREATE PROC [dbo].[usp_PAISInsert] 
    @NOME varchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[PAIS] ([NOME])
	SELECT @NOME
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME]
	FROM   [dbo].[PAIS]
	WHERE  [CODIGO] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
