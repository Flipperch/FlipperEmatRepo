using Dapper;
using Emat.IntegracaoSedConsoleApp.DataAccess;
using Emat.IntegracaoSedConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emat.IntegracaoSedConsoleApp.Repositories
{
	public class AtivoEmatRepository : IAtivoEmatRepository
	{
		private readonly DbSession _session;

		public AtivoEmatRepository(DbSession dbSession) => _session = dbSession;

		public IEnumerable<AtivoEmat> GetAtivosEmat(DateTime dataUltimoAtendimento)
		{
			string sql = @"
							SELECT 
								ALUNO.N_MAT NumeroMatricula, 
								ALUNO.NOME Nome,	
								ENSINO.NOME_ENSINO Ensino,
								DISCIPLINA.NOME Disciplina,
								TB_ATENDIMENTOS.DT_ATENDIMENTO UltimoAtendimento,
							
								--Dados Normalizados
								CASE
									WHEN RA LIKE '%-%'
										THEN REPLACE(SUBSTRING (RA,0,CHARINDEX('-',RA)),'.','')
									ELSE REPLACE(RA,'.','')
								END AS RaNormalizado,
							
								CASE
									WHEN RA LIKE '%-/%'
										THEN NULL
									WHEN RA LIKE '%-%'
										THEN SUBSTRING(RA,CHARINDEX('-',RA)+1,1)
									ELSE DIG_RA
								END AS DigRaNormalizado,
							
								CASE		
									WHEN RA LIKE '%SP%'
										THEN 'SP'
									ELSE UF_RA
								END AS UfRaNormalizado,
							
								--Dados tbAluno
								RA AS RaTbAluno,
								DIG_RA AS DigRaTbAluno,
								UF_RA AS UfRaTbAluno
							
							FROM
								DISCIPLINA_ALUNO 
								JOIN (
									SELECT MAX(CODIGO) ID_ATENDIMENTO, MAX(DT_ATENDIMENTO) DT_ATENDIMENTO, COD_DISCIPLINA_ALUNO ID_DISCIPLINA 
									FROM ATENDIMENTO_ALUNO 
									WHERE DT_ATENDIMENTO >= @DataUltimoAtendimento
									GROUP BY COD_DISCIPLINA_ALUNO 
									) TB_ATENDIMENTOS
								ON DISCIPLINA_ALUNO.CODIGO = TB_ATENDIMENTOS.ID_DISCIPLINA
							
							JOIN DISCIPLINA ON DISCIPLINA_ALUNO.COD_DISCIPLINA = DISCIPLINA.CODIGO
							
							JOIN ENSINO_ALUNO ON ENSINO_ALUNO.CODIGO = DISCIPLINA_ALUNO.COD_ENSINO_ALUNO
							
							JOIN ENSINO ON ENSINO_ALUNO.COD_ENSINO = ENSINO.CODIGO
							
							JOIN ALUNO ON ALUNO.N_MAT = ENSINO_ALUNO.N_MAT
							
							WHERE ALUNO.ATIVO = 1 AND ALUNO.CONCLUINTE = 0 AND ENSINO_ALUNO.ATUAL = 1 AND DISCIPLINA_ALUNO.ATUAL = 1 AND NOME_ENSINO = 'MÉDIO' AND RA IS NOT NULL AND    RA	<> '    '
							
							ORDER BY UltimoAtendimento asc
						";
			var result = _session.Connection.Query<AtivoEmat>(sql, new { DataUltimoAtendimento = dataUltimoAtendimento });
			return result;
		}
	}
}
