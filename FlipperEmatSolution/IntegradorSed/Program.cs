using Emat.IntegracaoSedConsoleApp.DataAccess;
using Emat.IntegracaoSedConsoleApp.Repositories;
using Emat.IntegracaoSedConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text;

try
{
	Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.WriteTo.Console()
	.WriteTo.File("C:\\Logs\\log.txt",
		rollingInterval: RollingInterval.Day,
		rollOnFileSizeLimit: true)
	.CreateLogger();

	Log.Information("Starting Up IntegracaoSedConsoleApp");
	Log.Information("Log Path: C:\\Logs\\log.txt");
	Log.Information("Create and Configure Host");

	using IHost host = Host.CreateDefaultBuilder(args)
		.ConfigureHostConfiguration(configHost =>
		{
			configHost.SetBasePath(Directory.GetCurrentDirectory());
			configHost.AddJsonFile("appsettings.json", optional: true);
			configHost.AddEnvironmentVariables(prefix: "PREFIX_");
			configHost.AddCommandLine(args);
		})
		.ConfigureServices((hostContext, services) =>
		{
			services.AddTransient<DbSession>();
			services.AddTransient<IAtivoEmatRepository, AtivoEmatRepository>();
			services.AddTransient<IIntegradorSedService, IntegradorSedService>();
		})
		.Build();

	TestarIntegrador(host.Services);

	await host.RunAsync();
}
catch (Exception e)
{
	Log.Error(e.Message);
}

static void TestarIntegrador(IServiceProvider serviceProvider)
{
	try
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider provider = serviceScope.ServiceProvider;
		IIntegradorSedService service = provider.GetRequiredService<IIntegradorSedService>();

		Console.WriteLine("Testar Integrador SED");
		Console.WriteLine("Selecione uma opção");
		Console.WriteLine("1 - CarregarInscricoesSed");

		if (int.TryParse(Console.ReadLine(), out int usrInput))
		{
			switch (usrInput)
			{
				case 1:
					service.CarregarInscricoesSed("INSCRIÇÃO E.M.", "ESTADUAL - SE");
					break;
				default:
					break;
			}
		}
	}
	catch (Exception)
	{
		throw;
	}
}

static void CarregarAtivosEmat(IServiceProvider serviceProvider)
{
	try
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider provider = serviceScope.ServiceProvider;
		IIntegradorSedService service = provider.GetRequiredService<IIntegradorSedService>();

		var ativosEmat = service.CarregarAtivosEmatDataBase(ObterData());

		Console.WriteLine(ativosEmat.Count());
	}
	catch (Exception)
	{
		throw;
	}
}

static void MatricularAtivosEmat(IServiceProvider serviceProvider)
{
	try
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider provider = serviceScope.ServiceProvider;
		IIntegradorSedService service = provider.GetRequiredService<IIntegradorSedService>();


		Console.WriteLine("Matricular Alunos Ativos Emat na SED");

		//AtivoEmat
		
		var ativosEmat = service.CarregarAtivosEmatDataBase(ObterData());
		Console.WriteLine($"Ativos no Emat: {ativosEmat.Count()}");

		////InscricaoSed
		//var inscricoes = service.CarregarInscricoes(ObterPath("Informe o caminho dos arquivos de inscrições da SED:"));
		//Console.WriteLine($"Inscrições na SED: {inscricoes.Count()}");

		//MatriculaSed
		var matriculas = service.CarregarMatriculas(ObterPath("Informe o caminho dos arquivos de matrículas da SED"));
		Console.WriteLine($"Matrículas na SED: {matriculas.Count()}");

		//Ativos no Emat não Matriculados na SED
		var ativosNaoMatriculados = service.AtivosEmatNaoMatriculadosNaSed(matriculas, ativosEmat);
		Console.WriteLine($"Ativos no Emat não Matriculados na SED: {ativosNaoMatriculados.Count()}");

		////Inscrições não matriculadas //TODO:: Verificar como utilizar inscrições SED para abater no processo de matrícula...
		//var inscricoesNaoMatriculadas = service.CarregarInscricoesNaoMatriculadas(inscricoesPath, matriculasPath);
		//Console.WriteLine($"Inscrições não matriculadas: {inscricoesNaoMatriculadas.Count()}");

		//Matricular Ativos
		service.MatricularAtivosEmatNaSed(ativosNaoMatriculados, ObterNumeroClasse());

	}
	catch (Exception)
	{
		throw;
	}
}

