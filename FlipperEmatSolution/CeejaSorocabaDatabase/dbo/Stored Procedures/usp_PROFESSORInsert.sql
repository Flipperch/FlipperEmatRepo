CREATE PROC [dbo].[usp_PROFESSORInsert] 
    @CODIGO smallint,
    @COD_DISCIPLINA tinyint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[PROFESSOR] ([CODIGO], [COD_DISCIPLINA])
	SELECT @CODIGO, @COD_DISCIPLINA
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [COD_DISCIPLINA]
	FROM   [dbo].[PROFESSOR]
	WHERE  [CODIGO] = @CODIGO
	-- End Return Select <- do not remove
               
	COMMIT
