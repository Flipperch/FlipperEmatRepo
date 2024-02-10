using Dapper;
using Microsoft.Data.SqlClient;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Repository
{
    public class RematriculaRepository : IRematriculaRepository
    {
        IConfiguration _configuration;
        public RematriculaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("CeejaSorocabaDb").Value;
            return connection;
        }
        public int Add(Rematricula rematricula)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                var storedProcedureName = "spInsertRematricula";
                count = conn.Execute(storedProcedureName, rematricula, commandType: System.Data.CommandType.StoredProcedure);
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
                var storedProcedureName = "spDeleteRematricula";
                count = conn.Execute(storedProcedureName, new { RematriculaId = id }, commandType: System.Data.CommandType.StoredProcedure);
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
        public int Edit(Rematricula rematricula)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "UPDATE Produtos SET Name = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = " + produto.ProdutoId;
                var storedProcedureName = "spUpdateRematricula";
                count = conn.Execute(storedProcedureName, rematricula, commandType: System.Data.CommandType.StoredProcedure);
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
        public Rematricula Get(int id)
        {
            var connectionString = this.GetConnection();
            Rematricula rematricula = new Rematricula();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwRematricula WHERE RematriculaId = @RematriculaId";
                rematricula = conn.Query<Rematricula>(query, new { RematriculaId = id }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rematricula;
        }
        public List<Rematricula> GetAll()
        {
            var connectionString = this.GetConnection();
            List<Rematricula> rematriculas = new List<Rematricula>();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwRematricula";
                rematriculas = conn.Query<Rematricula>(query).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rematriculas;
        }
    }
}
