CREATE PROC [dbo].[usp_EMPREGO_ALUNOInsert] 
    @N_MAT int,
    @NOME_EMPRESA varchar(MAX) = NULL,
    @TELEFONE varchar(MAX) = NULL,
    @COD_CIDADE smallint = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[EMPREGO_ALUNO] ([N_MAT], [NOME_EMPRESA], [TELEFONE], [COD_CIDADE])
	SELECT @N_MAT, @NOME_EMPRESA, @TELEFONE, @COD_CIDADE
	
	-- Begin Return Select <- do not remove
	SELECT [N_MAT], [NOME_EMPRESA], [TELEFONE], [COD_CIDADE]
	FROM   [dbo].[EMPREGO_ALUNO]
	WHERE  [N_MAT] = @N_MAT
	-- End Return Select <- do not remove
               
	COMMIT
