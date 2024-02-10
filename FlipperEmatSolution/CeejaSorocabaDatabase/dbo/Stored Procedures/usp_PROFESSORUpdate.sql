CREATE PROC [dbo].[usp_PROFESSORUpdate] 
    @CODIGO smallint,
    @COD_DISCIPLINA tinyint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[PROFESSOR]
	SET    [COD_DISCIPLINA] = @COD_DISCIPLINA
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [COD_DISCIPLINA]
	FROM   [dbo].[PROFESSOR]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
