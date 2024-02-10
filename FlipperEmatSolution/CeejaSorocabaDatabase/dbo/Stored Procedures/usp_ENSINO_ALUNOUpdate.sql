CREATE PROC [dbo].[usp_ENSINO_ALUNOUpdate] 
    @CODIGO int,
    @N_MAT int = NULL,
    @COD_ENSINO tinyint = NULL,
    @ATUAL bit = NULL,
    @DT_INICIO datetime = NULL,
    @DT_TERMINO datetime = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ENSINO_ALUNO]
	SET    [N_MAT] = @N_MAT, [COD_ENSINO] = @COD_ENSINO, [ATUAL] = @ATUAL, [DT_INICIO] = @DT_INICIO, [DT_TERMINO] = @DT_TERMINO
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [N_MAT], [COD_ENSINO], [ATUAL], [DT_INICIO], [DT_TERMINO]
	FROM   [dbo].[ENSINO_ALUNO]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
