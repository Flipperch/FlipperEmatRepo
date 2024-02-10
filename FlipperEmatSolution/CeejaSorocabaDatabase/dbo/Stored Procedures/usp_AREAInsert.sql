CREATE PROC [dbo].[usp_AREAInsert] 
    @NOME varchar(80)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[AREA] ([NOME])
	SELECT @NOME
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME]
	FROM   [dbo].[AREA]
	WHERE  [CODIGO] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
