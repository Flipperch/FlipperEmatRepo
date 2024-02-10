CREATE PROC [dbo].[usp_CIDADEInsert] 
    @NOME varchar(MAX),
    @COD_UF smallint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[CIDADE] ([NOME], [COD_UF])
	SELECT @NOME, @COD_UF
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [COD_UF]
	FROM   [dbo].[CIDADE]
	WHERE  [CODIGO] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
