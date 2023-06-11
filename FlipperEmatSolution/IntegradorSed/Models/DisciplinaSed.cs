namespace Emat.IntegracaoSedConsoleApp.Models
{
	public class DisciplinaSed
	{
		public int DisciplinaSedId { get; set; }
		public string Nome { get; set; } = string.Empty;
		public int CodigoDiscipinaOption { get; set; }
		public string OptionValue { get; set; } = string.Empty;
		public static DisciplinaSed GetDisciplinaSedByNome(string nome) => GetListaDisciplinasSed().First(d => d.Nome == nome);
		public static IEnumerable<DisciplinaSed> GetListaDisciplinasSed()
		{
			List<DisciplinaSed> list = new List<DisciplinaSed>
			{
				new DisciplinaSed() { DisciplinaSedId = 1, Nome = "LINGUA PORTUGUESA", CodigoDiscipinaOption = 0, OptionValue = "1100" },
				new DisciplinaSed() { DisciplinaSedId = 2, Nome = "ARTE", CodigoDiscipinaOption = 1, OptionValue = "1813" },
				new DisciplinaSed() { DisciplinaSedId = 3, Nome = "EDUCACAO FISICA", CodigoDiscipinaOption = 2, OptionValue = "1900" },
				new DisciplinaSed() { DisciplinaSedId = 4, Nome = "GEOGRAFIA", CodigoDiscipinaOption = 3, OptionValue = "2100" },
				new DisciplinaSed() { DisciplinaSedId = 5, Nome = "HISTORIA", CodigoDiscipinaOption = 4, OptionValue = "2200" },
				new DisciplinaSed() { DisciplinaSedId = 6, Nome = "SOCIOLOGIA", CodigoDiscipinaOption = 5, OptionValue = "2300" },
				new DisciplinaSed() { DisciplinaSedId = 7, Nome = "BIOLOGIA", CodigoDiscipinaOption = 6, OptionValue = "2400" },
				new DisciplinaSed() { DisciplinaSedId = 8, Nome = "FISICA", CodigoDiscipinaOption = 7, OptionValue = "2600" },
				new DisciplinaSed() { DisciplinaSedId = 9, Nome = "MATEMATICA", CodigoDiscipinaOption = 8, OptionValue = "2700" },
				new DisciplinaSed() { DisciplinaSedId = 10, Nome = "QUIMICA", CodigoDiscipinaOption = 9, OptionValue = "2800" },
				new DisciplinaSed() { DisciplinaSedId = 11, Nome = "FILOSOFIA", CodigoDiscipinaOption = 10, OptionValue = "3100" },
				new DisciplinaSed() { DisciplinaSedId = 12, Nome = "LINGUA INGLESA", CodigoDiscipinaOption = 11, OptionValue = "8467" }
			};

			return list;
		}
		public static string NormalizarNomeDisciplinaEmatParaSed(string nomeEmat)
		{
			string nomeSed = "";
			switch (nomeEmat)
			{
				case "PORTUGUÊS":
					nomeSed = "LINGUA PORTUGUESA";
					break;
				case "ARTE":
					nomeSed = "ARTE";
					break;
				case "GEOGRAFIA":
					nomeSed = "GEOGRAFIA";
					break;
				case "HISTÓRIA":
					nomeSed = "HISTORIA";
					break;
				case "SOCIOLOGIA":
					nomeSed = "SOCIOLOGIA";
					break;
				case "BIOLOGIA":
					nomeSed = "BIOLOGIA";
					break;
				case "FÍSICA":
					nomeSed = "FISICA";
					break;
				case "MATEMÁTICA":
					nomeSed = "MATEMATICA";
					break;
				case "QUÍMICA":
					nomeSed = "QUIMICA";
					break;
				case "FILOSOFIA":
					nomeSed = "FILOSOFIA";
					break;
				case "INGLÊS":
					nomeSed = "LINGUA INGLESA";
					break;
			}
			return nomeSed;
		}
	}
}
