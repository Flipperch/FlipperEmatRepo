CREATE VIEW [dbo].[vwEnderecoAluno]
	AS 
	SELECT
		[N_MAT] MatriculaCeeja,
		[COD_CIDADE] CidadeId,
		[CEP] Cep,
		[LOGRADOURO] Logradouro,
		[NUMERO] Numero,
		[BAIRRO] Bairro,
		[COMPLEMENTO] Complemento
	FROM
		[ENDERECO_ALUNO]
