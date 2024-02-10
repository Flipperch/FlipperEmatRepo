using Dapper;
using Microsoft.Data.SqlClient;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Repository
{
    public class DisciplinaRepository : IDisciplinaRepository
    {
        IConfiguration _configuration;
        public DisciplinaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("CeejaSorocabaDb").Value;
            return connection;
        }
        public int Add(Disciplina disciplina)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                var storedProcedureName = "spInsertDisciplina";
                count = conn.Execute(storedProcedureName, disciplina, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public int Delete(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "DELETE FROM Produtos WHERE ProdutoId =" + id;
                var storedProcedureName = "spDeleteDisciplina";
                count = conn.Execute(storedProcedureName, new { DisciplinaId = id }, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public int Edit(Disciplina disciplina)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "UPDATE Produtos SET Name = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = " + produto.ProdutoId;
                var storedProcedureName = "spUpdateDisciplina";
                count = conn.Execute(storedProcedureName, disciplina, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public Disciplina Get(int id)
        {
            var connectionString = this.GetConnection();
            Disciplina disciplina = new Disciplina();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwDisciplina WHERE DisciplinaId = @DisciplinaId";
                disciplina = conn.Query<Disciplina>(query, new { DisciplinaId = id }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return disciplina;
        }
        public List<Disciplina> GetAll()
        {
            var connectionString = this.GetConnection();
            List<Disciplina> disciplinas = new List<Disciplina>();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwDisciplina";
                disciplinas = conn.Query<Disciplina>(query).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return disciplinas;
        }
        public List<Disciplina> GetDisciplinasTiposAtendimento()
        {
            var connectionString = this.GetConnection();
            List<Disciplina> disciplinas = new List<Disciplina>();
            var disciplinaDict = new Dictionary<int, Disciplina>();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = @"select
	                            DISCIPLINA.CODIGO DisciplinaId,
	                            DISCIPLINA.NOME NomeDisciplina,
	                            DISCIPLINA.NOME_HISTORICO NomeDisciplinaHistorico,
	                            DISCIPLINA.HORARIO Horario,
	                            DISCIPLINA.CAPACIDADE Capacidade,
	                            DISCIPLINA.ORDEM Ordem,
	                            DISCIPLINA.BLOQ_ATRIBUICAO BloqueioAtribuicao,
	                            ATENDIMENTO.CODIGO TipoAtendimentoId, 
	                            ATENDIMENTO.NOME TipoAtendimentoNome, 
	                            ATENDIMENTO.MENCAO PermiteNota, 
	                            ATENDIMENTO.ATIVO Ativo, 
	                            ATENDIMENTO.ORDEM Ordem
	                           from DISCIPLINA 
	                            JOIN ATENDIMENTO ON DISCIPLINA.CODIGO = ATENDIMENTO.COD_DISCIPLINA";

                disciplinas = conn.Query<Disciplina, TipoAtendimento, Disciplina>(query, (disciplina, tipoAtendimento) =>
                {
                    if (!disciplinaDict.TryGetValue(disciplina.DisciplinaId, out var currentDisciplina))
                    {
                        currentDisciplina = disciplina;
                        disciplinaDict.Add(currentDisciplina.DisciplinaId, currentDisciplina);
                    }

                    currentDisciplina.TiposAtendimento.Add(tipoAtendimento);
                    return currentDisciplina;

                }, splitOn: "TipoAtendimentoId").ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return disciplinas;
        }
    }
}
