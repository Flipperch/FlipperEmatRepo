CREATE PROC [dbo].[usp_AREA_PROFESSORInsert] 
    @COD_PROFESSOR smallint,
    @COD_AREA tinyint
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[AREA_PROFESSOR] ([COD_PROFESSOR], [COD_AREA])
	SELECT @COD_PROFESSOR, @COD_AREA
	
	-- Begin Return Select <- do not remove
	SELECT [COD_PROFESSOR], [COD_AREA]
	FROM   [dbo].[AREA_PROFESSOR]
	WHERE  [COD_PROFESSOR] = @COD_PROFESSOR
	       AND [COD_AREA] = @COD_AREA
	-- End Return Select <- do not remove
               
	COMMIT
