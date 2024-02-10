CREATE PROC [dbo].[usp_ATENDIMENTOUpdate] 
    @CODIGO smallint,
    @NOME varchar(50),
    @COD_DISCIPLINA tinyint,
    @MENCAO bit,
    @ATIVO bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ATENDIMENTO]
	SET    [NOME] = @NOME, [COD_DISCIPLINA] = @COD_DISCIPLINA, [MENCAO] = @MENCAO, [ATIVO] = @ATIVO
	WHERE  [CODIGO] = @CODIGO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [COD_DISCIPLINA], [MENCAO], [ATIVO]
	FROM   [dbo].[ATENDIMENTO]
	WHERE  [CODIGO] = @CODIGO	
	-- End Return Select <- do not remove

	COMMIT
