namespace Emat.IntegracaoSedConsoleApp.Models
{
	/// <summary>
	/// Classe para modelo de uma nova matricula na sed.
	/// </summary>
	public class NovaMatricula
	{
		public string Ra { get; set; }
		public string NomeDisciplina { get; set; }

		public NovaMatricula(string ra, string nomeDisciplina)
		{
			Ra = ra;
			NomeDisciplina = nomeDisciplina;
		}
	}
}
