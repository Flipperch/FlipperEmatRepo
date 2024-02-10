CREATE VIEW [dbo].[vwAluno]
	AS SELECT 
			[ALUNO].[N_MAT] MatriculaCeeja,
			[DT_MAT] DataMatriculaCeeja,
			[CPF] Cpf,
			[RA] Ra,
			[RG] Rg,
			[UF_RG] UfRg,
			[ORGAO_RG] OrgaoRg,
			[DT_RG] DataRg,
			[NOME] Nome,
			[DT_NASCIMENTO] DataNascimento, 
			[SEXO] Sexo, 
			[NOME_MAE] NomeMae, 
			[NOME_PAI] NomePai, 
			[ESTADO_CIVIL] EstadoCivil, 
			[COR_ORIGEM_ETNICA] CorOrigemEtnica,
			[TELEFONE] Telefone,
			[CELULAR] Celular,
			[TERMO_MAT] TermoMatricula,
			[E_MAIL] Email,
			[ATIVO] Ativo, 
			[CONCLUINTE] Concluinte, 
			[OBS_PASSAPORTE] ObsPassaporte, 
			[APRESENTOU_CERTIDAO] ApresentouCertidao, 
			[APRESENTOU_HISTORICO] ApresentouHistorico, 
			[NOME_SOCIAL] NomeSocial, 
			[COD_USUARIO] UsuarioId, 
			[DIG_RA] DigRa, 
			[UF_RA] UfRa,
			[LOCAL_NASCIMENTO].[COD_CIDADE] NascimentoCidadeId
		FROM 
			ALUNO
		LEFT JOIN
			LOCAL_NASCIMENTO
		ON 
			ALUNO.N_MAT = LOCAL_NASCIMENTO.N_MAT
