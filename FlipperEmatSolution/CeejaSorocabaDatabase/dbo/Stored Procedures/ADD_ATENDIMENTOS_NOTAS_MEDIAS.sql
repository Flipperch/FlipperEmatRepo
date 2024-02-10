--função para incluir atendimento, nota e média

CREATE PROC ADD_ATENDIMENTOS_NOTAS_MEDIAS
AS
BEGIN
	DECLARE @resultadoCodAtendimento AS INT
	DECLARE @resultadoCodDisciplinaAluno AS SMALLINT
	DECLARE @resultadoCodProfessor AS SMALLINT
	DECLARE @resultadoDtAtendimento AS DATETIME
	DECLARE @resultadoCodProfessorModificacao AS SMALLINT
	DECLARE @resultadoDtModificacao AS DATETIME
	DECLARE @resultadoModulo AS VARCHAR(20)
	DECLARE @resultadoNota AS FLOAT
--INSERT DE ATENDIMENTO_ALUNO
INSERT INTO ceeja_sorocaba.dbo.ATENDIMENTO_ALUNO 
VALUES(@resultadoCodDisciplinaAluno,@resultadoCodAtendimento,@resultadoCodProfessor,@resultadoDtAtendimento,@resultadoCodProfessorModificacao,@resultadoDtModificacao,@resultadoModulo)
--PEGA NOVO ATENDIMENTO_ALUNO
DECLARE @resultadoCodNovoAtendimentoAluno AS INT
SET @resultadoCodNovoAtendimentoAluno = SCOPE_IDENTITY()
--INSERT DE NOTA
INSERT INTO ceeja_sorocaba.DBO.NOTA VALUES (@resultadoCodNovoAtendimentoAluno, @resultadoNota)
--INSERT DE MÉDIA
--INSERT INTO ceeja_sorocaba.DBO.MEDIA VALUES (@codInstituicao, @codDisciplinaAluno, @valor, dtMedia, codUsuario, codUsuarioModificacao, @dtModificacao, @resultadoCodNovoAtendimentoAluno)
END