CREATE PROC [dbo].[usp_USUARIOInsert] 
    @NOME varchar(100) = NULL,
    @NOME_ACESSO varchar(20) = NULL,
    @SENHA varchar(20) = NULL,
    @RG varchar(20) = NULL,
    @NIVEL_ACESSO tinyint = NULL,
    @ATIVO bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[USUARIO] ([NOME], [NOME_ACESSO], [SENHA], [RG], [NIVEL_ACESSO], [ATIVO])
	SELECT @NOME, @NOME_ACESSO, @SENHA, @RG, @NIVEL_ACESSO, @ATIVO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [NOME_ACESSO], [SENHA], [RG], [NIVEL_ACESSO], [ATIVO]
	FROM   [dbo].[USUARIO]
	WHERE  [CODIGO] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
