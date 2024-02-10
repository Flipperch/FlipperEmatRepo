CREATE PROC [dbo].[usp_ENDERECO_ALUNOInsert] 
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
	
	INSERT INTO [dbo].[ENDERECO_ALUNO] ([N_MAT], [COD_CIDADE], [CEP], [LOGRADOURO], [NUMERO], [BAIRRO], [COMPLEMENTO])
	SELECT @N_MAT, @COD_CIDADE, @CEP, @LOGRADOURO, @NUMERO, @BAIRRO, @COMPLEMENTO
	
	-- Begin Return Select <- do not remove
	SELECT [N_MAT], [COD_CIDADE], [CEP], [LOGRADOURO], [NUMERO], [BAIRRO], [COMPLEMENTO]
	FROM   [dbo].[ENDERECO_ALUNO]
	WHERE  [N_MAT] = @N_MAT
	-- End Return Select <- do not remove
               
	COMMIT
