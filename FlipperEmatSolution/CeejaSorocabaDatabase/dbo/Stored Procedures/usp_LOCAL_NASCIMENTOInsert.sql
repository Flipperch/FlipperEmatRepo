CREATE PROC [dbo].[usp_LOCAL_NASCIMENTOInsert] 
    @N_MAT int,
    @COD_CIDADE smallint = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LOCAL_NASCIMENTO] ([N_MAT], [COD_CIDADE])
	SELECT @N_MAT, @COD_CIDADE
	
	-- Begin Return Select <- do not remove
	SELECT [N_MAT], [COD_CIDADE]
	FROM   [dbo].[LOCAL_NASCIMENTO]
	WHERE  [N_MAT] = @N_MAT
	-- End Return Select <- do not remove
               
	COMMIT
