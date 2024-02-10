using Dapper;
using Microsoft.Data.SqlClient;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Repository
{
    public class TipoAtendimentoRepository : ITipoAtendimentoRepository
    {
        IConfiguration _configuration;
        public TipoAtendimentoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("CeejaSorocabaDb").Value;
            return connection;
        }
        public int Add(TipoAtendimento tipoAtendimento)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                var storedProcedureName = "spInsertTipoAtendimento";
                count = conn.Execute(storedProcedureName, tipoAtendimento, commandType: System.Data.CommandType.StoredProcedure);
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
                var storedProcedureName = "spDeleteTipoAtendimento";
                count = conn.Execute(storedProcedureName, new { TipoAtendimentoId = id }, commandType: System.Data.CommandType.StoredProcedure);
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
        public int Edit(TipoAtendimento tipoAtendimento)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "UPDATE Produtos SET Name = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = " + produto.ProdutoId;
                var storedProcedureName = "spUpdateTipoAtendimento";
                count = conn.Execute(storedProcedureName, tipoAtendimento, commandType: System.Data.CommandType.StoredProcedure);
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
        public TipoAtendimento Get(int id)
        {
            var connectionString = this.GetConnection();
            TipoAtendimento tipoAtendimento = new TipoAtendimento();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwTipoAtendimento WHERE TipoAtendimentoId = @TipoAtendimentoId";
                tipoAtendimento = conn.Query<TipoAtendimento>(query, new { TipoAtendimentoId = id }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return tipoAtendimento;
        }
        public List<TipoAtendimento> GetAll()
        {
            var connectionString = this.GetConnection();
            List<TipoAtendimento> tipoAtendimentos = new List<TipoAtendimento>();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwTipoAtendimento";
                tipoAtendimentos = conn.Query<TipoAtendimento>(query).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return tipoAtendimentos;
        }
    }
}
