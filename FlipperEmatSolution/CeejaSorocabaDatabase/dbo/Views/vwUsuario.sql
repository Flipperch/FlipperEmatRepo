CREATE VIEW [dbo].[vwUsuario]
	AS SELECT
	[CODIGO] UsuarioId, 
	[NOME] Nome,
	[NOME_ACESSO] NomeAcesso,
	[SENHA] Senha, 
	[RG] Rg,
	[NIVEL_ACESSO] NivelAcesso, 
	[ATIVO] Ativo
	FROM
	USUARIO
