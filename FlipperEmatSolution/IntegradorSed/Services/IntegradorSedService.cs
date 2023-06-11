using Emat.IntegracaoSedConsoleApp.Models;
using Emat.IntegracaoSedConsoleApp.Repositories;
using HtmlAgilityPack;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System;
using OpenQA.Selenium.Interactions;

namespace Emat.IntegracaoSedConsoleApp.Services
{
	public class IntegradorSedService : IIntegradorSedService
	{
		public List<Lancamento> ListaLancamentos { get; set; }
		private readonly IAtivoEmatRepository _ativoEmatRepository;
		private readonly IEnumerable<DisciplinaSed> _disciplinas;

		public IntegradorSedService(IAtivoEmatRepository ativoEmatRepository)
		{
			_ativoEmatRepository = ativoEmatRepository;
			_disciplinas = DisciplinaSed.GetListaDisciplinasSed();
			ListaLancamentos = new List<Lancamento>();

		}


		//Inscrições SED

		public IEnumerable<InscricaoSed> CarregarInscricoesArquivo(string path)
		{
			try
			{
				List<InscricaoSed> inscricoes = new List<InscricaoSed>();

				FileAttributes attr = File.GetAttributes(path);

				if (attr.HasFlag(FileAttributes.Directory))
				{
					foreach (var file in Directory.EnumerateFiles(path))
					{
						inscricoes.AddRange(_carregarInscricoes(file));
					}

					return inscricoes;
				}
				else
				{
					return _carregarInscricoes(path);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<InscricaoSed> _carregarInscricoes(string path)
		{
			Log.Information($"Carregando Arquivo {Path.GetDirectoryName(path)}");

			try
			{
				var extension = Path.GetExtension(path);
				if (extension == ".csv")
				{
					return _carregarInscricoesCsv(path);
				}
				else if (extension == ".htm")
				{
					return _carregarInscricoesHtm(path);
				}
				else
				{
					throw new Exception("Formato não suportado.");
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<InscricaoSed> _carregarInscricoesCsv(string path)
		{
			try
			{
				using (var reader = new StreamReader(path))
				{
					List<InscricaoSed> list = new List<InscricaoSed>();
					int i = 0;
					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var values = line?.Split(';');
						if (values?.Length > 2 && i > 2)
						{
							list.Add(new InscricaoSed
							{
								NomeAluno = values[0],
								Ra = values[1],
								DigRa = values[2],
								UfRa = values[3],
								Inscricao = values[4],
								FaseInscricao = values[5],
							});
						}
						i++;
					}
					return list;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<InscricaoSed> _carregarInscricoesHtm(string path)
		{
			try
			{
				List<InscricaoSed> inscricoes = new List<InscricaoSed>();
				var doc = new HtmlDocument();
				doc.Load(path);
				var lines = doc.DocumentNode.SelectNodes("//table/tbody/tr");
				foreach (var line in lines)
				{
					var values = line.ChildNodes;
					inscricoes.Add(new InscricaoSed
					{
						NomeAluno = values[0].InnerText,
						Ra = values[1].InnerText,
						DigRa = values[2].InnerText,
						UfRa = values[3].InnerText,
						Inscricao = values[4].InnerText,
						FaseInscricao = values[5].InnerText,
					});
				}
				return inscricoes;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public IEnumerable<InscricaoSed> CarregarInscricoesSed(string fase, string redeEnsino)
		{
			try
			{
				List<InscricaoSed> inscricoes = new List<InscricaoSed>();
				var chromeWebDriverService = new ChromeWebDriverService();
				_acessarSed(chromeWebDriverService);
				_navegarInscricaoAlunoCeeja(chromeWebDriverService);
				_pesquisarInscricoes(chromeWebDriverService, fase, redeEnsino);
				
				//--Clicar no botão "Tela Cheia"
				chromeWebDriverService.ClickByCssSelector(".decor-datatable-button-container > button:nth-child(1)"); 

				//--Clicar no botão "Imprimir"
				//chromeWebDriverService.ClickByCssSelector("button.btn:nth-child(3)");



				//Inscrição de Alunos
				//Relação de Inscrição
				//Matricular Aluno(a) CEEJA

				chromeWebDriverService.SwitchWindowHandle("Relação de Inscrição");


				Console.WriteLine("Ler Tabela...");
				
				
				//chromeWebDriverService.NewWindowHandle();
				
				

				

				
				//Console.WriteLine($"Title: {chromeWebDriverService.GetWebDriver().Title}");


				//var pageSource = chromeWebDriverService.GetWebDriver().PageSource;
				//File.WriteAllText($"C:\\EmatSolution\\IntegradorSed\\Exports\\PageSource_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.htm", pageSource);
				//pageSource = chromeWebDriverService.GetWebDriver().PageSource;
				//File.WriteAllText($"C:\\EmatSolution\\IntegradorSed\\Exports\\PageSource_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.htm", pageSource);

				return inscricoes;
			}
			catch (Exception)
			{
				throw;
			}
		}


		//Matrículas SED

		public IEnumerable<MatriculaSed> CarregarMatriculas(string path)
		{
			try
			{
				List<MatriculaSed> matriculas = new List<MatriculaSed>();

				FileAttributes attr = File.GetAttributes(path);

				if (attr.HasFlag(FileAttributes.Directory))
				{
					foreach (var file in Directory.EnumerateFiles(path))
					{
						matriculas.AddRange(_carregarMatriculas(file));
					}

					return matriculas;
				}
				else
				{
					return _carregarMatriculas(path);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<MatriculaSed> _carregarMatriculas(string path)
		{
			

			try
			{
				var extension = Path.GetExtension(path);
				if (extension == ".csv")
				{
					return _carregarMatriculasCsv(path);
				}
				else if (extension == ".htm")
				{
					return _carregarMatriculasHtm(path);
				}
				else
				{
					throw new Exception("Formato não suportado.");
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<MatriculaSed> _carregarMatriculasCsv(string path)
		{
			Log.Information($"Carregando Arquivo {path}");

			try
			{
				List<MatriculaSed> list = new List<MatriculaSed>();
				int i = 0;
				using (var reader = new StreamReader(path))
				{
					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var values = line?.Split(';');
						if (values?.Length > 2 && i > 2)
						{
							list.Add(new MatriculaSed
							{
								TipoEnsino = values[0],
								Numero = values[1],
								NomeAluno = values[2],
								Ra = values[3],
								DigRa = values[4],
								UfRa = values[5],
								DataNascimento = values[6],
								Situacao = values[7],
								DataMovimentacao = values[8],
								CategoriaProfissionalCenso = values[9],
								Deficiencia = values[10],
								PosDataCenso = values[11],
							});
						}
						i++;
					}
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<MatriculaSed> _carregarMatriculasHtm(string path)
		{
			Log.Information($"Carregando Arquivo {path}");

			try
			{
				List<MatriculaSed> list = new List<MatriculaSed>();
				var doc = new HtmlDocument();
				doc.Load(path);
				var lines = doc.DocumentNode.SelectNodes("//table/tbody/tr");

				foreach (var line in lines)
				{
					var values = line.ChildNodes;

					list.Add(new MatriculaSed
					{
						TipoEnsino = values[0].InnerText,
						Numero = values[1].InnerText,
						NomeAluno = values[2].InnerText,
						Ra = values[3].InnerText,
						DigRa = values[4].InnerText,
						UfRa = values[5].InnerText,
						DataNascimento = values[6].InnerText,
						Situacao = values[7].InnerText,
						DataMovimentacao = values[8].InnerText,
						CategoriaProfissionalCenso = values[9].InnerText,
						Deficiencia = values[10].InnerText,
						PosDataCenso = values[11].InnerText,
					});
				}

				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}


		//Ativos Emat

		public IEnumerable<AtivoEmat> CarregarAtivosEmat(string path)
		{
			try
			{
				List<AtivoEmat> ativosEmat = new List<AtivoEmat>();

				FileAttributes attr = File.GetAttributes(path);

				if (attr.HasFlag(FileAttributes.Directory)) //Directory 
				{
					foreach (var file in Directory.EnumerateFiles(path))
					{
						ativosEmat.AddRange(_carregarAtivosEmat(file));
					}

					return ativosEmat;
				}
				else //Single File
				{
					return _carregarAtivosEmat(path);
				}

			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<AtivoEmat> _carregarAtivosEmat(string path)
		{
			Log.Information($"Carregando Arquivo {Path.GetDirectoryName(path)}");

			try
			{
				var extension = Path.GetExtension(path);
				if (extension == ".csv")
				{
					return _carregarAtivosEmatCsv(path);
				}
				else if (extension == ".htm")
				{
					throw new NotImplementedException();
					//return _carregarAtivosEmatHtm(path);
				}
				else
				{
					throw new Exception("Formato não suportado.");
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<AtivoEmat> _carregarAtivosEmatCsv(string path)
		{
			try
			{
				List<AtivoEmat> list = new List<AtivoEmat>();
				int i = 0;
				using (var reader = new StreamReader(path))
				{
					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var values = line?.Split(';');
						if (values?.Length > 2 && i > 0)
						{
							list.Add(new AtivoEmat()
							{
								NumeroMatricula = values[0],
								Nome = values[1],
								Ensino = values[2],
								Disciplina = values[3],
								UltimoAtendimento = values[4],
								RaNormalizado = Regex.Replace(values[5], @"\d+", n => n.Value.PadLeft(12, '0')),
								DigRaNormalizado = values[6],
								UfRaNormalizado = values[7],
								RaTbAluno = values[8],
								DigRaTbAluno = values[9],
								UfRaTbAluno = values[10]
							});
						}
						i++;
					}
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}

			////ate aqui
			//try
			//{
			//	//TODO:: Confirmar se é arquivo .csv
			//	using (var reader = new StreamReader(path))
			//	{
			//		List<AtivoEmat> list = new List<AtivoEmat>();
			//		while (!reader.EndOfStream)
			//		{
			//			var line = reader.ReadLine();
			//			var values = line?.Split(';');
			//			if (values?.Length > 0)
			//			{
			//				var ativoEmat = new AtivoEmat()
			//				{
			//					NumeroMatricula = values[0],
			//					Nome = values[1],
			//					Ensino = values[2],
			//					Disciplina = values[3],
			//					UltimoAtendimento = values[4],
			//					RaNormalizado = Regex.Replace(values[5], @"\d+", n => n.Value.PadLeft(12, '0')),
			//					DigRaNormalizado = values[6],
			//					UfRaNormalizado = values[7],
			//					RaTbAluno = values[8],
			//					DigRaTbAluno = values[9],
			//					UfRaTbAluno = values[10]
			//				};

			//				list.Add(ativoEmat);
			//			}
			//		}
			//		return list;
			//	}
			//}
			//catch (Exception)
			//{
			//	throw;
			//}
		}

		public IEnumerable<AtivoEmat> CarregarAtivosEmatDataBase(DateTime dataUltimoAtendimento)
		{
			try
			{
				return _ativoEmatRepository.GetAtivosEmat(dataUltimoAtendimento);
			}
			catch (Exception)
			{

				throw;
			}
		}


		//Métodos diversos...

		public IEnumerable<Lancamento> CarregarLancamentos(string path)
		{
			try
			{
				List<Lancamento> lancamentos = new List<Lancamento>();

				FileAttributes attr = File.GetAttributes(path);

				if (attr.HasFlag(FileAttributes.Directory)) //Directory 
				{
					foreach (var file in Directory.EnumerateFiles(path))
					{
						lancamentos.AddRange(_carregarLancamentos(file));
					}

					return lancamentos;
				}
				else //Single File
				{
					return _carregarLancamentos(path);
				}

			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<Lancamento> _carregarLancamentos(string path)
		{
			Log.Information($"Carregando Arquivo {Path.GetDirectoryName(path)}");

			try
			{
				var extension = Path.GetExtension(path);
				if (extension == ".csv")
				{
					return _carregarLancamentosCsv(path);
				}
				else if (extension == ".htm")
				{
					throw new NotImplementedException();
					//return _carregarAtivosEmatHtm(path);
				}
				else
				{
					throw new Exception("Formato não suportado.");
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<Lancamento> _carregarLancamentosCsv(string path)
		{
			try
			{
				List<Lancamento> lancamentos = new List<Lancamento>();
				int i = 0;
				using (var reader = new StreamReader(path))
				{
					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var values = line?.Split(';');
						if (values?.Length > 2 && i > 0)
						{
							lancamentos.Add(new Lancamento()
							{
								
							});
						}
						i++;
					}
				}
				return lancamentos;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public IEnumerable<AtivoEmat> AtivosEmatNaoMatriculadosNaSed(IEnumerable<MatriculaSed> matriculas, IEnumerable<AtivoEmat> ativos)
		{
			try
			{
				//TODO> Utilizar lista de inscricoes para selecionar novas matriculas ?
				var raMatriculados = matriculas.Select(m => m.Ra);

				//var ativosParaMatricular = ativos.Where(at => !raMatriculados.Contains(at.RaNormalizado)).ToList();
				var ativosParaMatricular = ativos.Where(at => !raMatriculados.Contains(Regex.Replace(at.RaNormalizado, @"\d+", n => n.Value.PadLeft(12, '0')))).ToList();

				return ativosParaMatricular;
			}
			catch (Exception)
			{

				throw;
			}

		}

		public IEnumerable<InscricaoSed> CarregarInscricoesNaoMatriculadas(string pathInscricoes, string pathMatriculas)
		{
			try
			{
				var inscricoes = CarregarInscricoesArquivo(pathInscricoes);
				var matriculas = CarregarMatriculas(pathMatriculas);

				var raMatriculas = matriculas.Select(m => m.Ra);

				var inscricoesNaoMatriculadas = inscricoes.Where(i => !raMatriculas.Contains(i.Ra));

				return inscricoesNaoMatriculadas;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public IEnumerable<AtivoEmat> SelecionarNovasMatriculas(IEnumerable<InscricaoSed> inscricoes, IEnumerable<MatriculaSed> matriculas, IEnumerable<AtivoEmat> ativos)
		{
			Log.Information($"SelecionarNovasMatriculas");

			try
			{
				//TODO> Utilizar lista de inscricoes para selecionar novas matriculas ?
				var raMatriculados = matriculas.Select(m => m.Ra);

				var ativosParaMatricular = ativos.Where(at => !raMatriculados.Contains(at.RaNormalizado)).ToList();

				return ativosParaMatricular;
			}
			catch (Exception)
			{

				throw;
			}

		}
		

		//Ativos Não Inscritos e Não Matriculados

		private static IEnumerable<AtivoEmat> _ativosNaoInscritos(IEnumerable<AtivoEmat> ativos, IEnumerable<InscricaoSed> inscricoes)
		{
			try
			{
				var raInscricoes = inscricoes.Select(m => m.Ra);
				return ativos.Where(at => !raInscricoes.Contains(at.RaNormalizado)).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<AtivoEmat> _ativosNaoMatriculados(IEnumerable<AtivoEmat> ativos, IEnumerable<MatriculaSed> matriculas)
		{
			try
			{
				var raMatriculas = matriculas.Select(m => m.Ra);
				return ativos.Where(at => !raMatriculas.Contains(at.RaNormalizado)).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static IEnumerable<AtivoEmat> _ativosParaInscrever(IEnumerable<AtivoEmat> ativos, IEnumerable<InscricaoSed> inscricoes, IEnumerable<MatriculaSed> matriculas)
		{
			try
			{
				var raInscricoes = inscricoes.Select(i => i.Ra);
				var raMatriculados = matriculas.Select(m => m.Ra);
				var raNaSed = raInscricoes.Union(raMatriculados);
				return ativos.Where(at => !raNaSed.Contains(at.RaNormalizado));
			}
			catch (Exception)
			{
				throw;
			}
		}


		//Métodos Principais

		public void MatricularAtivosEmatNaSed(IEnumerable<AtivoEmat> ativos, string numeroClasse)
		{
			try
			{
				var chromeWebDriverService = new ChromeWebDriverService();

				chromeWebDriverService.NavigateToUrl(@"https://sed.educacao.sp.gov.br");
				chromeWebDriverService.SendKeysById("name", "rg203326428sp");
				chromeWebDriverService.SendKeysById("senha", "gp90308m@");
				chromeWebDriverService.ClickById("botaoEntrar");
				chromeWebDriverService.ExplicitWait("saudacao", TimeSpan.FromSeconds(60));
				chromeWebDriverService.NavigateToUrl(@"https://sed.educacao.sp.gov.br/NCA/Matricula/ConsultaMatricula/IndexCEEJA");
				chromeWebDriverService.SelectByText("tipoPesquisa", "Filtros");
				chromeWebDriverService.SelectByText("codigoDiretoria", "SOROCABA");
				chromeWebDriverService.SelectByText("codigoRedeEnsino", "ESTADUAL - SE", "#codigoRedeEnsino > option:nth-child(3)");
				chromeWebDriverService.SelectByText("codigoTipoEnsino", "53 - EJA ENSINO MEDIO - CEEJA", "#codigoTipoEnsino > option:nth-child(5)");
				chromeWebDriverService.ClickById("btnPesquisar");
				//TODO:: Melhorar seleção classe tanto EF ou EM como A, B, C, etc... Além disso... muda odd/even no css selector
				chromeWebDriverService.ClickByCssSelector($"tr.even:nth-child(4) > td:nth-child(14) > a:nth-child(1) > i:nth-child(1)");
				chromeWebDriverService.ImplicitWait(TimeSpan.FromSeconds(5));

				int _uiMultiSelectIndex = 1;
				//TODO:: Solução temporária para localizar o combobox/select incrementado a cada abertura do modal de add nova matricula.
				foreach (var ativo in ativos)
				{
					Log.Information($"Matricular RA: {ativo.RaNormalizado} - {_uiMultiSelectIndex} / {ativos.Count()}");

					ListaLancamentos.Add(_lancarAlunoSed(ativo, _uiMultiSelectIndex, chromeWebDriverService));

					//Gerar csv de lancamentos
					var fullPath = $"D:\\Spike\\Desktop\\Ceeja Sorocaba 2023\\Lancamentos_{DateTime.Now.ToString("dd_MM_yyyy")}";
					
					Utils.Utils.ExportarParaCsv(fullPath, ListaLancamentos);

					_uiMultiSelectIndex++;
				}

				//Gerar csv de lancamentos
				//Utils.Utils.ToCsv<Lancamento>(Lancamentos);

			}
			catch (Exception)
			{
				throw;
			}
			//finally
			//{
			//	//Gerar csv de lancamentos
			//	Utils.Utils.ToCsv<Lancamento>(ListaLancamentos);
			//}
		}

		private Lancamento _lancarAlunoSed(AtivoEmat ativo, int UiMultiSelectIndex, ChromeWebDriverService matriculaAlunoSedService)
		{
			var lancamento = new Lancamento()
			{
				DataLancamento = DateTime.Now.ToString("G"),
				NumeroMatricula = ativo.NumeroMatricula,
				Ra = ativo.RaNormalizado,
				DigRa = ativo.DigRaNormalizado,
				UfRa = ativo.UfRaNormalizado,
				EnsinoAtual = ativo.Ensino,
				DisciplinaAtual = DisciplinaSed.NormalizarNomeDisciplinaEmatParaSed(ativo.Disciplina),
				//NumeroClasse = numeroClasse
			};


			try
			{
				matriculaAlunoSedService.ClickById("btnAdicionarAluno");

				matriculaAlunoSedService.ExplicitWait("txtNumeroRA", TimeSpan.FromSeconds(5));

				matriculaAlunoSedService.SendKeysById("txtNumeroRA", lancamento.Ra);



				//TODO: Aprimorar verificação do modal para que não fique 5 segundos...
				//WebDriverWait wait = new WebDriverWait(matriculaAlunoSedService._webDriver, TimeSpan.FromSeconds(7));
				WebDriverWait wait = matriculaAlunoSedService.ExplicitWait(7);
				var msgTituloElement = wait.Until(e => e.FindElement(By.CssSelector(".msg-titulo")));
				
				if (msgTituloElement.Text != string.Empty)
				{
					IWebElement msgTextoElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".msg-texto")));

					if (msgTextoElement.Text != string.Empty)
					{
						lancamento.MensagemLancamento = $"RA: {lancamento.Ra} - {msgTituloElement.Text} - {msgTextoElement.Text}";
						matriculaAlunoSedService.ClickByCssSelector(".msg-button");
						matriculaAlunoSedService.ClickByCssSelector("div.modal-footer:nth-child(53) > button:nth-child(2)");
					}
					else
					{
						lancamento.MensagemLancamento = "Ainda acha titulo mas não mensagem.";
					}
				}
				else
				{
					lancamento.MensagemLancamento = "Modal sem titulo e sem mensagem";
				}
			}
			catch (ElementClickInterceptedException ex)
			{
				lancamento.MensagemLancamento = ex.Message;
				lancamento.SituacaoLancamento = "Não Matriculado";
			}
			catch (Exception ex)
			{
				if (matriculaAlunoSedService.ElementIsDisplayed("btnSalvar"))
				{
					lancamento.MensagemLancamento = $"RA - {lancamento.Ra} - Matriculado - {DateTime.Now.ToString("G")}";
					matriculaAlunoSedService.SelectByText("CodigoDisciplina", lancamento.DisciplinaAtual);
					//TODO: Pode não ser mais necessário, pois ao selecionar o "segundo" dropdown, o primeiro será selecionado em cascata. Verificar.
					matriculaAlunoSedService.ClickById("CodigoDisciplina_ms");
					matriculaAlunoSedService.ClickById("CodigoDisciplina_ms");
					string uiMultiSelectOptionId = $"ui-multiselect-{UiMultiSelectIndex}-CodigoDisciplina-option-{_disciplinas.First(d => d.Nome == lancamento.DisciplinaAtual).CodigoDiscipinaOption}";
					matriculaAlunoSedService.ClickById(uiMultiSelectOptionId);
					matriculaAlunoSedService.ClickById("btnSalvar");
					matriculaAlunoSedService.ClickByCssSelector(".msg-button");
					Log.Information(ex.Message);
				}
				else
				{
					lancamento.MensagemLancamento = $"{lancamento.Ra} - btnSalvar não existe. - {ex.Message}"; //PARA TESTES
					matriculaAlunoSedService.ClickByCssSelector("div.modal-footer:nth-child(53) > button:nth-child(2)");
					Log.Information(ex.Message);
				}
			}

			Log.Information(lancamento.MensagemLancamento);
			Thread.Sleep(5000);
			return lancamento;
		}		
		
		public void InscreverAtivosEmatNaSed(IEnumerable<AtivoEmat> ativos)
		{

			try
			{
				var chromeWebDriverService = new ChromeWebDriverService();

				_acessarSed(chromeWebDriverService);

				_navegarInscricaoAlunoCeeja(chromeWebDriverService);

				//Inicio Loop

				foreach (var ativo in ativos)
				{
					chromeWebDriverService.ClickByCssSelector("button.btn-primary:nth-child(1)", "Nova Inscrição Com RA");

					//Modal 
					chromeWebDriverService.SendKeysById("numeroRAPesquisar", ativo.RaNormalizado);

					chromeWebDriverService.ClickByCssSelector("div.modal-footer:nth-child(2) > button:nth-child(1)", "Pesquisar aluno");

					//Modal de Inscrição...
					chromeWebDriverService.SelectByText("codigoDiretoriaInscricao", "SOROCABA");
					chromeWebDriverService.SelectByText("codigoFaseInscricao", "INSCRICAO DE ALUNO FORA DA REDE PUBLICA - ENSINO MEDIO");
					chromeWebDriverService.SelectByText("codigoRedeEnsinoInscricao", "ESTADUAL - SE");
					chromeWebDriverService.SelectByText("codigoTipoEnsinoInscricao", "EJA ENSINO MEDIO - CEEJA");
					chromeWebDriverService.SelectByText("codigoEscolaCeejaInscricao", "980109 - CEEJA NORBERTO SOARES RAMOS PROFESSOR");

					chromeWebDriverService.ClickByCssSelector("div.modal-footer:nth-child(2) > button:nth-child(1)", "Salvar");
					//div.modal-footer:nth-child(2) > button:nth-child(1)

					//Modal Dados do Aluno CEEJA
					chromeWebDriverService.ClickByCssSelector("div.modal-footer:nth-child(3) > button:nth-child(1)", "Atualizar");
					//div.modal-footer:nth-child(3) > button:nth-child(1)

					//Modal Sucesso 
					//Obter Mensagem Modal....

					//Verificar Modal...

					//div.blockUI:nth-child(14)

					WebDriverWait wait2 = chromeWebDriverService.ExplicitWait(7);
					var modal = wait2.Until(ExpectedConditions.ElementExists(By.CssSelector("div.blockUI:nth-child(14)")));

					if (modal != null)
					{
						//Modal Exists
						//.msg-texto > span:nth-child(1)
						//.msg-texto
						WebDriverWait wait = chromeWebDriverService.ExplicitWait(7);
						var msgTituloElement = wait.Until(e => e.FindElement(By.CssSelector(".msg-titulo")));

						if (msgTituloElement != null)
						{
							if (msgTituloElement.Text != string.Empty)
							{
								if (msgTituloElement.Text == "Sucesso")
								{
									chromeWebDriverService.ClickLinkText("Fechar");

								}
							}
						}
					}
				}
			}
			catch (Exception)
			{

				throw;
			}
		}




		//WebDriver Handles

		private void _acessarSed(ChromeWebDriverService chromeWebDriverService)
		{
			Log.Information("Acessando Página da SED...");
			try
			{
				chromeWebDriverService.NavigateToUrl(@"https://sed.educacao.sp.gov.br");
				chromeWebDriverService.SendKeysById("name", "rg203326428sp");
				chromeWebDriverService.SendKeysById("senha", "gp90308m@");
				chromeWebDriverService.ClickById("botaoEntrar");
				chromeWebDriverService.ClickByCssSelector("#sedUiModalWrapper_1body > ul > li:nth-child(2) > a");
				chromeWebDriverService.ExplicitWait("saudacao", TimeSpan.FromSeconds(30));
			}
			catch (Exception)
			{
				throw;
			}
		}
		
		private void _navegarInscricaoAlunoCeeja(ChromeWebDriverService chromeWebDriverService)
		{
			Log.Information("Navegando para a Página de Inscrição Aluno Ceeja...");
			try
			{
				chromeWebDriverService.NavigateToUrl(@"https://sed.educacao.sp.gov.br/NCA/Inscricao/Inscricao/Index?ceeja=true");
			}
			catch (Exception)
			{
				throw;
			}
		}
		
		private void _pesquisarInscricoes(ChromeWebDriverService chromeWebDriverService, string fase, string redeEnsino, string tipoPesquisa = "FILTROS")
		{
			Log.Information("Pesquisando Inscrições...");
			try
			{
				var timeSpanPadrao = TimeSpan.FromSeconds(10);

				chromeWebDriverService.SelecionarOpcaoPeloTexto("tipoPesquisa", tipoPesquisa, timeSpanPadrao);

				chromeWebDriverService.WaitElementIsVisible("divFiltros", timeSpanPadrao);

				chromeWebDriverService.SelecionarOpcaoPeloTexto("fase", fase, timeSpanPadrao);
				
				chromeWebDriverService.SelecionarOpcaoPeloTexto("redeEnsino", redeEnsino, timeSpanPadrao);

				//chromeWebDriverService.ClickById("btnPesquisar");

				chromeWebDriverService.ClicarPeloId("btnPesquisar", timeSpanPadrao);
			}
			catch (Exception)
			{
				throw;
			}
		}


	}
}
