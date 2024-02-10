CREATE PROC [dbo].[usp_LOCAL_NASCIMENTOUpdate] 
    @N_MAT int,
    @COD_CIDADE smallint = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[LOCAL_NASCIMENTO]
	SET    [COD_CIDADE] = @COD_CIDADE
	WHERE  [N_MAT] = @N_MAT
	
	-- Begin Return Select <- do not remove
	SELECT [N_MAT], [COD_CIDADE]
	FROM   [dbo].[LOCAL_NASCIMENTO]
	WHERE  [N_MAT] = @N_MAT	
	-- End Return Select <- do not remove

	COMMIT
