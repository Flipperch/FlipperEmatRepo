CREATE PROC [dbo].[usp_USUARIOUpdate] 
    @CODIGO smallint,
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

	UPDATE [dbo].[USUARIO]
	SET    [NOME] = @NOME, [NOME_ACESSO] = @NOME_ACESSO, [SENHA] = @SENHA, [RG] = @RG, [NIVEL_ACESSO] = @NIVEL_ACESSO, [ATIVO] = @ATIVO
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [NOME_ACESSO], [SENHA], [RG], [NIVEL_ACESSO], [ATIVO]
	FROM   [dbo].[USUARIO]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
