CREATE PROC [dbo].[usp_DEFICIENCIAInsert] 
    @CODIGO smallint,
    @NOME int,
    @TIPO_DEFICIENCIA tinyint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[DEFICIENCIA] ([CODIGO], [NOME], [TIPO_DEFICIENCIA])
	SELECT @CODIGO, @NOME, @TIPO_DEFICIENCIA
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [TIPO_DEFICIENCIA]
	FROM   [dbo].[DEFICIENCIA]
	WHERE  [CODIGO] = @CODIGO
	-- End Return Select <- do not remove
               
	COMMIT
