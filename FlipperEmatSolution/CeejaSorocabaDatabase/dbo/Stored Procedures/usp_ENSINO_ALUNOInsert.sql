CREATE PROC [dbo].[usp_ENSINO_ALUNOInsert] 
    @N_MAT int = NULL,
    @COD_ENSINO tinyint = NULL,
    @ATUAL bit = NULL,
    @DT_INICIO datetime = NULL,
    @DT_TERMINO datetime = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ENSINO_ALUNO] ([N_MAT], [COD_ENSINO], [ATUAL], [DT_INICIO], [DT_TERMINO])
	SELECT @N_MAT, @COD_ENSINO, @ATUAL, @DT_INICIO, @DT_TERMINO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [N_MAT], [COD_ENSINO], [ATUAL], [DT_INICIO], [DT_TERMINO]
	FROM   [dbo].[ENSINO_ALUNO]
	WHERE  [CODIGO] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
