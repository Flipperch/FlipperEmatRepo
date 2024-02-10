CREATE PROC [dbo].[usp_ATENDIMENTOInsert] 
    @NOME varchar(50),
    @COD_DISCIPLINA tinyint,
    @MENCAO bit,
    @ATIVO bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ATENDIMENTO] ([NOME], [COD_DISCIPLINA], [MENCAO], [ATIVO])
	SELECT @NOME, @COD_DISCIPLINA, @MENCAO, @ATIVO
	
	-- Begin Return Select <- do not remove
	SELECT [CODIGO], [NOME], [COD_DISCIPLINA], [MENCAO], [ATIVO]
	FROM   [dbo].[ATENDIMENTO]
	WHERE  [CODIGO] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
