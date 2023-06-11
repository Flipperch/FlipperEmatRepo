using Emat.IntegracaoSedConsoleApp.Models;
using System.Security.Cryptography;

namespace Emat.IntegracaoSedConsoleApp.Services
{
	public interface IIntegradorSedService
	{
		IEnumerable<InscricaoSed> CarregarInscricoesArquivo(string path);
		IEnumerable<InscricaoSed> CarregarInscricoesSed(string fase, string redeEnsino);

		IEnumerable<InscricaoSed> CarregarInscricoesNaoMatriculadas(string pathInscricoes, string pathMatriculas);
		
		IEnumerable<MatriculaSed> CarregarMatriculas(string path);
		
		
		
		void MatricularAtivosEmatNaSed(IEnumerable<AtivoEmat> ativos, string numeroClasse);

		IEnumerable<AtivoEmat> SelecionarNovasMatriculas(
			IEnumerable<InscricaoSed> inscricoes,
			IEnumerable<MatriculaSed> matriculas,
			IEnumerable<AtivoEmat> novasMatriculas);



		IEnumerable<AtivoEmat> CarregarAtivosEmat(string path);
		IEnumerable<AtivoEmat> CarregarAtivosEmatDataBase(DateTime dataUltimoAtendimento);

		IEnumerable<AtivoEmat> AtivosEmatNaoMatriculadosNaSed(IEnumerable<MatriculaSed> matriculas, IEnumerable<AtivoEmat> ativos);
	}
}