static DateTime ObterData()
{
	Console.WriteLine("Informe a data do último atendimento.");

	string input = Console.ReadLine() ?? string.Empty;

	DateTime.TryParse(input, out DateTime dt);

	Console.WriteLine($"Data informada: {dt}");

	return dt;
}

static string ObterPath(string mensagem)
{
	Console.WriteLine(mensagem);
	return Console.ReadLine() ?? string.Empty;
}

static string ObterNumeroClasse()
{
	Console.WriteLine("Informe o número da classe para adicionar os alunos:");
	return Console.ReadLine() ?? string.Empty;
}

static void RodarIntegradorSed(IServiceProvider serviceProvider)
{
	Log.Information("RodarIntegradorSed...");

	try
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider provider = serviceScope.ServiceProvider;
		IIntegradorSedService service = provider.GetRequiredService<IIntegradorSedService>();

		Console.WriteLine("Informe o caminho (path) do arquivo: Relacao de Inscricao");
		string pathRelacaoInscricao = Console.ReadLine() ?? string.Empty;

		Console.WriteLine("Informe o caminho (path) do arquivo: Relacao de Alunos por Classe");
		string pathRelacaoAlunosPorClasse = Console.ReadLine() ?? string.Empty;

		Console.WriteLine("Informe o caminho (path) do arquivo: Ativos");
		string pathAtivosEmatCsv = Console.ReadLine() ?? string.Empty;

		var listaRelacaoInscricao = service.CarregarInscricoesArquivo(pathRelacaoInscricao);

		var listaRelacaoAlunosPorClasse = service.CarregarMatriculas(pathRelacaoAlunosPorClasse);

		var ativosEmat = service.CarregarAtivosEmat(pathAtivosEmatCsv);

		var listaNovasMatriculas = service.SelecionarNovasMatriculas(listaRelacaoInscricao, listaRelacaoAlunosPorClasse, ativosEmat);

		service.MatricularAtivosEmatNaSed(listaNovasMatriculas, ObterNumeroClasse());
	}
	catch (Exception)
	{
		throw;
	}
}

static void ExibirInscricoes(IServiceProvider serviceProvider)
{
	try
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider provider = serviceScope.ServiceProvider;
		IIntegradorSedService service = provider.GetRequiredService<IIntegradorSedService>();

		Console.WriteLine("Informe o caminho (path): Inscrições");
		string pathRelacaoInscricao = Console.ReadLine() ?? string.Empty;
		var inscricoes = service.CarregarInscricoesArquivo(pathRelacaoInscricao);

		Console.WriteLine(inscricoes.Count());
	}
	catch (Exception)
	{
		throw;
	}
}

static void ExibirMatriculas(IServiceProvider serviceProvider)
{
	try
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider provider = serviceScope.ServiceProvider;
		IIntegradorSedService service = provider.GetRequiredService<IIntegradorSedService>();

		Console.WriteLine("Informe o caminho (path): Matrículas");
		string pathRelacaoAlunosPorClasse = Console.ReadLine() ?? string.Empty;
		var matriculas = service.CarregarMatriculas(pathRelacaoAlunosPorClasse);

		Console.WriteLine(matriculas.Count());
	}
	catch (Exception)
	{
		throw;
	}
}

static void ExibirInscricoesNaoMatriculadas(IServiceProvider serviceProvider)
{
	try
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider provider = serviceScope.ServiceProvider;
		IIntegradorSedService service = provider.GetRequiredService<IIntegradorSedService>();

		Console.WriteLine("Informe o caminho (path): Inscrições");
		string inscricoesPath = Console.ReadLine() ?? string.Empty;
		var inscricoes = service.CarregarInscricoesArquivo(inscricoesPath);

		Console.WriteLine("Informe o caminho (path): Matrículas");
		string matriculasPath = Console.ReadLine() ?? string.Empty;
		var matriculas = service.CarregarMatriculas(matriculasPath);


		var inscricoesNaoMatriculadas = service.CarregarInscricoesNaoMatriculadas(inscricoesPath, matriculasPath);


		Console.WriteLine($"Inscrições: {inscricoes.Count()}");

		Console.WriteLine($"Matrículas: {matriculas.Count()}");

		Console.WriteLine($"Inscrições não matriculadas: {inscricoesNaoMatriculadas.Count()}");
	}
	catch (Exception)
	{
		throw;
	}
}

static void _storeDataInCsvFile()
{
	StringBuilder output = new StringBuilder();
	output.AppendLine(string.Join(",", new String[] { "1", "2", "3" }));
	// CSV File Output = 1,2,3
}

