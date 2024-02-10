using Dapper;
using Microsoft.Data.SqlClient;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Repository
{
    public class TipoEnsinoRepository : ITipoEnsinoRepository
    {
        IConfiguration _configuration;

        public TipoEnsinoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("CeejaSorocabaDb").Value;
            return connection;
        }

        public int Add(TipoEnsino tipoEnsino)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                var storedProcedureName = "spInsertTipoEnsino";
                count = conn.Execute(storedProcedureName, tipoEnsino, commandType: System.Data.CommandType.StoredProcedure);
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
                var storedProcedureName = "spDeleteTipoEnsino";
                count = conn.Execute(storedProcedureName, new { TipoEnsinoId = id }, commandType: System.Data.CommandType.StoredProcedure);
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
        public int Edit(TipoEnsino tipoEnsino)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "UPDATE Produtos SET Name = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = " + produto.ProdutoId;
                var storedProcedureName = "spUpdateTipoEnsino";
                count = conn.Execute(storedProcedureName, tipoEnsino, commandType: System.Data.CommandType.StoredProcedure);
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
        public TipoEnsino Get(int id)
        {
            var connectionString = this.GetConnection();
            TipoEnsino tipoEnsino = new TipoEnsino();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwTipoEnsino WHERE TipoEnsinoId = @TipoEnsinoId";
                tipoEnsino = conn.Query<TipoEnsino>(query, new { TipoEnsinoId = id }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return tipoEnsino;
        }
        public List<TipoEnsino> GetAll()
        {
            var connectionString = this.GetConnection();
            List<TipoEnsino> tipoEnsinos = new List<TipoEnsino>();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwTipoEnsino";
                tipoEnsinos = conn.Query<TipoEnsino>(query).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return tipoEnsinos;
        }
    }
}

