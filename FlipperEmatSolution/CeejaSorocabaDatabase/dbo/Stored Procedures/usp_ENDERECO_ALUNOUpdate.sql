CREATE PROC [dbo].[usp_ENDERECO_ALUNOUpdate] 
    @N_MAT int,
    @COD_CIDADE smallint,
    @CEP varchar(MAX) = NULL,
    @LOGRADOURO varchar(MAX) = NULL,
    @NUMERO int = NULL,
    @BAIRRO varchar(MAX) = NULL,
    @COMPLEMENTO varchar(MAX) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ENDERECO_ALUNO]
	SET    [COD_CIDADE] = @COD_CIDADE, [CEP] = @CEP, [LOGRADOURO] = @LOGRADOURO, [NUMERO] = @NUMERO, [BAIRRO] = @BAIRRO, [COMPLEMENTO] = @COMPLEMENTO
	WHERE  [N_MAT] = @N_MAT
	
	-- Begin Return Select <- do not remove
	SELECT [N_MAT], [COD_CIDADE], [CEP], [LOGRADOURO], [NUMERO], [BAIRRO], [COMPLEMENTO]
	FROM   [dbo].[ENDERECO_ALUNO]
	WHERE  [N_MAT] = @N_MAT	
	-- End Return Select <- do not remove

	COMMIT
