using Emat.IntegracaoSedConsoleApp.Models;

namespace Emat.IntegracaoSedConsoleApp.Repositories
{
	public interface IAtivoEmatRepository
	{
		IEnumerable<AtivoEmat> GetAtivosEmat(DateTime dataUltimoAtendimento);
	}
}