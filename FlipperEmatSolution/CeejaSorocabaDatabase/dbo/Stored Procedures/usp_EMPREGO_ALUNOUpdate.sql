CREATE PROC [dbo].[usp_EMPREGO_ALUNOUpdate] 
    @N_MAT int,
    @NOME_EMPRESA varchar(MAX) = NULL,
    @TELEFONE varchar(MAX) = NULL,
    @COD_CIDADE smallint = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[EMPREGO_ALUNO]
	SET    [NOME_EMPRESA] = @NOME_EMPRESA, [TELEFONE] = @TELEFONE, [COD_CIDADE] = @COD_CIDADE
	WHERE  [N_MAT] = @N_MAT
	
	-- Begin Return Select <- do not remove
	SELECT [N_MAT], [NOME_EMPRESA], [TELEFONE], [COD_CIDADE]
	FROM   [dbo].[EMPREGO_ALUNO]
	WHERE  [N_MAT] = @N_MAT	
	-- End Return Select <- do not remove

	COMMIT
